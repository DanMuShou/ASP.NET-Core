using DanMu.Pan.Data.Dto.User;

// OK

namespace DanMu.Pan.Repository.Hub;

/// <summary>
/// 连接映射仓储接口，用于管理用户连接信息和在线状态
/// </summary>
public interface IConnectionMappingRepository
{
    /// <summary>
    /// 添加或更新用户连接信息
    /// </summary>
    /// <param name="tempUserInfo">用户信息令牌</param>
    /// <param name="connectionId">连接ID</param>
    /// <returns>操作是否成功</returns>
    bool AddUpdate(UserInfoToken tempUserInfo, string connectionId);

    /// <summary>
    /// 移除用户连接信息
    /// </summary>
    /// <param name="tempUserInfo">用户信息令牌</param>
    void Remove(UserInfoToken tempUserInfo);

    /// <summary>
    /// 获取除指定用户外的所有在线用户
    /// </summary>
    /// <param name="tempUserInfo">用户信息令牌</param>
    /// <returns>在线用户信息令牌集合</returns>
    IEnumerable<UserInfoToken> GetAllUsersExceptThis(UserInfoToken tempUserInfo);

    /// <summary>
    /// 根据用户信息获取用户详细信息
    /// </summary>
    /// <param name="tempUserInfo">用户信息令牌</param>
    /// <returns>用户信息令牌</returns>
    UserInfoToken GetUserInfo(UserInfoToken tempUserInfo);

    /// <summary>
    /// 根据用户ID获取用户信息
    /// </summary>
    /// <param name="id">用户唯一标识符</param>
    /// <returns>用户信息令牌</returns>
    UserInfoToken GetUserInfoByName(Guid id);

    /// <summary>
    /// 根据连接ID获取用户信息
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>用户信息令牌</returns>
    UserInfoToken GetUserInfoByConnectionId(string connectionId);

    /// <summary>
    /// 根据用户ID列表获取在线用户列表
    /// </summary>
    /// <param name="users">用户ID列表</param>
    /// <returns>在线用户信息令牌列表</returns>
    List<UserInfoToken> GetOnlineUserByList(List<Guid> users);

    /// <summary>
    /// 向指定用户列表发送文件夹通知
    /// </summary>
    /// <param name="users">用户ID列表</param>
    /// <param name="folderId">文件夹唯一标识符</param>
    /// <returns>异步任务</returns>
    Task SendFolderNotification(List<Guid> users, Guid folderId);

    /// <summary>
    /// 向指定用户列表发送移除文件夹通知
    /// </summary>
    /// <param name="users">用户ID列表</param>
    /// <param name="folderId">文件夹唯一标识符</param>
    /// <returns>异步任务</returns>
    Task RemovedFolderNotification(List<Guid> users, Guid folderId);
}
