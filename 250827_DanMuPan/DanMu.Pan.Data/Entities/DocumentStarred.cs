namespace DanMu.Pan.Data.Entities;

/// <summary>
/// 文档收藏实体类，用于表示用户收藏的文档信息
/// 通过DocumentId和UserId的组合建立文档和用户之间的收藏关系
/// </summary>
public class DocumentStarred
{
    /// <summary>
    /// 被收藏文档的唯一标识符，关联到Document实体
    /// </summary>
    public Guid DocumentId { get; set; }

    /// <summary>
    /// 关联的文档对象
    /// </summary>
    public Document Document { get; set; }

    /// <summary>
    /// 收藏文档的用户唯一标识符，关联到User实体
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 关联的用户对象
    /// </summary>
    public User User { get; set; }
}
