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
/// 处理用户修改密码命令的处理器类
/// </summary>
public class ChangePasswordCommandHandler(
    IMapper mapper,
    ILogger<ChangePasswordCommandHandler> logger,
    SignInManager<Data.Entities.User> signInManager,
    UserManager<Data.Entities.User> userManager
) : IRequestHandler<ChangePasswordCommand, ServiceResponse<UserDto>>
{
    /// <summary>
    /// 处理修改密码请求
    /// </summary>
    /// <param name="request">修改密码命令请求对象，包含用户名、旧密码和新密码</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回包含用户信息的响应结果，如果修改成功返回200状态码，失败则返回相应错误码</returns>
    public async Task<ServiceResponse<UserDto>> Handle(
        ChangePasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await signInManager.PasswordSignInAsync(
            request.UserName,
            request.OldPassword,
            false,
            false
        ); // 验证旧密码是否正确
        if (!result.Succeeded)
        {
            logger.LogError(ErrorMessageStr.OldPasswordNotMatch);
            return ServiceResponse<UserDto>.ReturnFailed(422, ErrorMessageStr.OldPasswordNotMatch);
        }

        // 执行密码修改操作
        var user = await userManager.FindByNameAsync(request.UserName);
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var passwordResult = await userManager.ResetPasswordAsync(user, token, request.NewPassword);
        return passwordResult.Succeeded
            ? ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(user))
            : ServiceResponse<UserDto>.Return500();
    }
}
