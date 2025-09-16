using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

// OK

namespace DanMu.Pan.Repository.UnitOfWork;

/// <summary>
/// 工作单元实现类，用于管理数据库上下文和事务操作
/// </summary>
/// <typeparam name="TContext">数据库上下文类型，必须继承自DbContext</typeparam>
/// <param name="context">数据库上下文实例</param>
/// <param name="logger">日志记录器实例</param>
public class UnitOfWork<TContext>(
    TContext context,
    ILogger<UnitOfWork<TContext>> logger,
    UserInfoToken userInfoToken
) : IUnitOfWork<TContext>
    where TContext : DbContext
{
    /// <summary>
    /// 获取数据库上下文实例
    /// </summary>
    public TContext Context => context;

    /// <summary>
    /// 设置实体的修改信息，包括创建时间、修改时间等
    /// </summary>
    private void SetModifiedInformation()
    {
        foreach (var entry in context.ChangeTracker.Entries<BaseEntity>()) // ChangeTracker 负责跟踪所有被加载到上下文中的实体 Entries<BaseEntity>()：
        { // 返回所有类型为 BaseEntity 或其派生类的实体条目
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedDate =
                        entry.Entity.CreatedDate == DateTime.MinValue.ToLocalTime()
                            ? DateTime.UtcNow
                            : entry.Entity.CreatedDate;
                    entry.Entity.CreatedBy =
                        entry.Entity.CreatedBy == Guid.Empty
                            ? userInfoToken.Id
                            : entry.Entity.CreatedBy;
                    entry.Entity.ModifiedDate =
                        entry.Entity.ModifiedDate == DateTime.MinValue.ToLocalTime()
                            ? DateTime.UtcNow
                            : entry.Entity.ModifiedDate;
                    entry.Entity.ModifiedBy =
                        entry.Entity.ModifiedBy == Guid.Empty
                            ? userInfoToken.Id
                            : entry.Entity.ModifiedBy;
                    break;
                case EntityState.Modified:
                    if (entry.Entity.IsDeleted)
                    {
                        entry.Entity.DeletedDate = DateTime.UtcNow;
                        entry.Entity.DeletedBy = userInfoToken.Id;
                    }
                    else
                    {
                        entry.Entity.ModifiedDate = DateTime.UtcNow;
                        entry.Entity.ModifiedBy = userInfoToken.Id;
                    }
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                    logger.LogInformation("EntityState is other");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    /// <summary>
    /// 同步保存所有更改到数据库，并在事务中执行
    /// </summary>
    /// <returns>受影响的行数，如果发生异常则返回0</returns>
    public int Save()
    {
        using var transaction = context.Database.BeginTransaction(); // 开启数据库事务
        try
        {
            SetModifiedInformation();
            var retValue = context.SaveChanges();
            transaction.Commit();
            return retValue;
        }
        catch (Exception ex)
        {
            transaction.Rollback(); // 如果发生异常，则回滚事务并记录错误日志，返回0
            logger.LogError(ex, ex.Message);
            return 0;
        }
    }

    /// <summary>
    /// 异步保存所有更改到数据库，并在事务中执行
    /// </summary>
    /// <returns>受影响的行数的任务，如果发生异常则返回0</returns>
    public async Task<int> SaveAsync()
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            SetModifiedInformation();
            var retValue = await context.SaveChangesAsync();
            await transaction.CommitAsync();
            return retValue;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, ex.Message);
            return 0;
        }
    }
}
