using AutoMapper;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Info;
using DanMu.Pan.Helper;
using DanMu.Pan.MediatR.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

// OK

namespace DanMu.Pan.MediatR.Handlers.User;

/// <summary>
/// 处理删除用户命令的处理器类
/// </summary>
/// <remarks>
/// 该处理器负责处理DeleteUserCommand命令，实现用户软删除功能
/// </remarks>
public class DeleteUserCommandHandler(
    IMapper mapper,
    ILogger<DeleteUserCommandHandler> logger,
    UserManager<Data.Entities.User> userManager,
    UserInfoToken userInfoToken,
    ClaimsHelper claimsHelper
) : IRequestHandler<DeleteUserCommand, ServiceResponse<UserDto>>
{
    /// <summary>
    /// 处理删除用户的请求
    /// </summary>
    /// <param name="request">删除用户命令对象，包含要删除的用户ID</param>
    /// <param name="cancellationToken">取消令牌，用于取消操作</param>
    /// <returns>返回包含删除操作结果的服务响应</returns>
    public async Task<ServiceResponse<UserDto>> Handle(
        DeleteUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByIdAsync(userInfoToken.Id.ToString()); // 查找当前用户信息
        if (user == null)
        {
            logger.LogError(ErrorMessageStr.UserNotExist);
            return ServiceResponse<UserDto>.Return409(ErrorMessageStr.UserNotExist);
        }

        // 软删除用户：标记为已删除而不是物理删除
        user.IsDeleted = true;
        user.DeletedDate = DateTime.Now;
        user.DeletedBy = userInfoToken.Id;

        // 更新用户信息
        var result = await userManager.UpdateAsync(user);
        return result.Succeeded
            ? ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(user))
            : ServiceResponse<UserDto>.Return500();
    }
}
