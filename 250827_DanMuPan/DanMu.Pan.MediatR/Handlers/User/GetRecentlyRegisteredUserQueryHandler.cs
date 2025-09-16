using AutoMapper;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.MediatR.Queries.User;
using DanMu.Pan.Repository.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DanMu.Pan.MediatR.Handlers.User;

/// <summary>
/// 处理获取最近注册用户列表的查询请求
/// </summary>
public class GetRecentlyRegisteredUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetRecentlyRegisteredUserQuery, List<UserDto>>
{
    /// <summary>
    /// 处理获取最近注册用户列表的请求
    /// </summary>
    /// <param name="request">获取最近注册用户的查询请求</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>最近注册的用户DTO列表</returns>
    public async Task<List<UserDto>> Handle(
        GetRecentlyRegisteredUserQuery request,
        CancellationToken cancellationToken
    )
    {
        var userList = await userRepository
            .All.OrderByDescending(u => u.CreatedDate)
            .Take(10)
            .ToListAsync();
        return mapper.Map<List<UserDto>>(userList);
    }
}
