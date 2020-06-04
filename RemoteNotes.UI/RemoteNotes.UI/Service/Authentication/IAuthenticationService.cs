using System.Threading.Tasks;
using RemoteNotes.UI.Model;

namespace RemoteNotes.UI.Service.Authentication
{
    public interface IAuthenticationService
    {
        bool IsAuthorized { get; }
        
        Member CurrentMember { get; }
        
        Task<Result<Member>> LogInAsync(string login, string password);
        
        Task<Result> LogOutAsync();
    }
}