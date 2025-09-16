using System.Linq.Expressions;

namespace DanMu.Pan.Repository.GenericRepository;

// OK

/// <summary>
/// 通用仓储接口，提供对实体的基本数据操作
/// </summary>
/// <typeparam name="TC">实体类型，必须是引用类型</typeparam>
public interface IGenericRepository<TC>
{
    /// <summary>
    /// 获取所有实体的查询对象
    /// </summary>
    IQueryable<TC> All { get; }

    /// <summary>
    /// 获取所有实体的查询对象，并包含指定的相关属性
    /// </summary>
    /// <param name="includeProperties">需要包含的相关属性表达式列表</param>
    /// <returns>包含相关属性的所有实体查询对象</returns>
    IQueryable<TC> AllIncluding(params Expression<Func<TC, object>>[] includeProperties);

    /// <summary>
    /// 根据条件查询实体，并包含指定的相关属性
    /// </summary>
    /// <param name="predicate">查询条件表达式</param>
    /// <param name="includeProperties">需要包含的相关属性表达式列表</param>
    /// <returns>满足条件并包含相关属性的实体查询对象</returns>
    IQueryable<TC> FindByInclude(
        Expression<Func<TC, bool>> predicate,
        params Expression<Func<TC, object>>[] includeProperties
    );

    /// <summary>
    /// 根据条件查询实体
    /// </summary>
    /// <param name="predicate">查询条件表达式</param>
    /// <returns>满足条件的实体查询对象</returns>
    IQueryable<TC> FindBy(Expression<Func<TC, bool>> predicate);

    /// <summary>
    /// 根据主键ID查找实体
    /// </summary>
    /// <param name="id">实体主键ID</param>
    /// <returns>找到的实体对象</returns>
    TC Find(Guid id);

    /// <summary>
    /// 异步根据主键ID查找实体
    /// </summary>
    /// <param name="id">实体主键ID</param>
    /// <returns>找到的实体对象任务</returns>
    Task<TC> FindAsync(Guid id);

    /// <summary>
    /// 添加单个实体
    /// </summary>
    /// <param name="entity">要添加的实体对象</param>
    void Add(TC entity);

    /// <summary>
    /// 批量添加实体
    /// </summary>
    /// <param name="entities">要添加的实体集合</param>
    void AddRange(IEnumerable<TC> entities);

    /// <summary>
    /// 更新单个实体
    /// </summary>
    /// <param name="entity">要更新的实体对象</param>
    void Update(TC entity);

    /// <summary>
    /// 批量更新实体
    /// </summary>
    /// <param name="entities">要更新的实体集合</param>
    void UpdateRange(IEnumerable<TC> entities);

    /// <summary>
    /// 移除单个实体（标记为删除，不一定立即从数据库中删除）
    /// </summary>
    /// <param name="entity">要移除的实体对象</param>
    void Remove(TC entity);

    /// <summary>
    /// 批量移除实体（标记为删除，不一定立即从数据库中删除）
    /// </summary>
    /// <param name="entities">要移除的实体集合</param>
    void RemoveRange(IEnumerable<TC> entities);

    /// <summary>
    /// 根据主键ID删除实体
    /// </summary>
    /// <param name="id">要删除的实体主键ID</param>
    void Delete(Guid id);

    /// <summary>
    /// 删除单个实体
    /// </summary>
    /// <param name="entity">要删除的实体对象</param>
    void Delete(TC entity);

    /// <summary>
    /// 插入或更新实体图（处理实体及其相关实体的级联操作）
    /// </summary>
    /// <param name="entity">要插入或更新的实体对象</param>
    void InsertUpdateGraph(TC entity);
}
