using System;
using System.Threading.Tasks;
using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Client.Contract.Hubs;
using RemoteNotes.Service.Client.Contract.User;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.Service.Domain.Requests;

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
        
        public async Task<Result> SavePersonalInfoAsync(SavePersonalInfoRequest request)
        {
            var result = new Result();
            
            if (GlobalStorage.CurrentMember.MemberId != request.MemberId)
                throw new Exception("Unknown member id was given");
            
            GlobalStorage.CurrentMember.FirstName = request.FirstName;
            GlobalStorage.CurrentMember.LastName = request.LastName;
            GlobalStorage.CurrentMember.DateOfBirth = request.DateOfBirth;
            GlobalStorage.CurrentMember.ModifyTime = request.ModifyTime;

            result.SetSuccess();
            
            return result;
        }

        public async Task<Result<Member>> SaveMemberInfoAsync(SaveMemberInfoRequest request)
        {
            GlobalStorage.CurrentMember = new Member
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

            result.SetSuccess(GlobalStorage.CurrentMember);

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
    }
}