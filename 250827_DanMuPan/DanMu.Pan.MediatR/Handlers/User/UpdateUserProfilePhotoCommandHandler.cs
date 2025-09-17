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
/// 处理更新用户头像命令的处理器类
/// </summary>
/// <remarks>
/// 该处理器负责处理UpdateUserProfilePhotoCommand命令，实现用户头像的上传、更新和删除功能
/// </remarks>
public class UpdateUserProfilePhotoCommandHandler(
    IMapper mapper,
    ILogger<UpdateUserProfilePhotoCommandHandler> logger,
    UserManager<Data.Entities.User> userManager,
    UserInfoToken userInfoToken,
    PathHelper pathHelper
) : IRequestHandler<UpdateUserProfilePhotoCommand, ServiceResponse<UserDto>>
{
    /// <summary>
    /// 处理更新用户头像的请求
    /// </summary>
    /// <param name="request">更新用户头像命令对象，包含表单文件和根路径信息</param>
    /// <param name="cancellationToken">取消令牌，用于取消操作</param>
    /// <returns>返回包含更新后用户信息的服务响应结果</returns>
    public async Task<ServiceResponse<UserDto>> Handle(
        UpdateUserProfilePhotoCommand request,
        CancellationToken cancellationToken
    )
    {
        // 查找当前用户信息
        var user = await userManager.FindByIdAsync(userInfoToken.Id.ToString());
        if (user == null)
        {
            logger.LogError(ErrorMessageStr.UserNotExist);
            return ServiceResponse<UserDto>.Return409(ErrorMessageStr.UserNotExist);
        }

        var dirPath = Path.Combine(request.RootPath, pathHelper.UserProfilePath);
        if (!Directory.Exists(dirPath))
            Directory.CreateDirectory(dirPath); // 确保用户头像目录存在

        if (!string.IsNullOrEmpty(user.ProfilePhoto)) // 删除旧的用户头像文件
        {
            var filePath = Path.Combine(dirPath, user.ProfilePhoto);
            if (File.Exists(filePath) && user.ProfilePhoto != pathHelper.DefaultUserImage)
                File.Delete(filePath);
        }

        if (request.FormFile.Length > 0) // 处理新的头像文件
        {
            var profileFile = request.FormFile;
            var newProfilePhoto = $"{Guid.NewGuid()}{Path.GetExtension(profileFile.Name)}";
            var fullPath = Path.Combine(dirPath, newProfilePhoto);
            await using var stream = new FileStream(fullPath, FileMode.Create);
            await profileFile.CopyToAsync(stream, cancellationToken);
            user.ProfilePhoto = newProfilePhoto;
        }
        else
        {
            user.ProfilePhoto = pathHelper.DefaultUserImage;
        }

        var result = await userManager.UpdateAsync(user); // 更新用户信息
        return !result.Succeeded
            ? ServiceResponse<UserDto>.Return500()
            : ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(user));
    }
}
