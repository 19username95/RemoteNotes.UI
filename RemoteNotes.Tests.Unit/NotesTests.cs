using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using RemoteNotes.Service.Authentication;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.Service.Note;
using RemoteNotes.Tests.Unit.Data;

namespace RemoteNotes.Tests.Unit
{
    public class NotesTests
    {
        private IAuthenticationService _authenticationService;
        private INoteService _noteService;

        private int MemberId => _authenticationService.CurrentMember.MemberId;
        
        [SetUp]
        public async Task Setup()
        {
            _authenticationService = ServiceProvider.GetAuthenticationService();
            var result = await _authenticationService.LogInAsync("Yana", "PassWOrd");
            _noteService = ServiceProvider.GetNoteService();
        }

        [Test]
        public async Task GetAllNotesTests()
        {
            var result = await _noteService.GetAllAsync(MemberId);
            
            Assert.True(result.IsSuccess);
            Assert.True(_authenticationService.IsAuthorized);
            Assert.NotNull(result.SuccessResult);
            Assert.NotNull(_authenticationService.CurrentMember);
        }
        
        [Test]
        public async Task SaveNoteTests()
        {
            var allNotes = await _noteService.GetAllAsync(MemberId);
            var lastNote = allNotes.SuccessResult.LastOrDefault();
            var newId = lastNote.Id + 1;
            var note = new Note()
            {
                Id = newId,
                Topic = "topic text test",
                Text = "text test",
                PublishTime = DateTime.Now,
                MemberId = MemberId
            };
            
            var saveResult = await _noteService.SaveAsync(note);
            allNotes = await _noteService.GetAllAsync(MemberId);
            
            Assert.True(saveResult.IsSuccess);
            Assert.True(allNotes.IsSuccess);
            Assert.NotNull(lastNote);
            Assert.NotNull(allNotes.SuccessResult);
            Assert.NotNull(saveResult.SuccessResult);
            Assert.True(allNotes.SuccessResult.Any(n => n.Id == newId));
        }
        
        
        [Test]
        public async Task RemoveNoteTests()
        {
            var allNotes = await _noteService.GetAllAsync(MemberId);
            var lastNote = allNotes.SuccessResult.LastOrDefault();
            var lastNoteId = lastNote.Id;
            
            var result = await _noteService.RemoveAsync(lastNoteId);
            allNotes = await _noteService.GetAllAsync(MemberId);
            
            Assert.True(result.IsSuccess);
            Assert.True(allNotes.IsSuccess);
            Assert.NotNull(lastNote);
            Assert.False(allNotes.SuccessResult.Any(n => n.Id == lastNoteId));
        }
    }
}