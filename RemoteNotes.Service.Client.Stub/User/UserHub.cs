using System;
using System.Collections.Generic;
using System.Linq;
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
                Photo = request.Photo,
                IsActive = request.IsActive,
                AccessLevel = request.AccessLevel
            };

            var result = new Result<Member>();

            result.SetSuccess(_currentMember);

            return result;
        }

        public async Task<Result<IEnumerable<Member>>> GetAllMembersAsync()
        {
            InitMocks();

            var result = new Result<IEnumerable<Member>>();

            result.SetSuccess(_mocks);

            return result;
        }

        public async Task<Result> EditMemberAsync(int memberId, bool isActive)
        {
            var toEdit = _mocks.Find(m => m.MemberId == memberId);
            if (toEdit != null)
            {
                toEdit.IsActive = isActive;
            }

            var result = new Result();

            result.SetSuccess();

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
        private List<Member> _mocks;
        private void InitMocks()
        {
            if (_mocks == null)
            {
                _mocks = new List<Member>();

                _mocks.Add(new Member { MemberId = 1, FirstName = "Ivan", LastName = "Petrov", DateOfBirth = DateTime.Now, Email = "i.petrov@gmail.com", Interests = "111", NickName = "petru4o", AccessLevel = 2, IsActive = true, ModifyTime = DateTime.Now });
                _mocks.Add(new Member { MemberId = 2, FirstName = "Anton", LastName = "Ivanov", DateOfBirth = DateTime.Now, Email = "a.ivanov@gmail.com", Interests = "222", NickName = "AI", AccessLevel = 2, IsActive = true, ModifyTime = DateTime.Now });
                _mocks.Add(new Member { MemberId = 3, FirstName = "Boris", LastName = "Antonov", DateOfBirth = DateTime.Now, Email = "b.antonov@gmail.com", Interests = "333", NickName = "borisio", AccessLevel = 2, IsActive = false, ModifyTime = DateTime.Now });
                _mocks.Add(new Member { MemberId = 4, FirstName = "Vasiliy", LastName = "Borisov", DateOfBirth = DateTime.Now, Email = "v.borisov@gmail.com", Interests = "444", NickName = "vasek", AccessLevel = 4, IsActive = true, ModifyTime = DateTime.Now });
            }
        }
        #endregion
    }
}