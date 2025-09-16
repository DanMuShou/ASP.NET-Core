using DanMu.Pan.Data.Dto.LoginAuditDto;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Enum;
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

public class UserLoginCommandHandler(
    IUserRepository userRepository,
    UserManager<Data.Entities.User> userManager,
    SignInManager<Data.Entities.User> signInManager,
    ILoginAuditRepository loginAuditRepository,
    IHubContext<UserHub, IHubClient> hubContext
) : IRequestHandler<UserLoginCommand, ServiceResponse<UserAuthDto>>
{
    public async Task<ServiceResponse<UserAuthDto>> Handle(
        UserLoginCommand request,
        CancellationToken cancellationToken
    )
    {
        var loginAuditDto = new LoginAuditDto
        {
            UserName = request.UserName,
            RemoteIP = request.RemoteIp,
            Status = nameof(LoginStatus.Error),
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };
        var signResult = await signInManager.PasswordSignInAsync(
            request.UserName,
            request.Password,
            false, // 表示登录会话是否持久化（即"记住我"功能）
            false // 表示登录失败时是否启用账户锁定机制
        );

        if (!signResult.Succeeded) // 登录失败 记录登录审计
        {
            await loginAuditRepository.LoginAudit(loginAuditDto);
            return ServiceResponse<UserAuthDto>.ReturnFailed(
                401,
                "UserName Or Password is InCorrect."
            );
        }

        var user = await userRepository
            .All.Where(u => u.UserName == request.UserName)
            .FirstOrDefaultAsync();

        if (!user.IsActive) // 检查用户账户是否处于激活状态，如果不是则拒绝登录
        {
            await loginAuditRepository.LoginAudit(loginAuditDto);
            return ServiceResponse<UserAuthDto>.ReturnFailed(
                401,
                "UserName Or Password is InCorrect."
            );
        }

        loginAuditDto.Status = nameof(LoginStatus.Success);
        await loginAuditRepository.LoginAudit(loginAuditDto);

        var claims = await userManager.GetClaimsAsync(user); //获取用户声明信息，构建用户认证对象并返回成功响应
        var authUser = userRepository.BuildUserAuthObject(user, claims);
        var onlineUser = new UserInfoToken { Email = authUser.Email, Id = authUser.Id };
        await hubContext.Clients.All.Joined(onlineUser); //当有用户成功登录系统后，会通知所有在线的其他用户有新用户加入了系统。这在需要实时显示在线用户列表或通知的应用场景中非常有用
        return ServiceResponse<UserAuthDto>.ReturnResultWith200(authUser);
    }
}
