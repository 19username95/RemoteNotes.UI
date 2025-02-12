using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.Service.Domain.Requests;
using System;
using System.Threading.Tasks;

namespace RemoteNotes.Service.User
{
    public class UserService : IUserService
    {
        private readonly IFrontServiceClient _hubFacade;
        
        public UserService(
            IFrontServiceClient hubFacade)
        {
            _hubFacade = hubFacade;

            _hubFacade.UserHub.Notify += Notify.Invoke;
        }
        
        #region -- IUserService implementation --
        
        public event Action<string> Notify = delegate { };
        
        public Task<Result> SavePersonalInfoAsync(SavePersonalInfoRequest request)
        {
            return _hubFacade.UserHub.SavePersonalInfoAsync(request);
        }

        public Task<Result<Member>> SaveMemberInfoAsync(SaveMemberInfoRequest request)
        {
            return _hubFacade.UserHub.SaveMemberInfoAsync(request);
        }
        
        #endregion
    }
}