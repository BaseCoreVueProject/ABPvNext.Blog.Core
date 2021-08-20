## 缘起

该问题来自于`ABP Framework 研习社`的`Avatar`,聊天记录和文章已经和本人沟通过了,发这篇文章是因为今天他升级`ABP 4.4`不小心写出了点问题找到我，我才想可以把这个事写一下。老哥在群里提问ABP如何走角色授权，当时看到这个问题并没有在意因为ABP默认只是扩展了策略授权，角色授权直接走微软的就可以了。

但是：当我看到这个老哥的role验证的时候，我的第一直觉是这个人是个憨憨，怎么可以这样写角色授权，官方明明提供了。

![Jwt](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/24.png)

后面这位老哥发出了他的Jwt解析，成功引起了我的注意，他说他用角色授权一直不通过，无奈才用了上面的方案

![Jwt](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/25.png)

![Jwt](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/22.png)

当时群里很多大佬都在提出解决方案，当时我也觉得这个应该可以授权成功，但是我还是抱有丢丢的怀疑，我觉得去帮这个老哥解决这个问题。

![方案讨论](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/23.png)



## 解密

带着疑惑我开始一探究竟，首先官方提供了`IsInRole`方法来验证角色是否有权限，然后根据`IAuthorizationRequirement`引用我找到了`RolesAuthorizationRequirement`,
代码里已经很明显了。

![官方说明](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/26.png)

![源码](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/27.png)

请注意下面这个`RoleClaimType`,微软要求我们的角色key是`"http://schemas.microsoft.com/ws/2008/06/identity/claims/role"`而这个老哥用的是`Role`,这就直接找到问题原因了。

```cs
   public virtual bool IsInRole(string role)
    {
      for (int index = 0; index < this._identities.Count; ++index)
      {
        if (this._identities[index] != null && this._identities[index].HasClaim(this._identities[index].RoleClaimType, role))
          return true;
      }
      return false;
    }
```


```cs
    private string _roleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
```


## 验证设想

我将我的猜想发给这个老哥，让他进行实验，实验证明我的猜想对了，接下来就是解决问题了，他的这个token是授权服务器颁发的，授权服务器颁发下来key就是`role`,但是微软的`RolesAuthorizationRequirement`要求你必须是`"http://schemas.microsoft.com/ws/2008/06/identity/claims/role"`.

![测试](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/28.png)


## 安全落地

接下来就是给老哥出方案了，虽然老哥的基础有点让我懵圈了但是这不是问题！

![教育](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/29.png)


我们在认证中间和和授权中间件加入如下代码，针对`Role`进行替换。

```cs
app.Use(async (ctx, next) =>
            {
                var currentPrincipalAccessor = ctx.RequestServices.GetRequiredService<IHttpContextAccessor>();
                var map = new Dictionary<string, string>()
                {
                    { "role", ClaimTypes.Role },
                };
                var mapClaims = currentPrincipalAccessor.HttpContext.User.Claims.Where(p => map.Keys.Contains(p.Type)).ToList();
                currentPrincipalAccessor.HttpContext.User.AddIdentity(new ClaimsIdentity(mapClaims.Select(p =>

                    new Claim(map[p.Type], p.Value, p.ValueType, p.Issuer))

                ));
                await next();
            });
```

![方案落地](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/30.png)



## 反思

ABP框架值得学习，但是.Net Core官方文档也要记得经常去看一下巩固复习。不过这个地方也应该有其他更好的解决方案，希望大佬如果有更好的方案给我留言蟹蟹！

https://github.com/abpframework/abp/commit/d8bdcffd4c69787e69fb7ae3c3375a44a0067e7c#diff-536faff86021c0373581d67458a5bdf291c1df099523e119493c8211b541c27e

联系作者：加群：867095512  @MrChuJiu

非常欢迎各位针对日常开发中使用ABP遇到的疑难杂症找我提问(ps:我不一定能解决，哈哈哈)。
