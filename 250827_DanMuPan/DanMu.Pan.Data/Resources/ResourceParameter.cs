namespace DanMu.Pan.Data.Resources;

// OK

/// <summary>
/// 资源参数类，用于分页和查询资源数据
/// </summary>
/// <param name="orderBy">排序字段</param>
public class ResourceParameter(string orderBy)
{
    private const int MaxPageSize = 100;

    /// <summary>
    /// 获取排序字段
    /// </summary>
    public string OrderBy => orderBy;

    /// <summary>
    /// 跳过的记录数，用于分页
    /// </summary>
    public int Skip { get; set; } = 0;

    private int _pageSize = 10;

    /// <summary>
    /// 每页记录数，用于分页
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    /// <summary>
    /// 搜索查询字符串
    /// </summary>
    public string SearchQuery { get; set; } = string.Empty;

    /// <summary>
    /// 要返回的字段列表
    /// </summary>
    public string Fields { get; set; } = string.Empty;
}
