namespace DanMu.Pan.Data.Entities;

/// <summary>
/// 物理文件夹与用户关联实体类，用于建立文件夹和用户之间的多对多关系
/// 通过FolderId和UserId的组合键来表示某个用户对某个文件夹的访问权限
/// </summary>
public class PhysicalFolderUser
{
    /// <summary>
    /// 文件夹唯一标识符，关联到PhysicalFolder实体
    /// </summary>
    public Guid FolderId { get; set; }

    /// <summary>
    /// 用户唯一标识符，关联到User实体
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// 关联的物理文件夹对象
    /// </summary>
    public PhysicalFolder PhysicalFolder { get; set; }

    /// <summary>
    /// 关联的用户对象
    /// </summary>
    public User User { get; set; }
}
