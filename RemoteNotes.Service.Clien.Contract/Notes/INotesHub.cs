using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract.Base;
using RemoteNotes.Service.Domain.Data;

namespace RemoteNotes.Service.Client.Contract.Notes
{
    public interface INotesHub : IBaseHub
    {
        event Action<string> Notify;

        Task<Result<IEnumerable<Note>>> GetAllAsync(int memberId);

        Task<Result<Note>> SaveAsync(Note note);

        Task<Result> RemoveAsync(int noteId);
    }
}