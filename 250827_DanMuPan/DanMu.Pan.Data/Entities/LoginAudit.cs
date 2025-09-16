namespace DanMu.Pan.Data.Entities;

/// <summary>
/// 登录审计实体类，用于存储和跟踪用户登录信息
/// </summary>
public class LoginAudit
{
    /// <summary>
    /// 获取或设置登录审计记录的唯一标识符
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 获取或设置登录用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 获取或设置登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }

    /// <summary>
    /// 获取或设置登录来源的远程IP地址
    /// </summary>
    public string RemoteIP { get; set; }

    /// <summary>
    /// 获取或设置登录状态（如：成功、失败等）
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// 获取或设置身份验证提供者信息
    /// </summary>
    public string Provider { get; set; }

    /// <summary>
    /// 获取或设置登录位置的纬度坐标
    /// </summary>
    public string Latitude { get; set; }

    /// <summary>
    /// 获取或设置登录位置的经度坐标
    /// </summary>
    public string Longitude { get; set; }
}
