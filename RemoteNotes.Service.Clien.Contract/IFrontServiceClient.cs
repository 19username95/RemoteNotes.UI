using RemoteNotes.Service.Client.Contract.Authentication;
using RemoteNotes.Service.Client.Contract.Notes;
using RemoteNotes.Service.Client.Contract.User;

namespace RemoteNotes.Service.Client.Contract
{
    public interface IFrontServiceClient
    {
        IUserHub UserHub { get; }
        INotesHub NotesHub { get; }
        IAuthenticationHub AuthenticationHub { get; }
    }
}
