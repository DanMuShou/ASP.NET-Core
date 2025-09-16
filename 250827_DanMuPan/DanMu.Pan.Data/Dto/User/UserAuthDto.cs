namespace DanMu.Pan.Data.Dto.User;

// OK

/// <summary>
/// 用户认证数据传输对象
/// 包含用户认证相关信息和权限声明
/// </summary>
public class UserAuthDto
{
    /// <summary>
    /// 用户唯一标识符
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 用户名字
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// 用户姓氏
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// 用户邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 用户电话号码
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// 认证令牌
    /// </summary>
    public string BearerToken { get; set; } = string.Empty;

    /// <summary>
    /// 用户是否已认证
    /// </summary>
    public bool IsAuthenticated { get; set; }

    /// <summary>
    /// 用户头像路径
    /// </summary>
    public string ProfilePhoto { get; set; }

    /// <summary>
    /// 用户是否为管理员
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// 用户声明列表
    /// </summary>
    public List<AppClaimDto> Claims { get; set; }
}
