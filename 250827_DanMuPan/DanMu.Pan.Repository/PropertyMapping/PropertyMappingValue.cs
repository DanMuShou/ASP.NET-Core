namespace DanMu.Pan.Repository.PropertyMapping;

/// <summary>
/// 属性映射值类，用于存储属性映射的相关信息
/// </summary>
/// <param name="destinationProperties">目标属性集合</param>
/// <param name="revert">是否需要反转映射，默认为false</param>
public class PropertyMappingValue(IEnumerable<string> destinationProperties, bool revert = false)
{
    /// <summary>
    /// 获取目标属性集合
    /// </summary>
    public IEnumerable<string> DestinationProperties { get; private set; } = destinationProperties;

    /// <summary>
    /// 获取是否需要反转映射的标志
    /// </summary>
    public bool Revert { get; private set; } = revert;
}
