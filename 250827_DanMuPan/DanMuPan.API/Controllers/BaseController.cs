using DanMu.Pan.Helper;
using Microsoft.AspNetCore.Mvc;

namespace DanMuPan.API.Controllers;

/// <summary>
/// BaseController类，继承自ControllerBase，提供基础的控制器功能
/// </summary>
public class BaseController : ControllerBase
{
    /// <summary>
    /// 根据服务响应结果返回格式化的HTTP响应
    /// </summary>
    /// <typeparam name="T">响应数据的类型</typeparam>
    /// <param name="response">服务层返回的响应对象，包含成功状态、数据和错误信息</param>
    /// <returns>
    /// 如果服务调用成功，返回HTTP 200状态码和数据；
    /// 如果服务调用失败，返回对应的HTTP状态码和错误信息
    /// </returns>
    public IActionResult ReturnFormattedResponse<T>(ServiceResponse<T> response) =>
        response.Success ? Ok(response.Data) : StatusCode(response.StatusCode, response.Errors);
}
