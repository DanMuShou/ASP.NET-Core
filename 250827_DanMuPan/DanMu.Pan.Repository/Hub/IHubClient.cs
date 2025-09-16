using DanMu.Pan.Data.Dto.User;

// OK

namespace DanMu.Pan.Repository.Hub;

/// <summary>
/// 定义Hub客户端接口，用于处理实时通信相关的方法
/// </summary>
public interface IHubClient
{
    /// <summary>
    /// 通知用户离开
    /// </summary>
    /// <param name="id">用户唯一标识符</param>
    /// <returns>异步任务</returns>
    Task UserLeft(Guid id);

    /// <summary>
    /// 通知新用户上线
    /// </summary>
    /// <param name="userInfo">用户信息令牌</param>
    /// <returns>异步任务</returns>
    Task NewOnlineUser(UserInfoToken userInfo);

    /// <summary>
    /// 通知用户加入
    /// </summary>
    /// <param name="userInfo">用户信息令牌</param>
    /// <returns>异步任务</returns>
    Task Joined(UserInfoToken userInfo);

    /// <summary>
    /// 发送在线用户列表
    /// </summary>
    /// <param name="userInfo">用户信息令牌集合</param>
    /// <returns>异步任务</returns>
    Task OnlineUsers(IEnumerable<UserInfoToken> userInfo);

    /// <summary>
    /// 通知用户登出
    /// </summary>
    /// <param name="userInfo">用户信息令牌</param>
    /// <returns>异步任务</returns>
    Task Logout(UserInfoToken userInfo);

    /// <summary>
    /// 强制用户登出
    /// </summary>
    /// <param name="userInfo">用户信息令牌</param>
    /// <returns>异步任务</returns>
    Task ForceLogout(UserInfoToken userInfo);

    /// <summary>
    /// 发送私信消息
    /// </summary>
    /// <param name="message">消息内容</param>
    /// <param name="userInfo">用户信息令牌</param>
    /// <returns>异步任务</returns>
    Task SendDM(string message, UserInfoToken userInfo);

    /// <summary>
    /// 发送文件夹通知
    /// </summary>
    /// <param name="folderId">文件夹唯一标识符</param>
    /// <returns>异步任务</returns>
    Task FolderNotification(string folderId);

    /// <summary>
    /// 发送移除文件夹通知
    /// </summary>
    /// <param name="folderId">文件夹唯一标识符</param>
    /// <returns>异步任务</returns>
    Task RemoveFolderNotification(string folderId);
}
