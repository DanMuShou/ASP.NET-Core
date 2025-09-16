using System.Linq.Expressions;
using DanMu.Pan.Data.Entities.Base;
using DanMu.Pan.Repository.UnitOfWork;
using Microsoft.EntityFrameworkCore;

// TODO : 拓展异步方法

namespace DanMu.Pan.Repository.GenericRepository;

/// <summary>
/// 泛型仓储实现类，提供对实体的基本数据操作
/// </summary>
/// <typeparam name="TC">实体类型，必须是引用类型</typeparam>
/// <typeparam name="TContext">数据库上下文类型，必须继承自DbContext</typeparam>
/// <param name="unitOfWork">工作单元实例</param>
public class GenericRepository<TC, TContext> : IGenericRepository<TC>
    where TC : class
    where TContext : DbContext
{
    public GenericRepository(IUnitOfWork<TContext> unitOfWork)
    {
        UnitOfWork = unitOfWork;
        Context = unitOfWork.Context;
        DbSet = Context.Set<TC>(); // 它返回一个 DbSet<TC> 对象，用于对数据库中 TC 类型的实体进行操作
    }

    protected readonly TContext Context;
    protected IUnitOfWork<TContext> UnitOfWork;
    internal readonly DbSet<TC> DbSet; // 只能在定义它的程序集内部访问

    /// <summary>
    /// 获取所有实体的查询对象
    /// </summary>
    public IQueryable<TC> All => Context.Set<TC>(); //IQueryable 是一个接口，继承自 IEnumerable，专门用于构建可以转换为其他查询语言（如 SQL）的表达式树。

    /// <summary>
    /// 获取所有实体的查询对象，并包含指定的相关属性
    /// </summary>
    /// <param name="includeProperties">需要包含的相关属性表达式列表</param>
    /// <returns>包含相关属性的所有实体查询对象</returns>
    private IQueryable<TC> GetAllIncluding(params Expression<Func<TC, object>>[] includeProperties)
    {
        var queryable = DbSet.AsNoTracking(); // 创建一个不被 ChangeTracker 跟踪的查询 适用于只读场景，可以提高查询性能
        return includeProperties.Aggregate(
            queryable,
            (current, includeProperty) => current.Include(includeProperty)
        ); // Aggregate：这是 LINQ 中的一个方法，用于对集合中的元素进行累积操作
    }

    /// <summary>
    /// 获取所有实体的查询对象，并包含指定的相关属性
    /// </summary>
    /// <param name="includeProperties">需要包含的相关属性表达式列表</param>
    /// <returns>包含相关属性的所有实体查询对象</returns>
    public IQueryable<TC> AllIncluding(params Expression<Func<TC, object>>[] includeProperties) =>
        GetAllIncluding(includeProperties);

    /// <summary>
    /// 根据条件查询实体，并包含指定的相关属性
    /// </summary>
    /// <param name="predicate">查询条件表达式</param>
    /// <param name="includeProperties">需要包含的相关属性表达式列表</param>
    /// <returns>满足条件并包含相关属性的实体查询对象</returns>
    public IQueryable<TC> FindByInclude(
        Expression<Func<TC, bool>> predicate,
        params Expression<Func<TC, object>>[] includeProperties
    )
    {
        var query = GetAllIncluding();
        return query.Where(predicate);
    }

    /// <summary>
    /// 根据条件查询实体
    /// </summary>
    /// <param name="predicate">查询条件表达式</param>
    /// <returns>满足条件的实体查询对象</returns>
    public IQueryable<TC> FindBy(Expression<Func<TC, bool>> predicate)
    {
        var query = DbSet.AsNoTracking();
        return query.Where(predicate);
    }

    /// <summary>
    /// 根据主键ID查找实体
    /// </summary>
    /// <param name="id">实体主键ID</param>
    /// <returns>找到的实体对象</returns>
    public TC Find(Guid id) => DbSet.Find(id);

    /// <summary>
    /// 异步根据主键ID查找实体
    /// </summary>
    /// <param name="id">实体主键ID</param>
    /// <returns>找到的实体对象任务</returns>
    public async Task<TC> FindAsync(Guid id) => await DbSet.FindAsync(id);

    /// <summary>
    /// 添加单个实体
    /// </summary>
    /// <param name="entity">要添加的实体对象</param>
    public void Add(TC entity) => Context.Add(entity);

    /// <summary>
    /// 批量添加实体
    /// </summary>
    /// <param name="entities">要添加的实体集合</param>
    public void AddRange(IEnumerable<TC> entities) => Context.AddRange(entities);

    /// <summary>
    /// 更新单个实体
    /// </summary>
    /// <param name="entity">要更新的实体对象</param>
    public void Update(TC entity) => Context.Update(entity);

    /// <summary>
    /// 批量更新实体
    /// </summary>
    /// <param name="entities">要更新的实体集合</param>
    public void UpdateRange(IEnumerable<TC> entities) => Context.UpdateRange(entities);

    /// <summary>
    /// 移除单个实体（标记为删除，不一定立即从数据库中删除）
    /// </summary>
    /// <param name="entity">要移除的实体对象</param>
    public void Remove(TC entity) => DbSet.Remove(entity);

    /// <summary>
    /// 批量移除实体（标记为删除，不一定立即从数据库中删除）
    /// </summary>
    /// <param name="entities">要移除的实体集合</param>
    public void RemoveRange(IEnumerable<TC> entities) => DbSet.RemoveRange(entities);

    /// <summary>
    /// 根据主键ID删除实体（软删除）
    /// </summary>
    /// <param name="id">要删除的实体主键ID</param>
    public void Delete(Guid id)
    {
        if (DbSet.Find(id) is not BaseEntity entity)
            return;

        entity.IsDeleted = true;
        Context.Update(entity);
    }

    /// <summary>
    /// 删除单个实体（软删除）
    /// </summary>
    /// <param name="entity">要删除的实体对象</param>
    public void Delete(TC entity)
    {
        if (entity is not BaseEntity baseEntity)
            return;
        baseEntity.IsDeleted = true;
        Context.Update(entity);
    }

    /// <summary>
    /// 插入或更新实体图（处理实体及其相关实体的级联操作）
    /// </summary>
    /// <param name="entity">要插入或更新的实体对象</param>
    public void InsertUpdateGraph(TC entity) => DbSet.Add(entity);

    /// <summary>
    /// 释放数据库上下文资源
    /// </summary>
    public void Dispose() => Context.Dispose();
}
