using System;
using System.Threading.Tasks;
using RemoteNotes.UI.Hubs.User;
using RemoteNotes.UI.Model;

namespace RemoteNotes.UI.Service.User
{
    public class UserService : IUserService
    {
        private readonly IUserHub _userHub;
        
        public UserService(
            IUserHub userHub)
        {
            _userHub = userHub;

            _userHub.Notify += Notify.Invoke;
        }
        
        #region -- IUserService implementation --
        
        public event Action<string> Notify = delegate { };
        
        public Task<Result> SavePersonalInfoAsync(SavePersonalInfoRequest request)
        {
            return _userHub.SavePersonalInfoAsync(request);
        }

        public Task<Result<Member>> SaveMemberInfoAsync(SaveMemberInfoRequest request)
        {
            return _userHub.SaveMemberInfoAsync(request);
        }
        
        #endregion
    }
}