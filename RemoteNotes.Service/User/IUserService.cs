using RemoteNotes.Core;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.Service.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MemberModel = RemoteNotes.Service.Domain.Data.Member;

namespace RemoteNotes.Service.User
{
    public interface IUserService
    {
        event Action<string> Notify;

        Task<Result> SavePersonalInfoAsync(SavePersonalInfoRequest request);
        
        Task<Result<Member>> SaveMemberInfoAsync(SaveMemberInfoRequest request);

        Task<Result<IEnumerable<MemberModel>>> GetAllMembersAsync();
    }
}