using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using NoteModel = RemoteNotes.Service.Domain.Data.Note;

namespace RemoteNotes.Service.Note
{
    public class NoteService : INoteService
    {
        private readonly IFrontServiceClient _hubFacade;
        
        public NoteService(
            IFrontServiceClient hubFacade)
        {
            _hubFacade = hubFacade;

            _hubFacade.NotesHub.Notify += Notify.Invoke;
        }
        
        #region -- INoteService implementation --

        public event Action<string> Notify = delegate { };
        
        public Task<Result<IEnumerable<NoteModel>>> GetAllAsync(int memberId)
        {
            return _hubFacade.NotesHub.GetAllAsync(memberId);
        }

        public Task<Result<NoteModel>> SaveAsync(NoteModel note)
        {
            return _hubFacade.NotesHub.SaveAsync(note);
        }

        public Task<Result> RemoveAsync(int noteId)
        {
            return _hubFacade.NotesHub.RemoveAsync(noteId);
        }

        #endregion
    }
}