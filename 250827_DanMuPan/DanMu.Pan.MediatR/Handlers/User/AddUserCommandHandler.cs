using System.Security.Claims;
using AutoMapper;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Info;
using DanMu.Pan.Helper;
using DanMu.Pan.MediatR.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DanMu.Pan.MediatR.Handlers.User;

// OK

/// <summary>
/// 处理添加用户命令的处理器类
/// 负责处理用户注册请求，包括用户信息验证、创建用户账户、设置用户权限等操作
/// </summary>
public class AddUserCommandHandler(
    IMapper mapper,
    ILogger<AddUserCommandHandler> logger,
    UserManager<Data.Entities.User> userManager,
    UserInfoToken userInfoToken,
    ClaimsHelper claimsHelper,
    PathHelper pathHelper
) : IRequestHandler<AddUserCommand, ServiceResponse<UserDto>>
{
    /// <summary>
    /// 处理添加用户命令
    /// </summary>
    /// <param name="request">添加用户命令对象，包含用户的基本信息和权限设置</param>
    /// <param name="cancellationToken">取消令牌，用于取消操作</param>
    /// <returns>返回服务响应对象，包含操作结果或错误信息</returns>
    public async Task<ServiceResponse<UserDto>> Handle(
        AddUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var appUser = await userManager.FindByEmailAsync(request.Email);
        if (appUser != null)
        {
            const string errorMessage = "Email already exist for another user.";
            logger.LogError(errorMessage);
            return ServiceResponse<UserDto>.Return409(errorMessage);
        }

        var entity = mapper.Map<Data.Entities.User>(request);

        entity.UserName = request.Email;

        entity.CreatedBy = userInfoToken.Id;
        entity.ModifiedBy = userInfoToken.Id;
        entity.CreatedDate = DateTime.UtcNow;
        entity.ModifiedDate = DateTime.UtcNow;
        entity.Id = Guid.NewGuid();
        entity.ProfilePhoto = pathHelper.DefaultUserImage;
        var result = await userManager.CreateAsync(entity);
        if (!result.Succeeded)
        {
            return ServiceResponse<UserDto>.Return500();
        }
        await claimsHelper.AddUserClaim(userManager, entity, request.UserClaims, logger); // 为用户添加权限声明

        // 邀请注册流程：管理员邀请用户加入系统，但用户需要通过"忘记密码"流程设置自己的密码
        // 第三方认证：用户通过外部身份提供商(如Google、Facebook)登录，不需要本地密码
        // 分步注册：用户先提供基本信息，后续再完善账户设置
        // 临时账户：创建临时或受限账户，后续再激活
        if (string.IsNullOrEmpty(request.Password))
            return ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(entity));

        var code = await userManager.GeneratePasswordResetTokenAsync(entity);
        var passwordResult = await userManager.ResetPasswordAsync(entity, code, request.Password); //它为指定用户生成一个一次性使用的安全令牌, 这个令牌用于后续的密码设置或重置操作, 可以安全地为用户设置初始密码或重置现有密码
        return passwordResult.Succeeded
            ? ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(entity))
            : ServiceResponse<UserDto>.Return500();
    }
}
