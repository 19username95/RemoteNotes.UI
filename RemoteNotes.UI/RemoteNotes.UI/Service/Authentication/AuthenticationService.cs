using System.Threading.Tasks;
using RemoteNotes.UI.Hubs.Authentication;
using RemoteNotes.UI.Model;
using RemoteNotes.UI.Service.Storage;

namespace RemoteNotes.UI.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string CurrentUserNameStorageKey = nameof(CurrentUserNameStorageKey);
        
        private readonly IAuthenticationHub _authenticationHub;
        private readonly IStorageService _storage;
        
        public AuthenticationService(
            IAuthenticationHub authHub,
            IStorageService storage)
        {
            _authenticationHub = authHub;
            _storage = storage;
        }
        
        #region -- IAuthenticationService implementation --

        public Member CurrentMember => _storage.Load<Member>(CurrentUserNameStorageKey);

        public bool IsAuthorized => CurrentMember != null;

        public async Task<Result<Member>> LogInAsync(string login, string password)
        {
            var authResult = await _authenticationHub.LogInAsync(login, password);

            if (authResult.IsSuccess)
            {
                _storage.Save<Member>(CurrentUserNameStorageKey, authResult.SuccessResult);
            }

            return authResult;
        }

        public Task<Result> LogOutAsync()
        {
            _storage.Save<Member>(CurrentUserNameStorageKey, null);
            return _authenticationHub.LogOutAsync();
        }
        
        #endregion
    }
}