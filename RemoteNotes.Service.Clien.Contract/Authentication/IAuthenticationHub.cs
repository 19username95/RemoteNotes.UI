using System;
using System.Threading.Tasks;
using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract.Base;
using RemoteNotes.Service.Domain.Data;

namespace RemoteNotes.Service.Client.Contract.Authentication
{
    public interface IAuthenticationHub : IBaseHub
    {
        event Action<string> Notify;
        
        Task<Result<Member>> LogInAsync(string login, string password);

        Task<Result> LogOutAsync();
    }
}