using DanMu.Pan.Repository.LoginAudit;
using DanMu.Pan.Repository.PropertyMapping;
using DanMu.Pan.Repository.UnitOfWork;
using DanMu.Pan.Repository.User;

namespace DanMuPan.API.Helpers;

/// <summary>
/// 依赖注入扩展类，用于注册服务到DI容器
/// </summary>
public static class DependencyInjectionExtension
{
    /// <summary>
    /// 添加依赖注入服务
    /// </summary>
    /// <param name="services">服务集合</param>
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped<IPropertyMappingService, PropertyMappingService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILoginAuditRepository, LoginAuditRepository>();
    }
}
