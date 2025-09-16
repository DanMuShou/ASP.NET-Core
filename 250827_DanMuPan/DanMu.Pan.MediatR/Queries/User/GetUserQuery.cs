using DanMu.Pan.Data.Dto.User;
using DanMu.Pan.Helper;
using MediatR;

namespace DanMu.Pan.MediatR.Queries.User;

public class GetUserQuery(Guid id) : IRequest<ServiceResponse<UserDto>>
{
    public Guid Id => id;
}
