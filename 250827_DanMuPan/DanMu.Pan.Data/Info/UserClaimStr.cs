namespace DanMu.Pan.Data.Info;

/// <summary>
/// 用户声明字符串常量类，定义了系统中用户权限相关的声明名称
/// </summary>
public class UserClaimStr
{
    /// <summary>
    /// 创建文件夹权限声明
    /// </summary>
    public const string IsFolderCreate = "IsFolderCreate";

    /// <summary>
    /// 文件上传权限声明
    /// </summary>
    public const string IsFileUpload = "IsFileUpload";

    /// <summary>
    /// 删除文件或文件夹权限声明
    /// </summary>
    public const string IsDeleteFileFolder = "IsDeleteFileFolder";

    /// <summary>
    /// 共享文件或文件夹权限声明
    /// </summary>
    public const string IsSharedFileFolder = "IsSharedFileFolder";

    /// <summary>
    /// 发送邮件权限声明
    /// </summary>
    public const string IsSendEmail = "IsSendEmail";

    /// <summary>
    /// 重命名文件权限声明
    /// </summary>
    public const string IsRenameFile = "IsRenameFile";

    /// <summary>
    /// 下载文件权限声明
    /// </summary>
    public const string IsDownloadFile = "IsDownloadFile";

    /// <summary>
    /// 复制文件权限声明
    /// </summary>
    public const string IsCopyFile = "IsCopyFile";

    /// <summary>
    /// 复制文件夹权限声明
    /// </summary>
    public const string IsCopyFolder = "IsCopyFolder";

    /// <summary>
    /// 移动文件权限声明
    /// </summary>
    public const string IsMoveFile = "IsMoveFile";

    /// <summary>
    /// 创建共享链接权限声明
    /// </summary>
    public const string IsSharedLink = "IsSharedLink";
}
