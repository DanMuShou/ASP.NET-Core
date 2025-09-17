using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Helper;
using MediatR;

// OK

namespace DanMu.Pan.MediatR.Commands.User;

/// <summary>
/// 修改用户密码命令类，用于通过MediatR处理用户密码变更请求
/// </summary>
public class ChangePasswordCommand : IRequest<ServiceResponse<UserDto>>
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 用户当前密码（旧密码）
    /// </summary>
    public string OldPassword { get; set; }

    /// <summary>
    /// 用户新密码
    /// </summary>
    public string NewPassword { get; set; }
}
