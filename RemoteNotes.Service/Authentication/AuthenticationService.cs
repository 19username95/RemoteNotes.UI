using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.Service.Storage;
using System.Threading.Tasks;

namespace RemoteNotes.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string CurrentUserNameStorageKey = nameof(CurrentUserNameStorageKey);
        
        private readonly IFrontServiceClient _hubFacade;
        private readonly IStorageService _storage;
        
        public AuthenticationService(
            IFrontServiceClient hubFacade,
            IStorageService storage)
        {
            _hubFacade = hubFacade;
            _storage = storage;
        }
        
        #region -- IAuthenticationService implementation --

        public Member CurrentMember => _storage.Load<Member>(CurrentUserNameStorageKey);

        public bool IsAuthorized => CurrentMember != null;

        public async Task<Result<Member>> LogInAsync(string login, string password)
        {
            var authResult = await _hubFacade.AuthenticationHub.LogInAsync(login, password);

            if (authResult.IsSuccess)
            {
                _storage.Save<Member>(CurrentUserNameStorageKey, authResult.SuccessResult);
            }

            return authResult;
        }

        public Task<Result> LogOutAsync()
        {
            _storage.Save<Member>(CurrentUserNameStorageKey, null);
            return _hubFacade.AuthenticationHub.LogOutAsync();
        }
        
        #endregion
    }
}