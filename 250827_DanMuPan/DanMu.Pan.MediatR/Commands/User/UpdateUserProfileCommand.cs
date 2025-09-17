using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Helper;
using MediatR;

// OK

namespace DanMu.Pan.MediatR.Commands.User;

public class UpdateUserProfileCommand : IRequest<ServiceResponse<UserDto>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}
