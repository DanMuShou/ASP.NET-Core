using DanMu.Pan.Data.Dto.User;
using MediatR;

// OK

namespace DanMu.Pan.MediatR.Queries.User;

/// <summary>
/// 查询最近注册的用户列表
/// </summary>
public class GetRecentlyRegisteredUserQuery : IRequest<List<UserDto>> { }
