namespace DanMu.Pan.Helper;

// OK - 1

/// <summary>
/// 服务响应类，用于封装服务调用的结果 用于增删改查
/// </summary>
/// <typeparam name="T">响应数据的类型</typeparam>
public class ServiceResponse<T>
{
    /// <summary>
    /// 初始化 ServiceResponse 类的新实例，设置状态码和错误信息
    /// </summary>
    /// <param name="statusCode">HTTP状态码</param>
    /// <param name="errors">错误信息列表</param>
    private ServiceResponse(int statusCode, List<string> errors)
    {
        StatusCode = statusCode;
        Errors = errors;
    }

    /// <summary>
    /// 初始化 ServiceResponse 类的新实例，从异常创建错误响应
    /// </summary>
    /// <param name="ex">异常对象</param>
    private ServiceResponse(Exception ex)
    {
        Errors = [ex.Message];
    }

    /// <summary>
    /// 初始化 ServiceResponse 类的新实例，设置数据和状态码
    /// </summary>
    /// <param name="data">响应数据</param>
    /// <param name="statusCode">HTTP状态码</param>
    private ServiceResponse(int statusCode, T data)
    {
        Data = data;
        StatusCode = statusCode;
    }

    /// <summary>
    /// 获取一个值，指示服务调用是否成功
    /// </summary>
    public bool Success => Errors == null || Errors.Count == 0;

    /// <summary>
    /// 获取或设置响应数据
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// 获取或设置HTTP状态码，默认值为200
    /// </summary>
    public int StatusCode { get; set; } = 200;

    /// <summary>
    /// 获取或设置错误信息列表，默认为空列表
    /// </summary>
    public List<string> Errors { get; set; } = [];

    /// <summary>
    /// 创建一个表示异常的响应对象
    /// </summary>
    /// <param name="ex">异常对象</param>
    /// <returns>包含异常信息的ServiceResponse实例</returns>
    public static ServiceResponse<T> ReturnException(Exception ex) => new(ex);

    /// <summary>
    /// 创建一个表示失败的响应对象
    /// </summary>
    /// <param name="statusCode">HTTP状态码</param>
    /// <param name="errors">错误信息列表</param>
    /// <returns>包含错误信息的ServiceResponse实例</returns>
    public static ServiceResponse<T> ReturnFailed(int statusCode, List<string> errors) =>
        new(statusCode, errors);

    /// <summary>
    /// 创建一个表示失败的响应对象
    /// </summary>
    /// <param name="statusCode">HTTP状态码</param>
    /// <param name="errorMessage">错误信息</param>
    /// <returns>包含错误信息的ServiceResponse实例</returns>
    public static ServiceResponse<T> ReturnFailed(int statusCode, string errorMessage) =>
        new(statusCode, [errorMessage]);

    /// <summary>
    /// 创建一个表示成功的响应对象
    /// </summary>
    /// <returns>表示成功的ServiceResponse实例</returns>
    public static ServiceResponse<T> ReturnSuccess() => new(200, null);

    /// <summary>
    /// 创建一个包含数据的响应对象，状态码为200 返回网页
    /// </summary>
    /// <param name="data">响应数据</param>
    /// <returns>包含数据的ServiceResponse实例</returns>
    public static ServiceResponse<T> ReturnResultWith200(T data) => new(200, data);

    /// <summary>
    /// 创建一个包含数据的响应对象，状态码为201 创建新资源
    /// </summary>
    /// <param name="data">响应数据</param>
    /// <returns>包含数据的ServiceResponse实例</returns>
    public static ServiceResponse<T> ReturnResultWith201(T data) => new(201, data);

    /// <summary>
    /// 创建一个包含数据的响应对象，状态码为204 请求中进行资源更新，但是不需要改变当前展示给用户的页面
    /// </summary>
    /// <param name="data">响应数据</param>
    /// <returns>包含数据的ServiceResponse实例</returns>
    public static ServiceResponse<T> ReturnResultWith204(T data) => new(204, data);

    /// <summary>
    /// 创建一个表示服务器内部错误的响应对象，状态码为500
    /// </summary>
    /// <returns>表示服务器内部错误的ServiceResponse实例</returns>
    public static ServiceResponse<T> Return500() =>
        new(500, ["An unexpected fault happened. Try again later."]);

    /// <summary>
    /// 创建一个表示冲突的响应对象，状态码为409
    /// </summary>
    /// <param name="message">冲突错误信息</param>
    /// <returns>表示冲突的ServiceResponse实例</returns>
    public static ServiceResponse<T> Return409(string message) => new(409, [message]);

    /// <summary>
    /// 创建一个表示无法处理的实体的响应对象，状态码为422
    /// </summary>
    /// <param name="message">验证错误信息</param>
    /// <returns>表示验证失败的ServiceResponse实例</returns>
    public static ServiceResponse<T> Return422(string message) => new(422, [message]);

    /// <summary>
    /// 创建一个表示资源未找到的响应对象，状态码为404
    /// </summary>
    /// <returns>表示资源未找到的ServiceResponse实例</returns>
    public static ServiceResponse<T> Return404() => new(404, ["Not Found"]);

    /// <summary>
    /// 创建一个表示资源未找到的响应对象，状态码为404
    /// </summary>
    /// <param name="message">未找到资源的错误信息</param>
    /// <returns>表示资源未找到的ServiceResponse实例</returns>
    public static ServiceResponse<T> Return404(string message) => new(404, [message]);

    /// <summary>
    /// 创建一个表示错误请求的响应对象，状态码为400
    /// </summary>
    /// <param name="message">错误请求的错误信息</param>
    /// <returns>表示错误请求的ServiceResponse实例</returns>
    public static ServiceResponse<T> Return400(string message) => new(400, [message]);
}
