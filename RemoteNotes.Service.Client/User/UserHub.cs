using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract.Hubs;
using RemoteNotes.Service.Client.Contract.User;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.Service.Domain.Requests;

namespace RemoteNotes.UI.Hubs.User
{
    public class UserHub : BaseHub, IUserHub
    {
        #region -- BaseHub implementation --
        
        protected override string HubUrl => $"{Constants.BaseUrl}/ServerHub";

        protected override HubConnection Hub { get; set; }

        protected override void InitHubSubscriptions()
        {
            Hub.On<string>(HubEvents.Notify, Notify);
        }

        #endregion

        #region -- IUserHub implementation --

        public event Action<string> Notify = delegate { };
        
        public Task<Result> SavePersonalInfoAsync(SavePersonalInfoRequest request)
        {
            var requestModel = new object[] { request };

            return ExecuteAsync(HubMethods.SavePersonalData, requestModel);
        }

        public Task<Result<Member>> SaveMemberInfoAsync(SaveMemberInfoRequest request)
        {
            var requestModel = new object[] { request };

            return ExecuteAsync<Member>(HubMethods.SaveMemberData, requestModel);
        }

        #endregion

        #region -- UserHub configuration constants --

        private static class HubMethods
        {
            public const string SavePersonalData = "changePersonalInfo";
            public const string SaveMemberData = "changeMemberInfo";
        }

        private static class HubEvents
        {
            public const string Notify = "Notify";
        }

        #endregion
    }
}