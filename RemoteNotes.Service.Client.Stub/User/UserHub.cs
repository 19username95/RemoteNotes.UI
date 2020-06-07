using System;
using System.Threading.Tasks;
using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract.Hubs;
using RemoteNotes.Service.Client.Contract.User;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.Service.Domain.Requests;
using RemoteNotes.Service.Storage;

namespace RemoteNotes.UI.Hubs.User
{
    public class UserHub : BaseHub, IUserHub
    {
        #region -- BaseHub implementation --
        
        protected override string HubUrl => $"{Constants.BaseUrl}/notes";
        
        protected override void InitHubSubscriptions()
        {
        }

        #endregion

        #region -- IUserHub implementation --

        public event Action<string> Notify = delegate { };
        
        public Task<Result> SavePersonalInfoAsync(SavePersonalInfoRequest request)
        {
            var requestModel = new object[] { request };

            return ExecuteAsync(HubMethods.SavePersonalData, requestModel);
        }

        public async Task<Result<Member>> SaveMemberInfoAsync(SaveMemberInfoRequest request)
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

        private Member _currentMember { get; set; }

        #endregion
    }
}