using System;
using System.Threading.Tasks;
using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract.Authentication;
using RemoteNotes.Service.Client.Contract.Hubs;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.Service.Storage;

namespace RemoteNotes.UI.Hubs.Authentication
{
    public class AuthenticationHub : BaseHub, IAuthenticationHub
    {
        #region -- BaseHub implementation --
        
        protected override string HubUrl => $"{Constants.BaseUrl}/notes";
        
        protected override void InitHubSubscriptions()
        {
        }
        
        #endregion
        
        #region -- IAuthenticationHub implementation --
        
        public event Action<string> Notify = delegate { };
        
        public async Task<Result<Member>> LogInAsync(string login, string password)
        {
            _currentMember = new Member
            {
                FirstName = "Yana",
                LastName = "Kazakova",
                NickName = login,
                Email = "kazakovayana@outlook.com",
                Interests = "123123 12 12323 12341 234234234 23434 12343454",
                DateOfBirth = DateTime.Now,
                AccessLevel = 4,
                IsActive = true
            };

            var result = new Result<Member>();

            result.SetSuccess(_currentMember);

            return result;
        }

        public async Task<Result> LogOutAsync()
        {
            _currentMember = null;

            var result = new Result();

            result.SetSuccess();

            return result;
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

        private Member _currentMember { get; set; }

        #endregion
    }
}