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
/// 处理用户个人资料更新命令的处理器类
/// </summary>
public class UpdateUserProfileCommandHandler(
    ILogger<UpdateUserProfileCommandHandler> logger,
    IMapper mapper,
    UserManager<Data.Entities.User> userManager,
    UserInfoToken userInfoToken,
    PathHelper pathHelper
) : IRequestHandler<UpdateUserProfileCommand, ServiceResponse<UserDto>>
{
    /// <summary>
    /// 处理用户个人资料更新请求
    /// </summary>
    /// <param name="request">用户个人资料更新命令请求对象，包含要更新的用户个人信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>返回包含更新后用户信息的响应结果，如果更新成功返回200状态码，失败则返回相应错误码</returns>
    public async Task<ServiceResponse<UserDto>> Handle(
        UpdateUserProfileCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByIdAsync(userInfoToken.Id.ToString());
        if (user == null)
        {
            logger.LogError(ErrorMessageStr.UserNotExist);
            return ServiceResponse<UserDto>.Return409(ErrorMessageStr.UserNotExist);
        }

        // 更新用户个人资料信息
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.Address = request.Address;

        user.ModifiedBy = userInfoToken.Id;
        user.ModifiedDate = DateTime.UtcNow;

        var result = await userManager.UpdateAsync(user); // 保存更新到数据库
        if (!result.Succeeded)
            return ServiceResponse<UserDto>.Return500();

        // 处理用户头像路径
        if (!string.IsNullOrWhiteSpace(user.ProfilePhoto))
            user.ProfilePhoto = Path.Combine(pathHelper.UserProfilePath, user.ProfilePhoto);
        return ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(user));
    }
}
