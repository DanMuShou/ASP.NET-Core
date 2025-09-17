using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Helper;
using MediatR;

// OK

namespace DanMu.Pan.MediatR.Commands.User;

/// <summary>
/// 删除用户命令类，用于处理用户删除操作
/// </summary>
/// <remarks>
/// 该命令通过MediatR模式处理，执行删除指定用户的操作
/// </remarks>
public class DeleteUserCommand : IRequest<ServiceResponse<UserDto>>
{
    /// <summary>
    /// 获取或设置要删除的用户的唯一标识符
    /// </summary>
    public Guid Id { get; set; }
}
