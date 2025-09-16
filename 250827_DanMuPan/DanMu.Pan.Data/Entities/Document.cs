using DanMu.Pan.Data.Entities.Base;

namespace DanMu.Pan.Data.Entities;

/// <summary>
/// 文档实体类，用于表示网盘系统中的文件文档信息
/// 文档实体跟踪关键信息，包括用于完整性检查的文件 MD5 哈希值、文件名、扩展名、大小和存储路径
/// 它还维护与所属物理文件夹、创建或修改文档的用户的关系，以及收藏文档、已删除文档和共享用户的集合
/// </summary>
public class Document : BaseEntity
{
    /// <summary>
    /// 文档唯一标识符
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 文件的MD5哈希值，用于完整性校验和重复文件检测
    /// </summary>
    public string Md5 { get; set; }

    /// <summary>
    /// 文档名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 物理文件夹的唯一标识符，关联文档所在的文件夹
    /// </summary>
    public Guid PhysicalFolderId { get; set; }

    /// <summary>
    /// 物理文件夹对象，表示文档所在的文件夹实体
    /// </summary>
    public PhysicalFolder PhysicalFolder { get; set; }

    /// <summary>
    /// 文件扩展名，例如 .txt, .pdf, .jpg 等
    /// </summary>
    public string Extension { get; set; }

    /// <summary>
    /// 文件在系统中的完整存储路径
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// 文件大小（以字节为单位）
    /// </summary>
    public long Size { get; set; }

    /// <summary>
    /// 缩略图路径，用于图片或视频文件的预览图
    /// </summary>
    public string ThumbnailPath { get; set; }

    /// <summary>
    /// 创建该文档的用户信息
    /// </summary>
    public User CreatedByUser { get; set; }

    /// <summary>
    /// 删除该文档的用户信息
    /// </summary>
    public User DeletedByUser { get; set; }

    /// <summary>
    /// 最后修改该文档的用户信息
    /// </summary>
    public User ModifiedByUser { get; set; }

    /// <summary>
    /// 收藏该文档的用户列表
    /// </summary>
    public List<DocumentStarred> DocumentStarreds { get; set; } = [];

    /// <summary>
    /// 文档删除记录列表
    /// </summary>
    public List<DocumentDeleted> DocumentDeleteds { get; set; } = [];

    /// <summary>
    /// 共享给其他用户的文档列表
    /// </summary>
    public ICollection<SharedDocumentUser> SharedDocumentUsers { get; set; } = [];

    /// <summary>
    /// 文档的原始存储路径, 用于恢复文档, 不需要存储
    /// </summary>
    public string OriginalPath { get; set; }

    /// <summary>
    /// 文档的原始缩略图路径, 用于恢复文档, 不需要存储
    /// </summary>
    public string OriginalThumbnailPath { get; set; }
}
