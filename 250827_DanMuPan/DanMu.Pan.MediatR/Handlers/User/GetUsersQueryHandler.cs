using DanMu.Pan.MediatR.Queries.User;
using DanMu.Pan.Repository.User;
using MediatR;

// OK

namespace DanMu.Pan.MediatR.Handlers.User;

/// <summary>
/// 处理获取用户列表的查询请求
/// </summary>
public class GetUsersQueryHandler(IUserRepository userRepository)
    : IRequestHandler<GetUsersQuery, UserList>
{
    /// <summary>
    /// 处理获取用户列表的请求
    /// </summary>
    /// <param name="request">包含用户资源参数的查询请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>包含分页信息的用户列表</returns>
    public async Task<UserList> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken
    ) => await userRepository.GetUsersAsync(request.UserResource);
}
