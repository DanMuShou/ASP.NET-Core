namespace DanMu.Pan.Data.Entities;

/// <summary>
/// 共享文档用户实体类，用于表示文档共享关系
/// 通过DocumentId和UserId的组合建立文档和用户之间的共享关系
/// </summary>
public class SharedDocumentUser
{
    /// <summary>
    /// 被共享文档的唯一标识符，关联到Document实体
    /// </summary>
    public Guid DocumentId { get; set; }

    /// <summary>
    /// 被共享给的用户唯一标识符，关联到User实体
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
