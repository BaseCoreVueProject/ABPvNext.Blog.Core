## 说明

Abp vNext基础篇的文章还差一个单元测试模块就基本上完成了我争取10.1放假之前给大家赶稿出来，后面我们会开始进阶篇，开始拆一些东西，具体要做的事我会单独开一个文章来讲

## 缘起

本篇文章缘起于dyAbp大佬们在给`夏琳儿(简称：小富婆)`讲解技术的时候发起，因为多用户设计和用户扩展属性设计在社区已经是一个每天都会有人来问一遍的问题，这里浅谈一下我的理解，当然也是根据`EasyAbp作者Super`写的代码浅层的分析。

## 扩展属性

先从我们单用户系统来讲，如果我该如何扩展用户属性？

在Abp默认解决方案`Domain.Shared`中更改`ConfigureExtraProperties`,该操作会向`IdentityUser`实体添加`SocialSecurityNumber`属性

```cs
public static void ConfigureExtraProperties()
{
    OneTimeRunner.Run(() =>
    {
        ObjectExtensionManager.Instance.Modules()
            .ConfigureIdentity(identity =>
            {
                identity.ConfigureUser(user =>
                {
                    user.AddOrUpdateProperty<string>( //property type: string
                        "SocialSecurityNumber", //property name
                        property =>
                        {
                            //validation rules
                            property.Attributes.Add(new RequiredAttribute());
                            property.Attributes.Add(
                                new StringLengthAttribute(64) {
                                    MinimumLength = 4
                                }
                            );

                            //...other configurations for this property
                        }
                    );
                });
            });
    });
}
```

`EntityExtensions`还提供了很多配置操作,这里就简单的举几个常用的例子更多详细操作可以在文章下方连接到官方连接。

```cs
// 默认值选项
property =>
{
    property.DefaultValue = 42;
}
//默认值工厂选项 
property =>
{
    property.DefaultValueFactory = () => DateTime.Now;
}
// 数据注解属性
property =>
{
    property.Attributes.Add(new RequiredAttribute());
    property.Attributes.Add(new StringLengthAttribute(64) {MinimumLength = 4});
}
//验证操作
property =>
{
    property.Attributes.Add(new RequiredAttribute());
    property.Attributes.Add(new StringLengthAttribute(64) {MinimumLength = 4});

    property.Validators.Add(context =>
    {
        if (((string) context.Value).StartsWith("B"))
        {
            context.ValidationErrors.Add(
                new ValidationResult(
                    "Social security number can not start with the letter 'B', sorry!",
                    new[] {"extraProperties.SocialSecurityNumber"}
                )
            );
        }
    });

}
```


目前这种配置方式如果你的前端是mvc或者razor pages是不需要改动代码的，页面会动态生成字段，但是如果是angular就需要人工来操作了，除了扩展属性外，你可能还需要部分或完全覆盖某些服务和页面组件才行，不过Abp官方文档都有相应的操作指南所以没有任何问题。

具体更多操作官方地址：https://docs.abp.io/en/abp/latest/Module-Entity-Extensions

另外就是大家最关系的数据存储问题，默认我们添加的数据都会在ExtraProperties以JSON对象方式进行存储

![extrapropeites](https://raw.githubusercontent.com/abpframework/abp/rel-4.4/docs/en/images/add-new-propert-to-user-database-extra-properties.png)

但如果你想用字段的方式进行存储的话，可以在你的.EntityFrameworkCore项目的类中写下这个。然后您需要使用标准Add-Migration和Update-Database命令来创建新的数据库迁移并将更改应用到您的数据库。

```cs
ObjectExtensionManager.Instance
    .MapEfCoreProperty<IdentityUser, string>(
        "SocialSecurityNumber",
        (entityBuilder, propertyBuilder) =>
        {
            propertyBuilder.HasMaxLength(64);
        }
    );
```
![extrapropeites](https://raw.githubusercontent.com/abpframework/abp/rel-4.4/docs/en/images/add-new-propert-to-user-database-field.png)


## 多用户设计

举例你要开发学生管理系统

* 老师和学生都会进入系统来做自己对应的操作，我们如何来隔离呢？

首先我们就可以想到通过角色来做权限分配做能力隔离

* 然后学生和老师的参数不一样，怎么办，老师要填写工号、系部、教学科目、工龄，学生要填写年度、班级、学号？，看到过比较粗暴的方案就是直接在IdentityUser表全给干上去，但是这种做法相对于某个角色来看是不是太冗余？

这里我参考`Super`的一个做法采用`使用自己的数据库表/集合创建新实体`,具体什么意思呢？

我们创建`Teacher`实体,该实体通过UserId指定`IdentityUser`，来存储作为老师的额外属性
```cs
public class Teacher : AggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }
        
        public virtual Guid UserId { get; protected set; }
        
        public virtual bool Active { get; protected set; }

        [NotNull]
        public virtual string Name { get; protected set; }
        
        public virtual int? Age { get; protected set; }
        
        protected Teacher()
        {
        }

        public Teacher(Guid id, Guid? tenantId, Guid userId, bool active, [NotNull] string name, int? age) : base(id)
        {
            TenantId = tenantId;
            UserId = userId;
            
            Update(active, name, age);
        }

        public void Update(bool active, [NotNull] string name, int? age)
        {
            Active = active;
            Name = name;
            Age = age;
        }
    }
```


处理方案是通过订阅`UserEto`,这是User预定义的专用事件类,当User产生Created、Updated和Deleted操作收会到通知，然后执行我们自己逻辑，
```cs
 [UnitOfWork]
    public class TeacherUserInfoSynchronizer :
        IDistributedEventHandler<EntityCreatedEto<UserEto>>,
        IDistributedEventHandler<EntityUpdatedEto<UserEto>>,
        IDistributedEventHandler<EntityDeletedEto<UserEto>>,
        ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly IUserRoleFinder _userRoleFinder;
        private readonly IRepository<Teacher, Guid> _teacherRepository;

        public TeacherUserInfoSynchronizer(
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IUserRoleFinder userRoleFinder,
            IRepository<Teacher, Guid> teacherRepository)
        {
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _userRoleFinder = userRoleFinder;
            _teacherRepository = teacherRepository;
        }
        
        public async Task HandleEventAsync(EntityCreatedEto<UserEto> eventData)
        {
            if (!await HasTeacherRoleAsync(eventData.Entity))
            {
                return;
            }

            await CreateOrUpdateTeacherAsync(eventData.Entity, true);
        }

        public async Task HandleEventAsync(EntityUpdatedEto<UserEto> eventData)
        {
            if (await HasTeacherRoleAsync(eventData.Entity))
            {
                await CreateOrUpdateTeacherAsync(eventData.Entity, true);
            }
            else
            {
                await CreateOrUpdateTeacherAsync(eventData.Entity, false);
            }
        }

        public async Task HandleEventAsync(EntityDeletedEto<UserEto> eventData)
        {
            await TryUpdateAndDeactivateTeacherAsync(eventData.Entity);
        }

        protected async Task<bool> HasTeacherRoleAsync(UserEto user)
        {
            var roles = await _userRoleFinder.GetRolesAsync(user.Id);

            return roles.Contains(MySchoolConsts.TeacherRoleName);
        }
        
        protected async Task CreateOrUpdateTeacherAsync(UserEto user, bool active)
        {
            var teacher = await FindTeacherAsync(user);

            if (teacher == null)
            {
                teacher = new Teacher(_guidGenerator.Create(), _currentTenant.Id, user.Id, active, user.Name, null);

                await _teacherRepository.InsertAsync(teacher, true);
            }
            else
            {
                teacher.Update(active, user.Name, teacher.Age);

                await _teacherRepository.UpdateAsync(teacher, true);
            }
        }
        
        protected async Task TryUpdateAndDeactivateTeacherAsync(UserEto user)
        {
            var teacher = await FindTeacherAsync(user);

            if (teacher == null)
            {
                return;
            }

            teacher.Update(false, user.Name, teacher.Age);

            await _teacherRepository.UpdateAsync(teacher, true);
        }
        
        protected async Task<Teacher> FindTeacherAsync(UserEto user)
        {
            return await _teacherRepository.FindAsync(x => x.UserId == user.Id);
        }
    }
```


## 结语

我也是在阅读文档和对照Super大佬的代码后自己的理解，文中可能某些地方可能与作者设计有差距，还请大家多多理解！

也欢迎大家阅读我的Abp vNext系列教程

联系作者：加群：867095512  @MrChuJiu
