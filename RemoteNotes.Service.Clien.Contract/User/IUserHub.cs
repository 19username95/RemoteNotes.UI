using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract.Base;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.Service.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RemoteNotes.Service.Client.Contract.User
{
    public interface IUserHub : IBaseHub
    {
        event Action<string> Notify;

        Task<Result> SavePersonalInfoAsync(SavePersonalInfoRequest request);
        
        Task<Result<Member>> SaveMemberInfoAsync(SaveMemberInfoRequest request);

        Task<Result<IEnumerable<Member>>> GetAllMembersAsync();

        Task<Result> EditMemberAsync(int memberId, bool isActive);
    }
}