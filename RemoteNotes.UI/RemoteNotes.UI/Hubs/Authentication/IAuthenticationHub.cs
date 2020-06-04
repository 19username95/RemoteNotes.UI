using System;
using System.Threading.Tasks;
using RemoteNotes.UI.Model;

namespace RemoteNotes.UI.Hubs.Authentication
{
    public interface IAuthenticationHub : IBaseHub
    {
        event Action<string> Notify;
        
        Task<Result<Member>> LogInAsync(string login, string password);

        Task<Result> LogOutAsync();
    }
}