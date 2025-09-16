using DanMu.Pan.Data.Entities;

namespace DanMu.Pan.Data.Resources;

// OK

/// <summary>
/// 用户资源参数类，用于用户相关的分页查询和过滤
/// 继承自ResourceParameter，提供用户特定的查询属性
/// </summary>
public class UserResource() : ResourceParameter(nameof(User.Email))
{
    /// <summary>
    /// 用户名字
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// 用户姓氏
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 用户电话号码
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// 用户是否激活状态
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// 文件夹ID
    /// </summary>
    public string FolderId { get; set; } = string.Empty;

    /// <summary>
    /// 文档ID
    /// </summary>
    public string DocumentId { get; set; } = string.Empty;

    /// <summary>
    /// 用户类型
    /// </summary>
    public string Type { get; set; } = string.Empty;
}
