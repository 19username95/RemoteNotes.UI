using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RemoteNotes.UI.Hubs.Notes;
using RemoteNotes.UI.Model;

namespace RemoteNotes.UI.Service.Note
{
    public class NoteService : INoteService
    {
        private readonly INotesHub _notesHub;
        
        public NoteService(
            INotesHub notesHub)
        {
            _notesHub = notesHub;

            _notesHub.Notify += Notify.Invoke;
        }
        
        #region -- INoteService implementation --

        public event Action<string> Notify = delegate { };
        
        public Task<Result<IEnumerable<Model.Note>>> GetAllAsync(int memberId)
        {
            return _notesHub.GetAllAsync(memberId);
        }

        public Task<Result<Model.Note>> SaveAsync(Model.Note note)
        {
            return _notesHub.SaveAsync(note);
        }

        public Task<Result> RemoveAsync(int noteId)
        {
            return _notesHub.RemoveAsync(noteId);
        }
        
        #endregion
    }
}