using DanMu.Pan.Data.Dto.User;

// TODO : 未完成代码

namespace DanMu.Pan.Repository.Hub;

public class ConnectionMappingRepository : IConnectionMappingRepository
{
    public bool AddUpdate(UserInfoToken tempUserInfo, string connectionId)
    {
        throw new NotImplementedException();
    }

    public void Remove(UserInfoToken tempUserInfo)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<UserInfoToken> GetAllUsersExceptThis(UserInfoToken tempUserInfo)
    {
        throw new NotImplementedException();
    }

    public UserInfoToken GetUserInfo(UserInfoToken tempUserInfo)
    {
        throw new NotImplementedException();
    }

    public UserInfoToken GetUserInfoByName(Guid id)
    {
        throw new NotImplementedException();
    }

    public UserInfoToken GetUserInfoByConnectionId(string connectionId)
    {
        throw new NotImplementedException();
    }

    public List<UserInfoToken> GetOnlineUserByList(List<Guid> users)
    {
        throw new NotImplementedException();
    }

    public Task SendFolderNotification(List<Guid> users, Guid folderId)
    {
        throw new NotImplementedException();
    }

    public Task RemovedFolderNotification(List<Guid> users, Guid folderId)
    {
        throw new NotImplementedException();
    }
}
