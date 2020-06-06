using RemoteNotes.Core;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.Service.Domain.Requests;
using System;
using System.Threading.Tasks;

namespace RemoteNotes.Service.User
{
    public interface IUserService
    {
        event Action<string> Notify;

        Task<Result> SavePersonalInfoAsync(SavePersonalInfoRequest request);
        
        Task<Result<Member>> SaveMemberInfoAsync(SaveMemberInfoRequest request);
    }
}