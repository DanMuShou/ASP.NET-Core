using DanMu.Pan.Data.Dto;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Entities;
using DanMu.Pan.Domain.Context;
using DanMu.Pan.Helper;
using DanMu.Pan.MediatR;
using DanMuPan.API.Helpers;
using DanMuPan.API.Helpers.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace DanMuPan.API;

// TODO : 未完成代码

public class Startup(IConfiguration configuration, IWebHostEnvironment env)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMediatR(configs =>
            configs.RegisterServicesFromAssembly(typeof(MediatRPos).Assembly)
        );
        services.AddAutoMapper(
            (config) => config.AddMaps(typeof(MapperConfig).Assembly),
            typeof(MapperConfig).Assembly
        );
        services.AddDbContext<DocumentContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly("DanMu.Pan.Domain")
            ) // 指定迁移程序集为当前项目
        );
        services.AddSignalR(opt =>
        {
            opt.EnableDetailedErrors = true; // 启用详细的错误信息，便于调试SignalR相关错误
            opt.MaximumReceiveMessageSize = 1000000000; // 设置客户端可以接收的最大消息大小为10GB
        });

        var pathHelper = new PathHelper(configuration, env.ContentRootPath);
        services.AddSingleton(pathHelper);
        var setting = GetJwtSetting();
        services.AddSingleton(setting);
        services.AddSingleton<ClaimsHelper>();
        services.AddScoped(c => new UserInfoToken() { Id = Guid.NewGuid() });
        services.AddDependencyInjection();

        services
            .AddIdentity<User, IdentityRole<Guid>>() //IdentityRole<Guid>：这是 Identity 系统提供的默认角色类，使用 Guid 作为主键类型
            .AddEntityFrameworkStores<DocumentContext>() //配置 Identity 系统使用 Entity Framework 作为数据存储
            .AddDefaultTokenProviders(); //添加默认的令牌提供程序

        services.AddJwtConfiguration(setting);

        services.Configure<IdentityOptions>(options =>
        {
            // 配置信息 :
            // 1. 密码配置 (PasswordOptions)
            // RequireDigit - 密码必须包含数字 (默认: true)
            // RequireLowercase - 密码必须包含小写字母 (默认: true)
            // RequireUppercase - 密码必须包含大写字母 (默认: true)
            // RequireNonAlphanumeric - 密码必须包含非字母数字字符 (默认: true)
            // RequiredLength - 密码最小长度 (默认: 6)
            // RequiredUniqueChars - 密码中必须包含的唯一字符数 (默认: 1)
            // 2. 锁定配置 (LockoutOptions)
            // MaxFailedAccessAttempts - 触发锁定的最大失败访问尝试次数 (默认: 5)
            // DefaultLockoutTimeSpan - 默认锁定时长 (默认: 5分钟)
            // AllowedForNewUsers - 新用户是否可以被锁定 (默认: true)
            // 3. 登录配置 (SignInOptions)
            // RequireConfirmedEmail - 是否需要确认邮箱才能登录 (默认: false)
            // RequireConfirmedPhoneNumber - 是否需要确认手机号才能登录 (默认: false)
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 5;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        });

        services.AddControllers();
        services.AddEndpointsApiExplorer(); // 这是 Swagger 所需的
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(
                "v1",
                new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "DanMuPan API",
                    Description = "DanMuPan 网盘系统 API",
                    Contact = new OpenApiContact()
                    {
                        Name = "API Support",
                        Url = new Uri("https://localhost"),
                        Email = "support@example.com",
                    },
                }
            );
        }); // 确保添加了 API Explorer 服务，这是 Swagger 所需的
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger(); // 启用 Swagger 中间件，用于生成 API 文档
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "DanMuPan API v1"); // 指定 Swagger JSON 端点和描述信息
                options.RoutePrefix = "Swagger"; // 设置 Swagger UI 的路由前缀
            }); // 配置 Swagger UI，提供交互式 API 测试界面
        }

        app.UseRouting();

        app.Use(
            async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/Swagger");
                    return;
                }
                await next();
            }
        );

        if (!env.IsDevelopment())
        {
            app.UseAuthentication(); // 添加认证中间件
            app.UseAuthorization();
        }

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private JwtSetting GetJwtSetting() =>
        new()
        {
            Key = configuration["JwtSetting:Secretkey"],
            Audience = configuration["JwtSetting:Audience"],
            Issuer = configuration["JwtSetting:Issuer"],
            MinutesToExpiration = Convert.ToInt32(
                configuration["JwtSetting:AccessTokenExpirationMinutes"]
            ),
        };
}
