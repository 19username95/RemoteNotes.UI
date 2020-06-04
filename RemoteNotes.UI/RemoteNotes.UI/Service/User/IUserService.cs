using System;
using System.Threading.Tasks;
using RemoteNotes.UI.Model;

namespace RemoteNotes.UI.Service.User
{
    public interface IUserService
    {
        event Action<string> Notify;

        Task<Result> SavePersonalInfoAsync(SavePersonalInfoRequest request);
        
        Task<Result<Member>> SaveMemberInfoAsync(SaveMemberInfoRequest request);
    }
}