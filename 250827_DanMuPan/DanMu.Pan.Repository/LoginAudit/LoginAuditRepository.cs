using DanMu.Pan.Data.Dto.LoginAuditDto;
using DanMu.Pan.Data.Resources;
using DanMu.Pan.Domain.Context;
using DanMu.Pan.Repository.GenericRepository;
using DanMu.Pan.Repository.PropertyMapping;
using DanMu.Pan.Repository.UnitOfWork;
using Microsoft.Extensions.Logging;

// OK

namespace DanMu.Pan.Repository.LoginAudit;

/// <summary>
/// 登录审计仓储实现类，用于处理登录审计相关的数据操作
/// </summary>
/// <param name="unitOfWork">工作单元，用于管理数据库事务</param>
/// <param name="logger">日志记录器，用于记录操作日志</param>
/// <param name="propertyMappingService">属性映射服务，用于处理排序属性映射</param>
public class LoginAuditRepository(
    IUnitOfWork<DocumentContext> unitOfWork,
    ILogger<LoginAuditRepository> logger,
    IPropertyMappingService propertyMappingService
) : GenericRepository<Data.Entities.LoginAudit, DocumentContext>(unitOfWork), ILoginAuditRepository
{
    /// <summary>
    /// 获取登录审计记录列表，支持分页和排序
    /// </summary>
    /// <param name="loginAuditResrouce">登录审计资源参数，包含分页、排序和查询条件</param>
    /// <returns>包含分页数据的登录审计列表</returns>
    public Task<LoginAuditList> GetDocumentAuditTrails(LoginAuditResource loginAuditResrouce)
    {
        var collectionBeforePaging = All;
        collectionBeforePaging.ApplyShort(
            loginAuditResrouce.OrderBy,
            propertyMappingService.GetPropertyMapping<LoginAuditDto, Data.Entities.LoginAudit>()
        );
        if (!string.IsNullOrWhiteSpace(loginAuditResrouce.UserName))
        {
            collectionBeforePaging = collectionBeforePaging.Where(loginAudit =>
                loginAudit.UserName.Contains(loginAuditResrouce.UserName)
            );
        }

        var loginAuditList = new LoginAuditList();
        return loginAuditList.Create(
            collectionBeforePaging,
            loginAuditResrouce.Skip,
            loginAuditResrouce.PageSize
        );
    }

    /// <summary>
    /// 记录用户登录审计信息
    /// </summary>
    /// <param name="loginAudit">登录审计信息DTO</param>
    /// <returns>异步任务</returns>
    public async Task LoginAudit(LoginAuditDto loginAudit)
    {
        try
        {
            Add(
                new Data.Entities.LoginAudit()
                {
                    Id = Guid.NewGuid(),
                    LoginTime = DateTime.UtcNow,
                    Provider = loginAudit.Provider,
                    RemoteIP = loginAudit.RemoteIP,
                    Status = loginAudit.Status,
                    UserName = loginAudit.UserName,
                    Latitude = loginAudit.Latitude,
                    Longitude = loginAudit.Longitude,
                }
            );
            await unitOfWork.SaveAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }
    }
}
