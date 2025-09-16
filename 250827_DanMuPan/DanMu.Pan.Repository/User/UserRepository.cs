using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DanMu.Pan.Data.Dto;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Data.Info;
using DanMu.Pan.Data.Resources;
using DanMu.Pan.Domain.Context;
using DanMu.Pan.Helper;
using DanMu.Pan.Repository.GenericRepository;
using DanMu.Pan.Repository.PropertyMapping;
using DanMu.Pan.Repository.UnitOfWork;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

// TODO : 未完成注释 - JwtSettings - IPropertyMappingService

namespace DanMu.Pan.Repository.User;

public class UserRepository(
    IUnitOfWork<DocumentContext> unitOfWork,
    IPropertyMappingService propertyMappingService,
    UserInfoToken userInfo,
    JwtSetting setting,
    PathHelper pathHelper,
    ClaimsHelper claimsHelper
) : GenericRepository<DanMu.Pan.Data.Entities.User, DocumentContext>(unitOfWork), IUserRepository
{
    private string BuildJwtToken(UserAuthDto authUser, Guid Id, IList<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Key));
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, Id.ToString())); // 这是JWT标准中定义的声明名称，代表"Subject"（主题），用于标识令牌的主题（通常是用户ID）
        claims.Add(new Claim(nameof(UserAuthDto.Email), authUser.Email));
        var token = new JwtSecurityToken(
            issuer: setting.Issuer, // 签发者
            audience: setting.Audience, // 目标接收方
            claims: claims, // 包含的声明
            notBefore: DateTime.UtcNow, // 生效时间 在此时间之前，令牌被认为是无效的
            expires: DateTime.Now.AddMinutes(setting.MinutesToExpiration), // 过期时间
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256) // 签名凭据，用于验证令牌的完整性
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<UserList> GetUsersAsync(UserResource userResource)
    {
        var collectionBeforePaging = All;
        collectionBeforePaging = collectionBeforePaging.ApplyShort(
            userResource.OrderBy,
            propertyMappingService.GetPropertyMapping<UserDto, Data.Entities.User>()
        ); // 根据userResource.OrderBy指定的字段进行排序，并通过属性映射服务确保DTO和实体之间的属性映射正确。

        if (!string.IsNullOrWhiteSpace(userResource.FirstName))
        {
            collectionBeforePaging = collectionBeforePaging.Where(user =>
                user.FirstName.Contains(userResource.FirstName)
                || user.LastName.Contains(userResource.FirstName)
            );
        }
        if (!string.IsNullOrWhiteSpace(userResource.LastName))
        {
            collectionBeforePaging = collectionBeforePaging.Where(user =>
                user.LastName.Contains(userResource.LastName)
            );
        }
        if (!string.IsNullOrWhiteSpace(userResource.Email))
        {
            collectionBeforePaging = collectionBeforePaging.Where(user =>
                user.Email.Contains(userResource.Email)
            );
        }
        if (!string.IsNullOrWhiteSpace(userResource.PhoneNumber))
        {
            collectionBeforePaging = collectionBeforePaging.Where(user =>
                user.PhoneNumber.Contains(userResource.PhoneNumber)
            );
        }

        var isActive = userResource.IsActive ?? false;
        collectionBeforePaging = collectionBeforePaging.Where(c => c.IsActive == isActive);

        var loginAudits = new UserList(pathHelper);
        return await loginAudits.Create(
            collectionBeforePaging,
            userResource.Skip,
            userResource.PageSize
        );
    }

    public UserAuthDto BuildUserAuthObject(Data.Entities.User appUser, IList<Claim> claims)
    {
        var userAuthDto = new UserAuthDto
        {
            Id = appUser.Id,
            UserName = appUser.UserName,
            FirstName = appUser.FirstName,
            LastName = appUser.LastName,
            Email = appUser.Email,
            PhoneNumber = appUser.PhoneNumber,
            IsAuthenticated = true,
            ProfilePhoto = appUser.ProfilePhoto,
            IsAdmin = appUser.IsAdmin,
            Claims = claimsHelper.GetValidClaims(claims, claimsHelper.UserClaimTypes),
        };
        userAuthDto.BearerToken = BuildJwtToken(userAuthDto, appUser.Id, claims);
        return userAuthDto;
    }

    public Task<UserList> GetSharedUsers(
        UserResource userResource,
        List<Guid> folderUsers,
        List<Guid> documentUsers
    )
    {
        throw new NotImplementedException();
    }
}
