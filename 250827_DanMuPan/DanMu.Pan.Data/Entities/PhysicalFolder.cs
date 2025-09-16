using System.ComponentModel.DataAnnotations.Schema;
using DanMu.Pan.Data.Entities.Base;

namespace DanMu.Pan.Data.Entities;

/// <summary>
/// 物理文件夹实体类，用于表示网盘系统中的文件夹信息
/// 包含文件夹的基本属性、层级关系以及与用户关联的信息
/// </summary>
public class PhysicalFolder : BaseEntity
{
    /// <summary>
    /// 文件夹唯一标识符
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 文件夹显示名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 系统文件夹名称，使用长整型表示系统内部的文件夹标识
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long SystemFolderName { get; set; }

    /// <summary>
    /// 父文件夹的唯一标识符，可为空（表示根目录）
    /// </summary>
    public Guid? ParentId { get; set; }

    /// <summary>
    /// 父文件夹对象引用，表示当前文件夹的上级目录
    /// </summary>
    public PhysicalFolder Parent { get; set; }

    /// <summary>
    /// 文件夹大小信息
    /// </summary>
    public string Size { get; set; }

    /// <summary>
    /// 子文件夹集合，表示当前文件夹包含的所有子文件夹
    /// </summary>
    public ICollection<PhysicalFolder> Children { get; set; } = [];

    /// <summary>
    /// 文件夹与用户关联信息列表，记录哪些用户拥有此文件夹的访问权限
    /// </summary>
    public List<PhysicalFolderUser> PhysicalFolderUsers { get; set; } = [];
}
