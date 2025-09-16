using System.Security.Claims;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Entities;
using DanMu.Pan.Data.Info;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DanMu.Pan.Helper;

/// <summary>
/// 提供声明(claims)相关操作的辅助类
/// </summary>
public class ClaimsHelper
{
    /// <summary>
    /// 用户声明类型集合，包含所有有效的声明类型
    /// </summary>
    public readonly HashSet<string> UserClaimTypes =
    [
        UserClaimStr.IsFolderCreate,
        UserClaimStr.IsFileUpload,
        UserClaimStr.IsDeleteFileFolder,
        UserClaimStr.IsSharedFileFolder,
        UserClaimStr.IsSendEmail,
        UserClaimStr.IsRenameFile,
        UserClaimStr.IsDownloadFile,
        UserClaimStr.IsCopyFile,
        UserClaimStr.IsCopyFolder,
        UserClaimStr.IsMoveFile,
        UserClaimStr.IsSharedLink,
    ];

    /// <summary>
    /// 为指定用户添加声明
    /// </summary>
    /// <param name="userManager">用户管理器</param>
    /// <param name="user">要添加声明的用户</param>
    /// <param name="userClaim">用户声明数据传输对象</param>
    /// <param name="logger">日志记录器</param>
    /// <returns>异步任务</returns>
    public async Task AddUserClaim(
        UserManager<User> userManager,
        User user,
        UserClaimDto userClaim,
        ILogger logger
    )
    {
        try
        {
            await userManager.AddClaimsAsync(
                user,
                new List<Claim>
                {
                    new(UserClaimStr.IsFolderCreate, userClaim.IsFolderCreate.ToString()),
                    new(UserClaimStr.IsFileUpload, userClaim.IsFileUpload.ToString()),
                    new(UserClaimStr.IsDeleteFileFolder, userClaim.IsDeleteFileFolder.ToString()),
                    new(UserClaimStr.IsSharedFileFolder, userClaim.IsSharedFileFolder.ToString()),
                    new(UserClaimStr.IsSendEmail, userClaim.IsSendEmail.ToString()),
                    new(UserClaimStr.IsRenameFile, userClaim.IsRenameFile.ToString()),
                    new(UserClaimStr.IsDownloadFile, userClaim.IsDownloadFile.ToString()),
                    new(UserClaimStr.IsCopyFile, userClaim.IsCopyFile.ToString()),
                    new(UserClaimStr.IsCopyFolder, userClaim.IsCopyFolder.ToString()),
                    new(UserClaimStr.IsMoveFile, userClaim.IsMoveFile.ToString()),
                    new(UserClaimStr.IsSharedLink, userClaim.IsSharedLink.ToString()),
                }
            );
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
    }

    /// <summary>
    /// 从声明列表中提取用户声明信息
    /// </summary>
    /// <param name="claimList">声明列表</param>
    /// <returns>用户声明数据传输对象</returns>
    public UserClaimDto GetUserClaims(IList<Claim> claimList)
    {
        var userClaim = new UserClaimDto();
        foreach (var claim in claimList)
        {
            switch (claim.Type)
            {
                case UserClaimStr.IsFolderCreate:
                    userClaim.IsFolderCreate = claim.Value.Equals(
                        true.ToString(),
                        StringComparison.CurrentCultureIgnoreCase
                    );
                    break;
                case UserClaimStr.IsFileUpload:
                    userClaim.IsFileUpload = claim.Value.Equals(
                        true.ToString(),
                        StringComparison.CurrentCultureIgnoreCase
                    );
                    break;
                case UserClaimStr.IsDeleteFileFolder:
                    userClaim.IsDeleteFileFolder = claim.Value.Equals(
                        true.ToString(),
                        StringComparison.CurrentCultureIgnoreCase
                    );
                    break;
                case UserClaimStr.IsSharedFileFolder:
                    userClaim.IsSharedFileFolder = claim.Value.Equals(
                        true.ToString(),
                        StringComparison.CurrentCultureIgnoreCase
                    );
                    break;
                case UserClaimStr.IsSendEmail:
                    userClaim.IsSendEmail = claim.Value.Equals(
                        true.ToString(),
                        StringComparison.CurrentCultureIgnoreCase
                    );
                    break;
                case UserClaimStr.IsRenameFile:
                    userClaim.IsRenameFile = claim.Value.Equals(
                        true.ToString(),
                        StringComparison.CurrentCultureIgnoreCase
                    );
                    break;
                case UserClaimStr.IsDownloadFile:
                    userClaim.IsDownloadFile = claim.Value.Equals(
                        true.ToString(),
                        StringComparison.CurrentCultureIgnoreCase
                    );
                    break;
                case UserClaimStr.IsCopyFile:
                    userClaim.IsCopyFile = claim.Value.Equals(
                        true.ToString(),
                        StringComparison.CurrentCultureIgnoreCase
                    );
                    break;
                case UserClaimStr.IsCopyFolder:
                    userClaim.IsCopyFolder = claim.Value.Equals(
                        true.ToString(),
                        StringComparison.CurrentCultureIgnoreCase
                    );
                    break;
                case UserClaimStr.IsMoveFile:
                    userClaim.IsMoveFile = claim.Value.Equals(
                        true.ToString(),
                        StringComparison.CurrentCultureIgnoreCase
                    );
                    break;
                case UserClaimStr.IsSharedLink:
                    userClaim.IsSharedLink = claim.Value.Equals(
                        true.ToString(),
                        StringComparison.CurrentCultureIgnoreCase
                    );
                    break;
            }
        }
        return userClaim;
    }

    /// <summary>
    /// 从声明列表中筛选出有效的声明
    /// </summary>
    /// <param name="claimList">声明列表</param>
    /// <param name="validClaimTypes">有效声明类型集合</param>
    /// <returns>有效的声明数据传输对象列表</returns>
    public List<AppClaimDto> GetValidClaims(IList<Claim> claimList, HashSet<string> validClaimTypes)
    {
        List<AppClaimDto> appClaimDtoList = [];
        appClaimDtoList.AddRange(
            from claim in claimList
            where validClaimTypes.Contains(claim.Type)
            select new AppClaimDto() { ClaimType = claim.Type, ClaimValue = claim.Value }
        );
        return appClaimDtoList;
    }
}
