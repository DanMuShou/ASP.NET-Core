using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Helper;
using MediatR;

namespace DanMu.Pan.MediatR.Commands.User;

/// <summary>
/// 用户登录命令类，用于处理用户登录请求
/// </summary>
/// <remarks>
/// 该类实现了MediatR的IRequest接口，返回类型为ServiceResponse<UserAuthDto>
/// 主要用于通过邮箱验证用户身份并生成认证信息
/// </remarks>
public class UserIntoLoginCommand : IRequest<ServiceResponse<UserAuthDto>>
{
    /// <summary>
    /// 用户邮箱地址
    /// </summary>
    /// <value>用于用户身份验证的邮箱字符串</value>
    public string Email { get; set; }
}
