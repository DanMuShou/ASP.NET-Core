// OK

namespace DanMu.Pan.Data.Dto.LoginAuditDto;

/// <summary>
/// 登录审计信息数据传输对象
/// 用于记录和传输用户登录的相关审计信息
/// </summary>
public class LoginAuditDto
{
    /// <summary>
    /// 审计记录唯一标识符
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 登录用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

    /// <summary>
    /// 登录来源IP地址
    /// </summary>
    public string RemoteIP { get; set; }

    /// <summary>
    /// 登录状态（成功/失败）
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// 身份验证提供程序
    /// </summary>
    public string Provider { get; set; }

    /// <summary>
    /// 登录位置纬度
    /// </summary>
    public string Latitude { get; set; }

    /// <summary>
    /// 登录位置经度
    /// </summary>
    public string Longitude { get; set; }
}
