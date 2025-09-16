using DanMu.Pan.Data.Dto.LoginAuditDto;
using DanMu.Pan.Repository.BaseList;

namespace DanMu.Pan.Repository.LoginAudit;

/// <summary>
/// 登录审计列表类，用于处理登录审计记录的分页查询
/// </summary>
public class LoginAuditList : BaseList<LoginAuditDto, Data.Entities.LoginAudit>
{
    public LoginAuditList() { }

    /// <summary>
    /// 初始化 LoginAuditList 类的新实例
    /// </summary>
    /// <param name="items">当前页的登录审计 DTO 对象列表</param>
    /// <param name="count">登录审计记录总数</param>
    /// <param name="skip">跳过的记录数</param>
    /// <param name="pageSize">每页记录数</param>
    private LoginAuditList(List<LoginAuditDto> items, int count, int skip, int pageSize)
        : base(items, count, skip, pageSize) { }

    /// <summary>
    /// 异步创建登录审计列表
    /// </summary>
    /// <param name="source">登录审计实体查询源</param>
    /// <param name="skip">跳过的记录数</param>
    /// <param name="pageSize">每页记录数</param>
    /// <returns>包含分页数据的登录审计列表</returns>
    public async Task<LoginAuditList> Create(
        IQueryable<Data.Entities.LoginAudit> source,
        int skip,
        int pageSize
    )
    {
        var count = await GetCount(source);
        var dtoList = await GetDtoList(
            source,
            skip,
            pageSize,
            c => new LoginAuditDto
            {
                Id = c.Id,
                LoginTime = c.LoginTime,
                Provider = c.Provider,
                RemoteIP = c.RemoteIP,
                Status = c.Status,
                UserName = c.UserName,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
            },
            null
        );
        return new LoginAuditList(dtoList, count, skip, pageSize);
    }
}
