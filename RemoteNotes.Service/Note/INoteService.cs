using RemoteNotes.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using NoteModel = RemoteNotes.Service.Domain.Data.Note;

namespace RemoteNotes.Service.Note
{
    public interface INoteService
    {
        event Action<string> Notify;

        Task<Result<IEnumerable<NoteModel>>> GetAllAsync(int memberId);

        Task<Result<NoteModel>> SaveAsync(NoteModel note);

        Task<Result> RemoveAsync(int noteId);
    }
}