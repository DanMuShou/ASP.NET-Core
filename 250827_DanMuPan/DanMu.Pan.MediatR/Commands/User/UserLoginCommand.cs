using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Helper;
using MediatR;

// OK

namespace DanMu.Pan.MediatR.Commands.User;

/// <summary>
/// 用户登录命令，用于处理用户身份验证请求
/// </summary>
public class UserLoginCommand : IRequest<ServiceResponse<UserAuthDto>>
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 远程IP地址
    /// </summary>
    public string RemoteIp { get; set; }

    /// <summary>
    /// 纬度
    /// </summary>
    public string Latitude { get; set; }

    /// <summary>
    /// 经度
    /// </summary>
    public string Longitude { get; set; }
}
