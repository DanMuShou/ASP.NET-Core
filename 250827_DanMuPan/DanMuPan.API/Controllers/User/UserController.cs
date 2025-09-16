using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Resources;
using DanMu.Pan.Helper;
using DanMu.Pan.MediatR.Commands.User;
using DanMu.Pan.MediatR.Queries.User;
using DanMu.Pan.Repository.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DanMuPan.API.Controllers.User;

// TODO : 代码未完成

[Controller]
[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class UserController(
    IMediator mediator,
    IWebHostEnvironment webHostEnvironment,
    PathHelper pathHelper,
    UserInfoToken UserInfo
) : BaseController
{
    /// <summary>
    /// 添加新用户
    /// </summary>
    /// <param name="addUserCommand">添加用户命令对象，包含用户的基本信息</param>
    /// <returns>如果添加成功，返回Created状态码和用户信息；如果添加失败，返回格式化的错误响应</returns>
    [HttpPost]
    [Produces("application/json", "application/xml", Type = typeof(UserDto))] // 明确指定 API 端点支持的响应格式 json xaml, 明确指定 API 返回的数据类型是 UserDto, Swagger/OpenAPI 文档工具会使用这些信息生成准确的 API 文档
    public async Task<IActionResult> AddUser(AddUserCommand addUserCommand)
    {
        // 传统 Controller → DTO → Service → Entity
        // 实现 Controller → Command → Handler → Entity
        // Command -> 执行操作的请求
        // Query -> 获取数据的请求
        var result = await mediator.Send(addUserCommand);
        return result.Success
            ? CreatedAtAction("GetUser", new { id = result.Data.Id }, result.Data)
            : ReturnFormattedResponse(result);
    }

    /// <summary>
    /// 获取所有用户列表
    /// </summary>
    /// <returns>返回包含所有用户信息的列表</returns>
    [HttpGet(nameof(GetAllUsers))]
    [Produces("application/json", "application/xml", Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var getAllUserQuery = new GetAllUserQuery(); // Query -> 获取数据的请求
        var result = await mediator.Send(getAllUserQuery);
        return Ok(result);
    }

    /// <summary>
    /// 根据用户ID获取特定用户的信息
    /// </summary>
    /// <param name="id">要获取的用户的唯一标识符</param>
    /// <returns>返回包含用户信息的响应结果，如果用户存在则返回用户数据，否则返回相应的错误信息</returns>
    [HttpGet("{id:guid}", Name = nameof(GetUser))]
    [Produces("application/json", "application/xml", Type = typeof(UserDto))]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var getUserQuery = new GetUserQuery(id);
        var result = await mediator.Send(getUserQuery);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// 根据用户资源参数获取用户列表，支持分页和过滤
    /// </summary>
    /// <param name="userResource">用户资源参数，包含分页、排序和过滤条件</param>
    /// <returns>返回包含用户信息的列表以及分页元数据</returns>
    [HttpGet(nameof(GetUsers))]
    [Produces("application/json", "application/xml", Type = typeof(UserList))]
    public async Task<IActionResult> GetUsers([FromQuery] UserResource userResource)
    {
        var getUsersQuery = new GetUsersQuery(userResource);
        var result = await mediator.Send(getUsersQuery);

        var paginationMetadata = new
        {
            totalCount = result.TotalCount,
            pageSize = result.PageSize,
            skip = result.Skip,
            totalPages = result.TotalPages,
        };

        // 将分页元数据添加到响应头中
        Response.Headers.Append(
            "X-Pagination",
            System.Text.Json.JsonSerializer.Serialize(paginationMetadata)
        );

        return Ok(result);
    }

    /// <summary>
    /// 获取最近注册的用户列表
    /// </summary>
    /// <returns>返回包含最近注册的用户信息的列表</returns>
    [HttpGet(nameof(GetRecentlyRegisteredUsers))]
    [Produces("application/json", "application/xml", Type = typeof(List<UserDto>))]
    public async Task<IActionResult> GetRecentlyRegisteredUsers()
    {
        var getRecentlyRegisteredUserQuery = new GetRecentlyRegisteredUserQuery { };
        var result = await mediator.Send(getRecentlyRegisteredUserQuery);
        return Ok(result);
    }

    /// <summary>
    /// 处理用户登录请求
    /// </summary>
    /// <param name="userLoginCommand">用户登录命令对象，包含用户名、密码和位置信息</param>
    /// <returns>如果登录成功，返回包含认证信息的用户数据；如果登录失败，返回错误响应</returns>
    [AllowAnonymous]
    [HttpPost(nameof(UserLogin))]
    [Produces("application/json", "application/xml", Type = typeof(UserAuthDto))]
    public async Task<IActionResult> UserLogin(UserLoginCommand userLoginCommand)
    {
        userLoginCommand.RemoteIp = HttpContext.Connection.RemoteIpAddress.ToString();
        var result = await mediator.Send(userLoginCommand);
        if (!result.Success)
            return ReturnFormattedResponse(result);
        if (!string.IsNullOrWhiteSpace(result.Data.ProfilePhoto))
            result.Data.ProfilePhoto = pathHelper.UserProfilePath + result.Data.ProfilePhoto;

        return Ok(result.Data);
    }

    /// <summary>
    /// 处理用户登录请求
    /// </summary>
    /// <param name="userLoginCommand">用户登录命令对象，包含登录所需的信息</param>
    /// <returns>返回IActionResult类型结果，包含用户认证信息或错误信息</returns>
    [HttpPost(nameof(UserIntoLogin))]
    [Produces("application/json", "application/xml", Type = typeof(UserAuthDto))]
    public async Task<IActionResult> UserIntoLogin(UserIntoLoginCommand userLoginCommand)
    {
        var result = await mediator.Send(userLoginCommand);
        if (!result.Success)
            return ReturnFormattedResponse(result);
        if (!string.IsNullOrWhiteSpace(result.Data.ProfilePhoto))
            result.Data.ProfilePhoto = Path.Combine(
                pathHelper.UserProfilePath,
                result.Data.ProfilePhoto
            );
        return Ok(result.Data);
    }

    [HttpPut("{id:guid}")]
    [Produces("application/json", "application/xml", Type = typeof(UserDto))]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserCommand updateUserCommand)
    {
        updateUserCommand.Id = id;
        var result = await mediator.Send(updateUserCommand);
        return ReturnFormattedResponse(result);
    }
}
