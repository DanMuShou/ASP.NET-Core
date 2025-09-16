using DanMu.Pan.Data.Entities.Base;

namespace DanMu.Pan.Data.Entities;

/// <summary>
/// 文档版本实体类，用于表示文档的不同版本信息
/// 包含文档版本的路径、大小、修改信息等版本控制相关数据
/// </summary>
public class DocumentVersion : BaseEntity
{
    /// <summary>
    /// 文档版本的唯一标识符
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 关联文档的唯一标识符
    /// </summary>
    public Guid DocumentId { get; set; }

    /// <summary>
    /// 文档版本存储路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 版本变更信息或注释
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 文档版本大小（字节）
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// 关联的文档对象
    /// </summary>
    public Document Document { get; set; }

    /// <summary>
    /// 创建该版本的用户对象
    /// </summary>
    public User CreatedByUser { get; set; }

    /// <summary>
    /// 最后修改该版本的用户对象
    /// </summary>
    public User ModifiedByUser { get; set; }

    /// <summary>
    /// 删除该版本的用户对象
    /// </summary>
    public User DeletedByUser { get; set; }
}
