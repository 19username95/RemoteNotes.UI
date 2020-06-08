using System;
using System.Threading.Tasks;
using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract;
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
            GlobalStorage.CurrentMember = new Member
            {
                FirstName = "Yana",
                LastName = "Kazakova",
                NickName = login,
                Email = "kazakovayana@outlook.com",
                Interests = "123123 12 12323 12341 234234234 23434 12343454",
                DateOfBirth = DateTime.Now
            };

            var result = new Result<Member>();

            result.SetSuccess(GlobalStorage.CurrentMember);

            return result;
        }

        public async Task<Result> LogOutAsync()
        {
            GlobalStorage.CurrentMember = null;

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
    }
}