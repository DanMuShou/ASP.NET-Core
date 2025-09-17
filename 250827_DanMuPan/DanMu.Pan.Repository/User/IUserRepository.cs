using System.Security.Claims;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Resources;
using DanMu.Pan.Repository.GenericRepository;

// OK

namespace DanMu.Pan.Repository.User;

/// <summary>
/// 用户仓储接口，提供用户相关的数据访问操作
/// </summary>
public interface IUserRepository : IGenericRepository<Data.Entities.User>
{
    /// <summary>
    /// 根据用户资源参数获取用户列表
    /// </summary>
    /// <param name="userResource">用户资源参数，包含分页和查询条件</param>
    /// <returns>用户列表信息</returns>
    Task<UserList> GetUsersAsync(UserResource userResource);

    /// <summary>
    /// 构建用户认证对象
    /// </summary>
    /// <param name="user">应用程序用户实体</param>
    /// <param name="claims">用户声明列表</param>
    /// <returns>用户认证数据传输对象</returns>
    UserAuthDto BuildUserAuthObject(DanMu.Pan.Data.Entities.User user, IList<Claim> claims);

    /// <summary>
    /// 获取共享用户列表
    /// </summary>
    /// <param name="userResource">用户资源参数，包含分页和查询条件</param>
    /// <param name="folderUsers">文件夹用户ID列表</param>
    /// <param name="documentUsers">文档用户ID列表</param>
    /// <returns>共享用户列表信息</returns>
    Task<UserList> GetSharedUsers(
        UserResource userResource,
        List<Guid> folderUsers,
        List<Guid> documentUsers
    );
}
