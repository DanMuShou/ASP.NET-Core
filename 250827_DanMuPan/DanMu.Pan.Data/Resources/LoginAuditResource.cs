// OK

using DanMu.Pan.Data.Entities;

namespace DanMu.Pan.Data.Resources;

/// <summary>
/// 登录审计资源参数类，用于登录审计记录的查询参数
/// 继承自ResourceParameter，以LoginTime作为默认排序字段
/// </summary>
public class LoginAuditResource() : ResourceParameter(nameof(LoginAudit.LoginTime))
{
    /// <summary>
    /// 获取或设置用于查询的用户名
    /// </summary>
    public string UserName { get; set; }
}
