namespace DanMu.Pan.Repository.PropertyMapping;

// OK

/// <summary>
/// 属性映射类，用于定义从源类型TSource到目标类型TDestination的属性映射关系
/// </summary>
/// <typeparam name="TSource">源类型</typeparam>
/// <typeparam name="TDestination">目标类型</typeparam>
/// <param name="mappingDic">包含属性映射关系的字典，键为属性名，值为PropertyMappingValue对象</param>
public class PropertyMapping<TSource, TDestination>(
    Dictionary<string, PropertyMappingValue> mappingDic
) : IPropertyMapping
{
    /// <summary>
    /// 获取属性映射字典
    /// </summary>
    public Dictionary<string, PropertyMappingValue> MappingDic => mappingDic;
}
