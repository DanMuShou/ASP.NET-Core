using DanMu.Pan.Data.Dto.User;

// TODO : 未完成代码

namespace DanMu.Pan.Repository.PropertyMapping;

/// <summary>
/// 属性映射服务实现类，用于管理不同类型之间的属性映射关系
/// </summary>
public class PropertyMappingService : IPropertyMappingService
{
    /// <summary>
    /// 初始化属性映射服务，添加默认的用户实体与用户DTO之间的映射关系
    /// </summary>
    public PropertyMappingService()
    {
        _propertyMappings.Add(new PropertyMapping<UserDto, Data.Entities.User>(_userMapping));
    }

    private readonly List<IPropertyMapping> _propertyMappings = [];

    private readonly Dictionary<string, PropertyMappingValue> _userMapping = new()
    {
        { nameof(UserDto.Id), new PropertyMappingValue([nameof(Data.Entities.User.Id)]) },
        {
            nameof(UserDto.UserName),
            new PropertyMappingValue([nameof(Data.Entities.User.UserName)])
        },
        { nameof(UserDto.Email), new PropertyMappingValue([nameof(Data.Entities.User.Email)]) },
        {
            nameof(UserDto.FirstName),
            new PropertyMappingValue([nameof(Data.Entities.User.FirstName)])
        },
        {
            nameof(UserDto.LastName),
            new PropertyMappingValue([nameof(Data.Entities.User.LastName)])
        },
        {
            nameof(UserDto.PhoneNumber),
            new PropertyMappingValue([nameof(Data.Entities.User.PhoneNumber)])
        },
        {
            nameof(UserDto.IsActive),
            new PropertyMappingValue([nameof(Data.Entities.User.IsActive)])
        },
    };

    // private Dictionary<string, PropertyMappingValue> _loginAuditMapping =
    //     new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
    //     {
    //         { "Id", new PropertyMappingValue(new List<string>() { "Id" } ) },
    //         { "UserName", new PropertyMappingValue(new List<string>() { "UserName" } )},
    //         { "LoginTime", new PropertyMappingValue(new List<string>() { "LoginTime" } )},
    //         { "RemoteIP", new PropertyMappingValue(new List<string>() { "RemoteIP" } )},
    //         { "Status", new PropertyMappingValue(new List<string>() { "Status" } )},
    //         { "Provider", new PropertyMappingValue(new List<string>() { "Provider" } )}
    //     };

    /// <summary>
    /// 验证指定类型之间是否存在有效的属性映射
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <typeparam name="TDestination">目标类型</typeparam>
    /// <param name="fields">要验证的字段列表</param>
    /// <returns>如果存在有效映射则返回true，否则返回false</returns>
    public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
    {
        if (string.IsNullOrWhiteSpace(fields))
            return true;

        var propertyMapping = GetPropertyMapping<TSource, TDestination>();
        var fieldsAfterSplit = fields.Split(',');

        foreach (var field in fieldsAfterSplit)
        {
            var trimmedField = field.Trim();
            var indexOfFirstSpace = trimmedField.IndexOf(' ');
            var propertyName =
                indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);
            if (!propertyMapping.TryGetValue(propertyName, out var propertyMappingValue))
                return false;
        }
        return true;
    }

    /// <summary>
    /// 获取指定类型之间的属性映射字典
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <typeparam name="TDestination">目标类型</typeparam>
    /// <returns>包含属性映射的字典，键为属性名，值为PropertyMappingValue对象</returns>
    public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
    {
        var matchMapping = _propertyMappings
            .OfType<PropertyMapping<TSource, TDestination>>()
            .ToList();
        return matchMapping.Count == 1
            ? matchMapping.First().MappingDic
            : throw new Exception(
                $"Cannot find exact property mapping instance for <{typeof(TSource)},{typeof(TDestination)}"
            );
    }
}
