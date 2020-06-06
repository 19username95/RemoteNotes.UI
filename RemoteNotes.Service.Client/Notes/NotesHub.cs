using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using RemoteNotes.Core;
using RemoteNotes.Service.Client.Contract.Hubs;
using RemoteNotes.Service.Client.Contract.Notes;
using RemoteNotes.Service.Domain.Data;

namespace RemoteNotes.UI.Hubs.Notes
{
    public class NotesHub : BaseHub, INotesHub
    {
        #region -- BaseHub implementation --
        
        protected override string HubUrl => $"{Constants.BaseUrl}/notes";

        protected override HubConnection Hub { get; set; }

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
            return ExecuteAsync<IEnumerable<Note>>(HubMethods.GetNotes, memberIdModel);
        }

        public Task<Result<Note>> SaveAsync(Note note)
        {
            var noteModel = new object[] { note };
            return ExecuteAsync<Note>(HubMethods.SaveNote, noteModel);
        }

        public Task<Result> RemoveAsync(int noteId)
        {
            var noteIdModel = new object[] { noteId };
            return ExecuteAsync(HubMethods.RemoveNote, noteIdModel);
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
    }
}