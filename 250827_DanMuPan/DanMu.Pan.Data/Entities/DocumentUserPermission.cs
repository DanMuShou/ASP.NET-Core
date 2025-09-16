using DanMu.Pan.Data.Entities.Base;

namespace DanMu.Pan.Data.Entities;

/// <summary>
/// 文档用户权限实体类，用于表示文档和用户之间的权限关系
/// 通过DocumentId和UserId的组合建立文档和用户之间的权限关联
/// </summary>
public class DocumentUserPermission : BaseEntity
{
    /// <summary>
    /// 关联文档的唯一标识符
    /// </summary>
    public Guid DocumentId { get; set; }

    /// <summary>
    /// 关联用户的唯一标识符
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
}
