using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using RemoteNotes.UI.Model;

namespace RemoteNotes.UI.Hubs.Notes
{
    public class NotesHub : BaseHub, INotesHub
    {
        #region -- BaseHub implementation --
        
        protected override string HubUrl => $"{Constants.BaseUrl}/notes";
        
        protected override void InitHubSubscriptions()
        {
            Hub.On<string>(HubEvents.Notify, Notify);
        }

        #endregion

        #region -- INotesHub implementation --

        public event Action<string> Notify = delegate { };
        
        public Task<Result<IEnumerable<Note>>> GetAllAsync(int memberId)
        {
            var memberIdModel = new object[] { memberId };

            //return ExecuteAndReturnAsync<IEnumerable<Note>>(HubMethods.GetNotes, memberIdModel);
            return GetAllAsync_mock(memberId);
        }

        public Task<Result<Note>> SaveAsync(Note note)
        {
            var noteModel = new object[] { note };

            //return ExecuteAndReturnAsync<Note>(HubMethods.SaveNote, noteModel);
            return SaveAsync_mock(note);
        }

        public Task<Result> RemoveAsync(int noteId)
        {
            var noteIdModel = new object[] { noteId };

            //return ExecuteAsync(HubMethods.RemoveNote, noteIdModel);
            return RemoveAsync_mock(noteId);
        }

        #endregion

        #region -- NotesHub configuration constants --

        private static class HubMethods
        {
            public const string GetNotes = "getNoteInfoCollectionByMemberId";
            public const string SaveNote = "addNoteInfo";
            public const string RemoveNote = "removeNoteInfo";
        }

        private static class HubEvents
        {
            public const string Notify = "Notify";
        }

        #endregion

        #region -- Mocks --

        private List<Note> _mocks;

        private void InitMocks()
        {
            if (_mocks == null)
            {
                _mocks = new List<Note>();

                _mocks.Add(new Note { Topic = "note 1", Id = 0, MemberId = 0, ModifyTime = DateTime.Now, PublishTime = DateTime.Now, Text = "Note text 1" });
                _mocks.Add(new Note { Topic = "note 2", Id = 1, MemberId = 0, ModifyTime = DateTime.Now, PublishTime = DateTime.Now, Text = "Note text 2" });
                _mocks.Add(new Note { Topic = "note 3", Id = 2, MemberId = 0, ModifyTime = DateTime.Now, PublishTime = DateTime.Now, Text = "Note text 3" });
                _mocks.Add(new Note { Topic = "note 4", Id = 3, MemberId = 0, ModifyTime = DateTime.Now, PublishTime = DateTime.Now, Text = "Note text 4" });
                _mocks.Add(new Note { Topic = "note 5", Id = 4, MemberId = 0, ModifyTime = DateTime.Now, PublishTime = DateTime.Now, Text = "Note text 5" });
                _mocks.Add(new Note { Topic = "note 6", Id = 5, MemberId = 0, ModifyTime = DateTime.Now, PublishTime = DateTime.Now, Text = "Note text 6" });
                _mocks.Add(new Note { Topic = "note 7", Id = 6, MemberId = 0, ModifyTime = DateTime.Now, PublishTime = DateTime.Now, Text = "Note text 7" });
                _mocks.Add(new Note { Topic = "note 8", Id = 7, MemberId = 0, ModifyTime = DateTime.Now, PublishTime = DateTime.Now, Text = "Note text 8" });
                _mocks.Add(new Note { Topic = "note 9", Id = 8, MemberId = 0, ModifyTime = DateTime.Now, PublishTime = DateTime.Now, Text = "Note text 9" });
                _mocks.Add(new Note { Topic = "note 10", Id = 9, MemberId = 0, ModifyTime = DateTime.Now, PublishTime = DateTime.Now, Text = "Note text 10" });
            }
        }

        public async Task<Result<IEnumerable<Note>>> GetAllAsync_mock(int memberId)
        {
            InitMocks();

            var result = new Result<IEnumerable<Note>>();

            result.SetSuccess(_mocks.Where(n => n.MemberId == memberId));

            return result;
        }

        public async Task<Result<Note>> SaveAsync_mock(Note note)
        {
            InitMocks();

            if (note != null)
            {
                await RemoveAsync_mock(note.Id);
                
                var nextId = _mocks.Max(n => n.Id) + 1;

                note.Id = nextId;

                _mocks.Add(note);
            }

            var result = new Result<Note>();

            result.SetSuccess(note);

            return result;
        }

        public async Task<Result> RemoveAsync_mock(int noteId)
        {
            InitMocks();

            var toRemove = _mocks.FirstOrDefault(n => n.Id == noteId);

            if (toRemove != null)
            {
                _mocks.Remove(toRemove);
            }

            var result = new Result();

            result.SetSuccess();

            return result;
        }

        #endregion
    }
}