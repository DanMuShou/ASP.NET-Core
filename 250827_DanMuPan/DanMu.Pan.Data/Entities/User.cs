using Microsoft.AspNetCore.Identity;

namespace DanMu.Pan.Data.Entities;

/// <summary>
/// 用户实体类，用于表示系统中的用户信息
/// 包含用户的基本信息、状态信息、审计信息以及关联的文件夹信息
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// 用户名字
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    /// 用户姓氏
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// 用户是否已被删除的标记
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 用户账户是否处于激活状态
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 用户头像图片路径
    /// </summary>
    public string ProfilePhoto { get; set; }

    /// <summary>
    /// 用户账户提供商信息
    /// </summary>
    public string? Provider { get; set; }

    /// <summary>
    /// 用户地址信息
    /// </summary>
    public string Address { get; set; }

    /// <summary>
    /// 用户账户创建时间
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// 创建该用户账户的用户标识符
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    /// 用户账户最后修改时间
    /// </summary>
    public DateTime ModifiedDate { get; set; }

    /// <summary>
    /// 最后修改该用户账户的用户标识符
    /// </summary>
    public Guid? ModifiedBy { get; set; }

    /// <summary>
    /// 用户账户删除时间（如果已被删除）
    /// </summary>
    public DateTime? DeletedDate { get; set; }

    /// <summary>
    /// 删除该用户账户的用户标识符
    /// </summary>
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// 用户关联的物理文件夹集合
    /// </summary>
    public ICollection<PhysicalFolderUser> Folders { get; set; } = [];

    /// <summary>
    /// 用户是否为管理员的标记
    /// </summary>
    public bool IsAdmin { get; set; }
}
