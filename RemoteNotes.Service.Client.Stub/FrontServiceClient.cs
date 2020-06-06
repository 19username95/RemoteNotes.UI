using RemoteNotes.Service.Client.Contract.Authentication;
using RemoteNotes.Service.Client.Contract.Notes;
using RemoteNotes.Service.Client.Contract.User;
using RemoteNotes.UI.Hubs.Authentication;
using RemoteNotes.UI.Hubs.Notes;
using RemoteNotes.UI.Hubs.User;

namespace RemoteNotes.Service.Client.Contract
{
    public class FrontServiceClient : IFrontServiceClient
    {
        public IUserHub UserHub { get; private set; }

        public INotesHub NotesHub { get; private set; }

        public IAuthenticationHub AuthenticationHub { get; private set; }

        public FrontServiceClient()
        {
            UserHub = new UserHub();
            NotesHub = new NotesHub();
            AuthenticationHub = new AuthenticationHub();
        }
    }
}
