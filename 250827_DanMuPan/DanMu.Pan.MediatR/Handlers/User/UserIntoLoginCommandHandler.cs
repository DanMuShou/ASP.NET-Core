using DanMu.Pan.Data.Dto.LoginAuditDto;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Enum;
using DanMu.Pan.Data.Info;
using DanMu.Pan.Helper;
using DanMu.Pan.MediatR.Commands.User;
using DanMu.Pan.Repository.Hub;
using DanMu.Pan.Repository.LoginAudit;
using DanMu.Pan.Repository.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

// OK

namespace DanMu.Pan.MediatR.Handlers.User;

/// <summary>
/// 用户登录命令处理器，处理用户登录请求
/// </summary>
/// <remarks>
/// 该类实现了MediatR的IRequestHandler接口，用于处理UserIntoLoginCommand命令，
/// 验证用户凭据并返回认证结果
/// </remarks>
/// <param name="userRepository">用户数据仓库，用于访问用户数据</param>
/// <param name="userManager">用户管理器，用于处理用户相关的身份验证操作</param>
/// <param name="loginAuditRepository">登录审计仓库，用于记录登录日志</param>
/// <param name="hubContext">SignalR集线器上下文，用于实时通信</param>
public class UserIntoLoginCommandHandler(
    IUserRepository userRepository,
    ILoginAuditRepository loginAuditRepository,
    IHubContext<UserHub, IHubClient> hubContext,
    UserManager<Data.Entities.User> userManager
) : IRequestHandler<UserIntoLoginCommand, ServiceResponse<UserAuthDto>>
{
    /// <summary>
    /// 处理用户登录请求
    /// </summary>
    /// <param name="request">用户登录命令请求，包含登录所需的信息</param>
    /// <param name="cancellationToken">取消令牌，用于取消操作</param>
    /// <returns>返回ServiceResponse<UserAuthDto>类型的结果，包含用户认证信息或错误信息</returns>
    public async Task<ServiceResponse<UserAuthDto>> Handle(
        UserIntoLoginCommand request,
        CancellationToken cancellationToken
    )
    {
        var loginAuditDto = new LoginAuditDto
        {
            UserName = request.Email,
            RemoteIP = "",
            Status = nameof(LoginStatus.Error),
            Latitude = "",
            Longitude = "",
        };

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is not { IsActive: true, IsDeleted: false })
        {
            await loginAuditRepository.LoginAudit(loginAuditDto);
            return ServiceResponse<UserAuthDto>.ReturnFailed(401, ErrorMessageStr.LoginInvalid);
        }

        loginAuditDto.Status = nameof(LoginStatus.Success);
        await loginAuditRepository.LoginAudit(loginAuditDto);
        var claims = await userManager.GetClaimsAsync(user);
        var authUser = userRepository.BuildUserAuthObject(user, claims);
        var onlineUser = new UserInfoToken { Email = authUser.Email, Id = authUser.Id };
        await hubContext.Clients.All.Joined(onlineUser);
        return ServiceResponse<UserAuthDto>.ReturnResultWith200(authUser);
    }
}
