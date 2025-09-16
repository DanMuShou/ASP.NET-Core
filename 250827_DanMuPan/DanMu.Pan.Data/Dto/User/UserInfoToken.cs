namespace DanMu.Pan.Data.Dto.User;

// OK

/// <summary>
/// 用户信息令牌类，用于存储和传递用户的基本信息
/// </summary>
public class UserInfoToken
{
    /// <summary>
    /// 用户唯一标识符
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 用户邮箱地址
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 用户连接标识符，通常用于SignalR等实时通信场景
    /// </summary>
    public string ConnectionId { get; set; }
}
