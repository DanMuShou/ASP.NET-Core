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
using Microsoft.EntityFrameworkCore;

// OK

namespace DanMu.Pan.MediatR.Handlers.User;

/// <summary>
/// 处理用户登录命令的处理器类
/// </summary>
public class UserLoginCommandHandler(
    ILoginAuditRepository loginAuditRepository,
    IUserRepository userRepository,
    IHubContext<UserHub, IHubClient> hubContext,
    UserManager<Data.Entities.User> userManager,
    SignInManager<Data.Entities.User> signInManager
) : IRequestHandler<UserLoginCommand, ServiceResponse<UserAuthDto>>
{
    /// <summary>
    /// 处理用户登录请求
    /// </summary>
    /// <param name="request">用户登录命令请求对象，包含用户名、密码、IP地址等登录信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回包含用户认证信息的响应结果，如果登录成功返回200状态码及认证信息，失败则返回401错误码及相应错误信息</returns>
    public async Task<ServiceResponse<UserAuthDto>> Handle(
        UserLoginCommand request,
        CancellationToken cancellationToken
    )
    {
        // 创建登录审计记录对象
        var loginAuditDto = new LoginAuditDto
        {
            UserName = request.UserName,
            RemoteIP = request.RemoteIp,
            Status = nameof(LoginStatus.Error),
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };

        var user = await userManager.FindByNameAsync(request.UserName);
        if (user is not { IsActive: true, IsDeleted: false }) // 检查用户账户是否处于激活状态，如果不是则拒绝登录
        {
            await loginAuditRepository.LoginAudit(loginAuditDto);
            return ServiceResponse<UserAuthDto>.ReturnFailed(401, ErrorMessageStr.UserNotExist);
        }

        // 执行密码验证登录
        var signResult = await signInManager.PasswordSignInAsync(
            request.UserName,
            request.Password,
            false, // 表示登录会话是否持久化（即"记住我"功能）
            false // 表示登录失败时是否启用账户锁定机制
        );

        if (!signResult.Succeeded) // 记录登录审计
        {
            await loginAuditRepository.LoginAudit(loginAuditDto);
            return ServiceResponse<UserAuthDto>.ReturnFailed(401, ErrorMessageStr.LoginInvalid);
        }

        loginAuditDto.Status = nameof(LoginStatus.Success);
        await loginAuditRepository.LoginAudit(loginAuditDto);

        // 获取用户声明信息，构建用户认证对象并返回成功响应
        var claims = await userManager.GetClaimsAsync(user);
        var authUser = userRepository.BuildUserAuthObject(user, claims);
        var onlineUser = new UserInfoToken { Email = authUser.Email, Id = authUser.Id };

        // 当有用户成功登录系统后，会通知所有在线的其他用户有新用户加入了系统。
        // 这在需要实时显示在线用户列表或通知的应用场景中非常有用
        await hubContext.Clients.All.Joined(onlineUser);
        return ServiceResponse<UserAuthDto>.ReturnResultWith200(authUser);
    }
}
