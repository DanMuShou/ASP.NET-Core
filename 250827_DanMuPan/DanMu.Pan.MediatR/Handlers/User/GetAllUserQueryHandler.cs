using AutoMapper;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.MediatR.Queries.User;
using DanMu.Pan.Repository.User;
using MediatR;
using Microsoft.EntityFrameworkCore;

// OK

namespace DanMu.Pan.MediatR.Handlers.User;

/// <summary>
/// 处理获取所有用户查询的处理器类
/// </summary>
/// <param name="userRepository">用户数据仓库，用于访问用户数据</param>
/// <param name="mapper">对象映射器，用于在不同对象类型间进行映射</param>
public class GetAllUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    : IRequestHandler<GetAllUserQuery, List<UserDto>>
{
    /// <summary>
    /// 处理获取所有用户的请求
    /// </summary>
    /// <param name="request">获取所有用户的查询请求对象</param>
    /// <param name="cancellationToken">取消操作的通知</param>
    /// <returns>包含所有用户信息的DTO列表</returns>
    public async Task<List<UserDto>> Handle(
        GetAllUserQuery request,
        CancellationToken cancellationToken
    )
    {
        var userList = await userRepository.All.ToListAsync();
        var userDtoList = mapper.Map<List<UserDto>>(userList);
        return userDtoList;
    }
}
