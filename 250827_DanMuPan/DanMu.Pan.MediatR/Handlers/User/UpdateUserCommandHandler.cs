using AutoMapper;
using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Helper;
using DanMu.Pan.MediatR.Commands.User;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace DanMu.Pan.MediatR.Handlers.User;

public class UpdateUserCommandHandler(
    ILogger<UpdateUserCommandHandler> logger,
    IMapper mapper,
    UserManager<Data.Entities.User> userManager,
    UserInfoToken userInfoToken,
    ClaimsHelper claimsHelper
) : IRequestHandler<UpdateUserCommand, ServiceResponse<UserDto>>
{
    public async Task<ServiceResponse<UserDto>> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken
    )
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
        {
            logger.LogError("User does not exist.");
            return ServiceResponse<UserDto>.Return409("User does not exist.");
        }
        var userClaims = await userManager.GetClaimsAsync(user);
        if (userClaims.Count > 0)
            await userManager.RemoveClaimsAsync(user, userClaims);

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.Address = request.Address;
        user.IsActive = request.IsActive;
        user.IsAdmin = request.IsAdmin;

        user.ModifiedDate = DateTime.UtcNow;
        user.ModifiedBy = userInfoToken.Id;

        await userManager.UpdateAsync(user);
        await claimsHelper.AddUserClaim(userManager, user, request.UserClaims, logger);
        return ServiceResponse<UserDto>.ReturnResultWith200(mapper.Map<UserDto>(user));
    }
}
