using Microsoft.Extensions.Configuration;

namespace DanMu.Pan.Helper;

// OK

/// <summary>
/// 路径帮助类，用于从配置中获取各种路径和相关配置信息
/// </summary>
/// <param name="configuration">应用程序配置对象，用于读取配置项</param>
/// <param name="contentPath">内容路径参数</param>
public class PathHelper(IConfiguration configuration, string contentPath)
{
    private string _contentPath = contentPath;

    /// <summary>
    /// 获取文档存储路径
    /// </summary>
    public string DocumentPath => configuration["DocumentPath"];

    /// <summary>
    /// 获取加密密钥
    /// </summary>
    public string EncryptionKey => configuration["EncryptionKey"];

    /// <summary>
    /// 获取用户配置文件路径
    /// </summary>
    public string UserProfilePath => configuration["UserProfilePath"];

    /// <summary>
    /// 获取可执行文件类型列表
    /// </summary>
    public List<string> ExecutableFileTypes
    {
        get
        {
            try
            {
                return configuration["ExecutableFileTypes"].Split(",").ToList();
            }
            catch
            {
                return [];
            }
        }
    }

    /// <summary>
    /// 获取默认用户图像路径
    /// </summary>
    public string DefaultUserImage => configuration["DefaultUserImage"];

    /// <summary>
    /// 获取Web应用程序URL数组
    /// </summary>
    public string[] WebApplicationUrl =>
        string.IsNullOrEmpty(configuration["WebApplicationUrl"])
            ? []
            : configuration["WebApplicationUrl"].Split(",");

    /// <summary>
    /// 获取内容根路径
    /// </summary>
    public string ContentRootPath
    {
        get
        {
            var contentRootPath = configuration["ContentRootPath"];
            _contentPath = string.IsNullOrEmpty(contentRootPath) ? "wwwroot" : contentRootPath;
            return _contentPath;
        }
    }
}
