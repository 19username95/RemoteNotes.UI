using RemoteNotes.Service.Authentication;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Note;
using RemoteNotes.Service.Storage;
using RemoteNotes.Tests.Unit.MockedServices;

namespace RemoteNotes.Tests.Unit.Data
{
    public static class ServiceProvider
    {
        public static IStorageService GetStorageService()
        {
            return new StorageService_mock();
        }

        public static IFrontServiceClient GetHubFacade()
        {
            return new FrontServiceClient();
        }

        public static IAuthenticationService GetAuthenticationService()
        {
            return new AuthenticationService(GetHubFacade(), GetStorageService());
        }

        public static INoteService GetNoteService()
        {
            return new NoteService(GetHubFacade());
        }
    }
}
