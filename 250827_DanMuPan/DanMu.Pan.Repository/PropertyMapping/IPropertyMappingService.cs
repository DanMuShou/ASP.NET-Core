namespace DanMu.Pan.Repository.PropertyMapping;

// OK

/// <summary>
/// 属性映射服务接口，用于处理源类型和目标类型之间的属性映射
/// </summary>
public interface IPropertyMappingService
{
    /// <summary>
    /// 验证指定类型之间是否存在有效的属性映射
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <typeparam name="TDestination">目标类型</typeparam>
    /// <param name="fields">要验证的字段列表</param>
    /// <returns>如果存在有效映射则返回true，否则返回false</returns>
    bool ValidMappingExistsFor<TSource, TDestination>(string fields);

    /// <summary>
    /// 获取指定类型之间的属性映射字典
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <typeparam name="TDestination">目标类型</typeparam>
    /// <returns>包含属性映射的字典，键为属性名，值为PropertyMappingValue对象</returns>
    Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
}
