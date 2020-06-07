using System.Threading.Tasks;
using NUnit.Framework;
using RemoteNotes.Service.Authentication;
using RemoteNotes.Service.Note;
using RemoteNotes.Tests.Unit.Data;

namespace RemoteNotes.Tests.Unit
{
    public class NotesTests
    {
        private IAuthenticationService _authenticationService;
        private INoteService _noteService;
        
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
            var result = await _noteService.GetAllAsync(_authenticationService.CurrentMember.MemberId);
            
            Assert.True(result.IsSuccess);
            Assert.True(_authenticationService.IsAuthorized);
            Assert.NotNull(result.SuccessResult);
            Assert.NotNull(_authenticationService.CurrentMember);
        }
    }
}