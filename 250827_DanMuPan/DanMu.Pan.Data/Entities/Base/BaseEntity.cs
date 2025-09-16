using System.ComponentModel.DataAnnotations.Schema;
using DanMu.Pan.Data.Enum;
using DanMu.Pan.Data.Info;

namespace DanMu.Pan.Data.Entities.Base;

/// <summary>
/// 实体基类，包含所有实体共有的基本属性
/// </summary>
public abstract class BaseEntity
{
    private DateTime _createdDate;

    /// <summary>
    /// 获取或设置实体创建时间
    /// </summary>
    [Column(TypeName = PostgresqlStr.ColumnTimeName)]
    public DateTime CreatedDate
    {
        get => _createdDate.ToLocalTime();
        set => _createdDate = value.ToLocalTime();
    }

    /// <summary>
    /// 获取或设置创建实体的用户标识
    /// </summary>
    public Guid CreatedBy { get; set; }

    private DateTime _modifiedDate;

    /// <summary>
    /// 获取或设置实体最后修改时间
    /// </summary>
    [Column(TypeName = PostgresqlStr.ColumnTimeName)]
    public DateTime ModifiedDate
    {
        get => _modifiedDate.ToLocalTime();
        set => _modifiedDate = value.ToLocalTime();
    }

    /// <summary>
    /// 获取或设置最后修改实体的用户标识
    /// </summary>
    public Guid ModifiedBy { get; set; }

    private DateTime? _deletedDate;

    /// <summary>
    /// 获取或设置实体删除时间
    /// </summary>
    [Column(TypeName = PostgresqlStr.ColumnTimeName)]
    public DateTime? DeletedDate
    {
        get => _deletedDate?.ToLocalTime();
        set => _deletedDate = value?.ToLocalTime();
    }

    /// <summary>
    /// 获取或设置删除实体的用户标识
    /// </summary>
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// 获取或设置实体状态（未映射到数据库）
    /// </summary>
    [NotMapped]
    public ObjectState ObjectState { get; set; }

    /// <summary>
    /// 获取或设置实体是否已被删除
    /// </summary>
    public bool IsDeleted { get; set; } = false;
}
