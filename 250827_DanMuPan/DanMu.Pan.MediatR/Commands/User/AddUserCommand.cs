using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Helper;
using MediatR;

// OK

namespace DanMu.Pan.MediatR.Commands.User;

/// <summary>
/// 添加用户命令类，用于封装创建新用户所需的信息
/// 该类实现了IRequest接口，返回类型为ServiceResponse<UserDto>
/// </summary>
public class AddUserCommand : IRequest<ServiceResponse<UserDto>>
{
    /// <summary>
    /// 用户邮箱地址
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 用户名字
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// 用户姓氏
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// 用户密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 用户电话号码
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    /// 用户地址信息
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 用户是否激活状态标识
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 用户是否为管理员标识
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// 用户声明信息
    /// </summary>
    public UserClaimDto UserClaims { get; set; }
}
