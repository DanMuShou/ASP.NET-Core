using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Helper;
using MediatR;
using Microsoft.AspNetCore.Http;

// OK

namespace DanMu.Pan.MediatR.Commands.User;

/// <summary>
/// 更新用户头像命令类，用于处理用户头像上传和更新操作
/// </summary>
/// <remarks>
/// 该命令通过MediatR模式处理，返回ServiceResponse<UserDto>类型结果
/// </remarks>
public class UpdateUserProfilePhotoCommand : IRequest<ServiceResponse<UserDto>>
{
    /// <summary>
    /// 获取或设置表单文件集合，包含用户上传的头像文件
    /// </summary>
    public IFormFile FormFile { get; set; }

    /// <summary>
    /// 获取或设置根路径，用于指定文件存储的根目录路径
    /// </summary>
    public string RootPath { get; set; }
}
