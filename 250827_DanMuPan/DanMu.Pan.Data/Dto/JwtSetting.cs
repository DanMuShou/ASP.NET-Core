// OK

namespace DanMu.Pan.Data.Dto;

/// <summary>
/// JWT配置设置类，用于存储JSON Web Token相关的配置信息
/// </summary>
public class JwtSetting
{
    /// <summary>
    /// JWT签名密钥，用于令牌的签名和验证
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// JWT签发者，标识令牌的签发方
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// JWT受众，指定令牌的目标接收方
    /// </summary>
    public string Audience { get; set; }

    /// <summary>
    /// 令牌过期时间（分钟），设置令牌在多少分钟后过期
    /// </summary>
    public int MinutesToExpiration { get; set; }
}
