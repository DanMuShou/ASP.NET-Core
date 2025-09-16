using Microsoft.EntityFrameworkCore;

namespace DanMu.Pan.Repository.UnitOfWork;

/// <summary>
/// 工作单元接口，用于管理数据库上下文和事务操作
/// </summary>
/// <typeparam name="TContext">数据库上下文类型，必须继承自DbContext</typeparam>
public interface IUnitOfWork<TContext>
    where TContext : DbContext
{
    /// <summary>
    /// 获取数据库上下文实例
    /// </summary>
    TContext Context { get; }

    /// <summary>
    /// 同步保存所有更改到数据库
    /// </summary>
    /// <returns>受影响的行数</returns>
    int Save();

    /// <summary>
    /// 异步保存所有更改到数据库
    /// </summary>
    /// <returns>受影响的行数的任务</returns>
    Task<int> SaveAsync();
}
