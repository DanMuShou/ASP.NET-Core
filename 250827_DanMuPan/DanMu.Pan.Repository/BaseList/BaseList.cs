using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

// OK

namespace DanMu.Pan.Repository.BaseList;

/// <summary>
/// 泛型基类列表，用于分页处理
/// </summary>
/// <typeparam name="TDto">数据传输对象类型</typeparam>
/// <typeparam name="TEntity">实体类型</typeparam>
public class BaseList<TDto, TEntity> : List<TDto>
    where TDto : class
    where TEntity : class
{
    /// <summary>
    /// 初始化 BaseList 类的新实例
    /// </summary>
    /// <param name="items">当前页的 DTO 对象列表</param>
    /// <param name="count">总记录数</param>
    /// <param name="skip">跳过的记录数</param>
    /// <param name="pageSize">每页记录数</param>
    protected BaseList(List<TDto> items, int count, int skip, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        Skip = skip;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        AddRange(items);
    }

    /// <summary>
    /// 受保护的无参构造函数，用于支持某些序列化场景或派生类的初始化
    /// </summary>
    protected BaseList() { }

    /// <summary>
    /// 获取跳过的记录数
    /// </summary>
    public int Skip { get; private set; }

    /// <summary>
    /// 获取总页数
    /// </summary>
    public int TotalPages { get; private set; }

    /// <summary>
    /// 获取每页记录数
    /// </summary>
    public int PageSize { get; private set; }

    /// <summary>
    /// 获取总记录数
    /// </summary>
    public int TotalCount { get; private set; }

    /// <summary>
    /// 异步获取记录总数
    /// </summary>
    /// <param name="source">实体查询源</param>
    /// <returns>记录总数</returns>
    protected async Task<int> GetCount(IQueryable<TEntity> source) =>
        await source.AsNoTracking().CountAsync();

    /// <summary>
    /// 异步获取 DTO 对象列表
    /// </summary>
    /// <param name="source">实体查询源</param>
    /// <param name="skip">跳过的记录数</param>
    /// <param name="pageSize">每页记录数</param>
    /// <param name="selector">用于将实体转换为 DTO 的表达式</param>
    /// <param name="forEach">对每个 DTO 对象执行的操作，可为空</param>
    /// <returns>DTO 对象列表</returns>
    protected async Task<List<TDto>> GetDtoList(
        IQueryable<TEntity> source,
        int skip,
        int pageSize,
        Expression<Func<TEntity, TDto>> selector,
        Action<TDto>? forEach
    )
    {
        var list = await source
            .AsNoTracking()
            .Skip(skip)
            .Take(pageSize)
            .Select(selector)
            .ToListAsync();
        if (forEach != null)
            list.ForEach(forEach);
        return list;
    }
}
