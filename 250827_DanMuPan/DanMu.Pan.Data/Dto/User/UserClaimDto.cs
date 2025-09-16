namespace DanMu.Pan.Data.Dto.User;

// OK

/// <summary>
/// 用户权限数据传输对象
/// 用于定义用户在文件管理系统中的各项操作权限
/// </summary>
public class UserClaimDto
{
    /// <summary>
    /// 是否有创建文件夹权限
    /// </summary>
    public bool IsFolderCreate { get; set; } = false;

    /// <summary>
    /// 是否有文件上传权限
    /// </summary>
    public bool IsFileUpload { get; set; } = false;

    /// <summary>
    /// 是否有删除文件和文件夹权限
    /// </summary>
    public bool IsDeleteFileFolder { get; set; } = false;

    /// <summary>
    /// 是否有共享文件和文件夹权限
    /// </summary>
    public bool IsSharedFileFolder { get; set; } = false;

    /// <summary>
    /// 是否有发送邮件权限
    /// </summary>
    public bool IsSendEmail { get; set; } = false;

    /// <summary>
    /// 是否有文件重命名权限
    /// </summary>
    public bool IsRenameFile { get; set; } = false;

    /// <summary>
    /// 是否有文件下载权限
    /// </summary>
    public bool IsDownloadFile { get; set; } = false;

    /// <summary>
    /// 是否有复制文件权限
    /// </summary>
    public bool IsCopyFile { get; set; } = false;

    /// <summary>
    /// 是否有复制文件夹权限
    /// </summary>
    public bool IsCopyFolder { get; set; } = false;

    /// <summary>
    /// 是否有移动文件权限
    /// </summary>
    public bool IsMoveFile { get; set; } = false;

    /// <summary>
    /// 是否有创建共享链接权限
    /// </summary>
    public bool IsSharedLink { get; set; } = false;
}
