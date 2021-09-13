## 缘起

说明：名字用Abp vNext是因为这属于我Abp vNext系列的文章，该问题其实应该属于ids4的部署问题和abp没啥关系。

问题源于周五晚上在和群友聊的时候聊到使用Nginx反向代理后端来做SSL，本来觉得这个问题很简单我就直接部署了一个没想到这就碰到问题了，我的`ids4`的issuer出问题了。

大家可以看下面这幅图，我的Scheme分明是Https但是到了issuer却变成了http。

![问题图](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/35.png)

下面是我的nginx,我把`scheme`和`X-Forwarded-For`都做了配置，但是后端还是拿不到正确的`scheme`。
![Ngxin配置](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/38.png)


## 初步方案

我之前我仔细阅读过老张的ids4系列，我记得他讲过这方面的东西，然后我就去翻阅了他的博客，通过手动修改`IssuerUri`来解决。

![修改IssuerUri](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/36.png)

## 研究文档

我在官方文档看到了这么一句话（建议不要设置此属性，该属性根据主机名推断颁发者名称），我又加以想想发现这样写会存在一个问题，如果我的项目像被多个域名映射岂不是直接歇菜了（当然这个是可以配置的，但是我的想法是约定大于配置，尽量不要去更改这些东西）。

![官方说明](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/37.png)

## 查找根源

我决定自己从源头来看看到底什么原因，我在`IdentityServer4`的源码中找到了下面代码,在此我们知道了为啥设置`IssuerUri`会生效，另外我们也看到`GetIdentityServerOrigin`中的Http来自于`context.Request.Scheme`;

```cs
        /// <summary>
        /// Gets the identity server issuer URI.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public static string GetIdentityServerIssuerUri(this HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            // if they've explicitly configured a URI then use it,
            // otherwise dynamically calculate it
            var options = context.RequestServices.GetRequiredService<IdentityServerOptions>();
            var uri = options.IssuerUri;
            if (uri.IsMissing())
            {
                uri = context.GetIdentityServerOrigin() + context.GetIdentityServerBasePath();
                if (uri.EndsWith("/")) uri = uri.Substring(0, uri.Length - 1);
                if (options.LowerCaseIssuerUri)
                {
                    uri = uri.ToLowerInvariant();
                }
            }

            return uri;
        }


  public static string GetIdentityServerOrigin(this HttpContext context)
        {
            var options = context.RequestServices.GetRequiredService<IdentityServerOptions>();
            var request = context.Request;
            
            if (options.MutualTls.Enabled && options.MutualTls.DomainName.IsPresent())
            {
                if (!options.MutualTls.DomainName.Contains("."))
                {
                    if (request.Host.Value.StartsWith(options.MutualTls.DomainName, StringComparison.OrdinalIgnoreCase))
                    {
                        return request.Scheme + "://" +
                               request.Host.Value.Substring(options.MutualTls.DomainName.Length + 1);
                    }
                }
            }
            
            return request.Scheme + "://" + request.Host.Value;
        }

               /// <summary>
        /// Gets the base path of IdentityServer.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static string GetIdentityServerBasePath(this HttpContext context)
        {
            return context.Items[Constants.EnvironmentKeys.IdentityServerBasePath] as string;
        }

```

## 寻找方案

我在博客园找到了`LouieGuo`的一篇文章,他给出了一个官方issues：https://github.com/dotnet/AspNetCore.Docs/issues/2384

其实主要问题就在于`proxy_pass http://172.17.0.8:8000;`我们请求会被转发，这里就变成了http请求了，导致`context.Request.Scheme`拿到的就是http，解决方案是配置双方(nginx/kestrel)的 X-Forwarded-Proto 让其正确地识别实际用户发出的协议是 http 还是 https。

## 代码

在 `OnApplicationInitialization` 方法中加入
```cs

            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
            };

            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(forwardOptions);
```

![解决方案](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/40.png)

![解决方案](https://git.imweb.io/hdong/ImageBed/raw/master/BlogVnextCore/41.png)


## 反思

这是目前我的一个解决方案，这个方案说实话不是很好，因为对代码有污染，希望大佬如果有更好的方案给我留言蟹蟹！

也欢迎大家阅读我的Abp vNext系列教程

联系作者：加群：867095512  @MrChuJiu
