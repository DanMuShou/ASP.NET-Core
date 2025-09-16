namespace DanMu.Pan.Data.Dto.User;

// OK

/// <summary>
/// 用户数据传输对象类，用于在应用程序各层之间传输用户信息
/// </summary>
public class UserDto
{
    /// <summary>
    /// 获取或设置用户的唯一标识符
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 获取或设置用户的登录名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 获取或设置用户的电子邮箱地址
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 获取或设置用户的名字
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// 获取或设置用户的姓氏
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// 获取或设置用户的电话号码
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// 获取或设置用户的地址信息
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 获取或设置用户头像的URL路径
    /// </summary>
    public string ProfilePhoto { get; set; }

    /// <summary>
    /// 获取或设置用户账户的提供商信息
    /// </summary>
    public string Provider { get; set; }

    /// <summary>
    /// 获取或设置用户账户是否处于激活状态
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 获取或设置用户是否具有管理员权限
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// 获取或设置用户存储空间大小（字节）
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// 获取或设置用户的权限声明信息
    /// </summary>
    public UserClaimDto UserClaim { get; set; } = null;
}
