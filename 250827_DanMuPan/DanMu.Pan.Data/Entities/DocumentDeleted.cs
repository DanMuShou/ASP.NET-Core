using DanMu.Pan.Data.Entities.Base;

namespace DanMu.Pan.Data.Entities;

/// <summary>
/// 文档删除记录实体类，用于跟踪已被删除的文档信息
/// 包含文档删除的相关信息，如删除的文档、执行删除操作的用户以及文档的创建和修改用户信息
/// </summary>
public class DocumentDeleted : BaseEntity
{
    /// <summary>
    /// 被删除文档的唯一标识符，关联到Document实体
    /// </summary>
    public Guid DocumentId { get; set; }

    /// <summary>
    /// 与删除文档关联的用户唯一标识符
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 关联的文档对象
    /// </summary>
    public Document Document { get; set; }

    /// <summary>
    /// 关联的用户对象
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// 创建该文档的用户信息
    /// </summary>
    public User CreatedByUser { get; set; }

    /// <summary>
    /// 最后修改该文档的用户信息
    /// </summary>
    public User ModifiedByUser { get; set; }

    /// <summary>
    /// 执行删除操作的用户信息
    /// </summary>
    public User DeletedByUser { get; set; }
}
