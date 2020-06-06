using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract.Authentication;
using RemoteNotes.Service.Client.Contract.Hubs;
using RemoteNotes.Service.Domain.Data;

namespace RemoteNotes.UI.Hubs.Authentication
{
    public class AuthenticationHub : BaseHub, IAuthenticationHub
    {
        #region -- BaseHub implementation --
        
        protected override string HubUrl => $"{Constants.BaseUrl}/notes";

        protected override HubConnection Hub { get; set; }

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
            return ExecuteAsync<Member>(HubMethods.LogIn, authModel);
        }

        public Task<Result> LogOutAsync()
        {
            return ExecuteAsync(HubMethods.LogOut);
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