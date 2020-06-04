using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.UI.Model;

namespace RemoteNotes.UI.Service.Note
{
    public interface INoteService
    {
        event Action<string> Notify;

        Task<Result<IEnumerable<Model.Note>>> GetAllAsync(int memberId);

        Task<Result<Model.Note>> SaveAsync(Model.Note note);

        Task<Result> RemoveAsync(int noteId);
    }
}