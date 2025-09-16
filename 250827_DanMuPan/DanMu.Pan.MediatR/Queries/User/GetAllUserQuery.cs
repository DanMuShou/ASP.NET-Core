using DanMu.Pan.Data.Dto.User;
using MediatR;

namespace DanMu.Pan.MediatR.Queries.User;

public class GetAllUserQuery : IRequest<List<UserDto>> { }
