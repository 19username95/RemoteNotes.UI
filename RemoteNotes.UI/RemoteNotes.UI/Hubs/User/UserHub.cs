using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using RemoteNotes.UI.Model;
using RemoteNotes.UI.Service.Storage;

namespace RemoteNotes.UI.Hubs.User
{
    public class UserHub : BaseHub, IUserHub
    {
        #region -- BaseHub implementation --
        
        protected override string HubUrl => $"{Constants.BaseUrl}/notes";
        
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

            //return ExecuteAndReturnAsync<Member>(HubMethods.SaveMemberData, requestModel);
            return SaveMemberInfoAsync_mock(request);
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

        public async Task<Result<Member>> SaveMemberInfoAsync_mock(SaveMemberInfoRequest request)
        {
            _currentMember = new Member
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Interests = request.Interests,
                Email = request.Email,
                NickName = request.NickName,
                Photo = request.Photo
            };

            var result = new Result<Member>();

            result.SetSuccess(_currentMember);

            return result;
        }

        #endregion
    }
}