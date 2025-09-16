using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Helper;
using DanMu.Pan.Repository.BaseList;

// OK

namespace DanMu.Pan.Repository.User;

/// <summary>
/// 用户列表类，继承自BaseList<UserDto, Data.Entities.User>，提供用户分页和数据处理功能
/// </summary>
public class UserList : BaseList<UserDto, Data.Entities.User>
{
    /// <summary>
    /// 路径帮助类实例，用于处理文件路径相关操作
    /// </summary>
    private PathHelper _pathHelper;

    /// <summary>
    /// 构造函数，初始化UserList实例
    /// </summary>
    /// <param name="pathHelper">路径帮助类实例，用于处理文件路径相关操作</param>
    public UserList(PathHelper pathHelper) => _pathHelper = pathHelper;

    /// <summary>
    /// 构造函数，使用指定的用户列表和分页信息初始化UserList实例
    /// </summary>
    /// <param name="items">用户DTO列表</param>
    /// <param name="count">总记录数</param>
    /// <param name="skip">跳过的记录数</param>
    /// <param name="pageSize">每页记录数</param>
    public UserList(List<UserDto> items, int count, int skip, int pageSize)
        : base(items, count, skip, pageSize) { }

    /// <summary>
    /// 异步创建用户列表
    /// </summary>
    /// <param name="source">用户实体的查询源</param>
    /// <param name="skip">跳过的记录数</param>
    /// <param name="pageSize">每页记录数</param>
    /// <returns>包含分页数据的 UserList 实例</returns>
    public async Task<UserList> Create(
        IQueryable<Data.Entities.User> source,
        int skip,
        int pageSize
    )
    {
        var count = await GetCount(source);
        var dtoList = await GetDtoList(
            source,
            skip,
            pageSize,
            user => new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
                ProfilePhoto = Path.Combine(_pathHelper.UserProfilePath, user.ProfilePhoto),
            },
            null
        );
        return new UserList(dtoList, count, skip, pageSize);
    }

    /// <summary>
    /// 获取指定用户的存储空间大小
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户存储空间大小（字节）</returns>
    private long GetSize(Guid userId)
    {
        var path = Path.Combine(
            _pathHelper.ContentRootPath,
            _pathHelper.DocumentPath,
            userId.ToString()
        );
        if (!Directory.Exists(path))
            return 0;

        var dirInfo = new DirectoryInfo(path);
        var size = DirectorySizeCalculation.DirectorySize(dirInfo, true);
        return size;
    }
}
