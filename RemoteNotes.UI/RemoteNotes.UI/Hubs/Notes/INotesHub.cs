using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.UI.Model;

namespace RemoteNotes.UI.Hubs.Notes
{
    public interface INotesHub : IBaseHub
    {
        event Action<string> Notify;

        Task<Result<IEnumerable<Note>>> GetAllAsync(int memberId);

        Task<Result<Note>> SaveAsync(Note note);

        Task<Result> RemoveAsync(int noteId);
    }
}