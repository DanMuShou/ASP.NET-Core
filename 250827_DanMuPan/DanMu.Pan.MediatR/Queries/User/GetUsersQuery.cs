using DanMu.Pan.Data.Resources;
using DanMu.Pan.Repository.User;
using MediatR;

namespace DanMu.Pan.MediatR.Queries.User;

public class GetUsersQuery(UserResource userResource) : IRequest<UserList>
{
    public UserResource UserResource => userResource;
}
