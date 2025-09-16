using System.Linq.Dynamic.Core;

// TODO : 未完成代码

namespace DanMu.Pan.Repository.PropertyMapping;

public static class QueryableExtensions
{
    /// <summary>
    /// 对IQueryable数据源应用排序
    /// </summary>
    /// <typeparam name="T">数据源中的元素类型</typeparam>
    /// <param name="source">要排序的数据源</param>
    /// <param name="orderBy">排序字符串，支持多个字段，以逗号分隔，支持desc后缀表示降序</param>
    /// <param name="mappingDic">属性映射字典，键为属性名，值为PropertyMappingValue对象</param>
    /// <returns>排序后的IQueryable数据源</returns>
    public static IQueryable<T> ApplyShort<T>(
        this IQueryable<T> source,
        string orderBy,
        Dictionary<string, PropertyMappingValue> mappingDic
    )
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(mappingDic);
        if (string.IsNullOrWhiteSpace(orderBy))
            return source;

        var orderByAfterSplit = orderBy.Split(",");
        // 反向处理排序子句以确保正确的排序优先级
        // 当用户传递一个排序字符串如"Name, Age desc"时，他们期望的结果是先按Name升序排序，再按Age降序排序。
        // 然而，LINQ的OrderBy和ThenBy操作的工作方式是：后应用的排序会覆盖先应用的排序，除非它们的值不同。
        foreach (var orderByClause in orderByAfterSplit.Reverse())
        {
            var trimOrderByClause = orderByClause.Trim();
            var isOrderDes = trimOrderByClause.EndsWith(" desc");
            var indexOfFirstSpace = trimOrderByClause.IndexOf(' ');
            var propertyName =
                indexOfFirstSpace == -1
                    ? trimOrderByClause
                    : trimOrderByClause.Remove(indexOfFirstSpace);

            // 检查属性映射是否存在
            if (!mappingDic.TryGetValue(propertyName, out var propertyMappingValue))
                throw new ArgumentException($"Key mapping for {propertyName} is missing");
            if (propertyMappingValue == null)
                throw new ArgumentNullException($"Key mapping for {propertyName} Value is null ");

            foreach (
                var destinationProperty in propertyMappingValue.DestinationProperties.Reverse()
            ) // 这里遍历映射属性值中的目标属性集合（DestinationProperties）。一个API属性可能映射到实体上的多个属性，这些属性都会被用于排序。使用.Reverse()是为了确保正确的排序优先级。
            {
                if (propertyMappingValue.Revert)
                    isOrderDes = !isOrderDes;

                source = source.OrderBy(
                    destinationProperty + (isOrderDes ? " descending" : " ascending")
                );
            }
        }
        return source;
    }
}
