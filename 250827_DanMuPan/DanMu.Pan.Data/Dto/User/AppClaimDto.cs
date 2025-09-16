namespace DanMu.Pan.Data.Dto.User;

// OK

/// <summary>
/// 应用程序声明数据传输对象
/// 用于表示用户或角色的声明信息
/// </summary>
public class AppClaimDto
{
    /// <summary>
    /// 声明类型
    /// </summary>
    public string ClaimType { get; set; }

    /// <summary>
    /// 声明值
    /// </summary>
    public string ClaimValue { get; set; }
}
