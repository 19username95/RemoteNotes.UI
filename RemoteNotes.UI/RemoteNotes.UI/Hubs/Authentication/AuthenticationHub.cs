using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using RemoteNotes.UI.Model;
using RemoteNotes.UI.Service.Storage;

namespace RemoteNotes.UI.Hubs.Authentication
{
    public class AuthenticationHub : BaseHub, IAuthenticationHub
    {
        #region -- BaseHub implementation --
        
        protected override string HubUrl => $"{Constants.BaseUrl}/notes";
        
        protected override void InitHubSubscriptions()
        {
            Hub.On<string>(HubEvents.Notify, Notify);
        }
        
        #endregion
        
        #region -- IAuthenticationHub implementation --
        
        public event Action<string> Notify = delegate { };
        
        public Task<Result<Member>> LogInAsync(string login, string password)
        {
            var authModel = new object[] { login, password };

            //return ExecuteAndReturnAsync<Member>(HubMethods.LogIn, authModel);
            return LogInAsync_mock(login, password);
        }

        public Task<Result> LogOutAsync()
        {
            //return ExecuteAsync(HubMethods.LogOut);
            return LogOutAsync_mock();
        }
        
        #endregion
        
        #region -- AuthenticationHub configuration constants --

        private static class HubMethods
        {
            public const string LogIn = "enter";
            public const string LogOut = "exit";
        }

        private static class HubEvents
        {
            public const string Notify = "Notify";
        }

        #endregion

        #region -- Mocks --

        private Member _currentMember
        {
            get
            {
                var storage = App.Resolve<IStorageService>();

                return storage.Load<Member>("CurrentUserNameStorageKey");
            }
            set
            {
                var storage = App.Resolve<IStorageService>();

                storage.Save<Member>("CurrentUserNameStorageKey", value);
            }
        }

        public async Task<Result<Member>> LogInAsync_mock(string login, string password)
        {
            _currentMember = new Member
            {
                FirstName = "Yana",
                LastName = "Kazakova",
                NickName = login,
                Email = "kazakovayana@outlook.com",
                Interests = "123123 12 12323 12341 234234234 23434 12343454",
                DateOfBirth = DateTime.Now
            };
            
            var result = new Result<Member>();

            result.SetSuccess(_currentMember);

            return result;
        }

        public async Task<Result> LogOutAsync_mock()
        {
            var storage = App.Resolve<IStorageService>();
            
            _currentMember = null;

            var result = new Result();

            result.SetSuccess();

            return result;
        }

        #endregion
    }
}