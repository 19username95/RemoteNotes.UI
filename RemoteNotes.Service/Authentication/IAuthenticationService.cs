using System.Threading.Tasks;
using RemoteNotes.Core;
using RemoteNotes.Service.Domain.Data;

namespace RemoteNotes.Service.Authentication
{
    public interface IAuthenticationService
    {
        bool IsAuthorized { get; }
        
        Member CurrentMember { get; }
        
        Task<Result<Member>> LogInAsync(string login, string password);
        
        Task<Result> LogOutAsync();
    }
}