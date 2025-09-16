using Microsoft.AspNetCore.SignalR;

// TODO : 未完成代码

namespace DanMu.Pan.Repository.Hub;

public class UserHub(IConnectionMappingRepository userInfoInMemory) : Hub<IHubClient> { }
