using AutoMapper;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Info;
using DanMu.Pan.Helper;
using DanMu.Pan.MediatR.Queries.User;
using DanMu.Pan.Repository.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// OK

namespace DanMu.Pan.MediatR.Handlers.User;

/// <summary>
/// 处理获取用户信息的查询请求
/// </summary>
/// <param name="userRepository">用户数据仓储，用于访问用户数据</param>
/// <param name="userManager">用户管理器，用于处理用户相关的操作如获取用户声明</param>
/// <param name="mapper">对象映射器，用于在实体和DTO之间进行转换</param>
/// <param name="logger">日志记录器，用于记录处理过程中的日志信息</param>
public class GetUserQueryHandler(
    IUserRepository userRepository,
    IMapper mapper,
    ILogger<GetUserQueryHandler> logger,
    UserManager<Data.Entities.User> userManager,
    ClaimsHelper claimsHelper
) : IRequestHandler<GetUserQuery, ServiceResponse<UserDto>>
{
    /// <summary>
    /// 处理获取用户信息的查询请求
    /// </summary>
    /// <param name="request">获取用户查询请求对象，包含要获取的用户ID</param>
    /// <param name="cancellationToken">取消令牌，用于在请求取消时停止操作</param>
    /// <returns>包含用户信息的服务响应对象</returns>
    public async Task<ServiceResponse<UserDto>> Handle(
        GetUserQuery request,
        CancellationToken cancellationToken
    )
    {
        var user = await userRepository
            .FindByInclude(user => user.Id == request.Id)
            .FirstOrDefaultAsync(); // 根据用户ID查找用户实体
        if (user == null)
        {
            logger.LogError(ErrorMessageStr.UserNotExist);
            return ServiceResponse<UserDto>.Return404(ErrorMessageStr.UserNotExist);
        }

        // 获取用户声明并映射到UserDto
        var claims = await userManager.GetClaimsAsync(user);
        var userDto = mapper.Map<UserDto>(user);
        userDto.UserClaim = claimsHelper.GetUserClaims(claims);
        return ServiceResponse<UserDto>.ReturnResultWith200(userDto);
    }
}
