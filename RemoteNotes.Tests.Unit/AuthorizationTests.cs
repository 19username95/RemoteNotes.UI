using System.Threading.Tasks;
using NUnit.Framework;
using RemoteNotes.Service.Authentication;
using RemoteNotes.Tests.Unit.Data;

namespace RemoteNotes.Tests.Unit
{
    public class AuthorizationTests
    {
        private IAuthenticationService _authenticationService;
        
        [SetUp]
        public void Setup()
        {
            _authenticationService = ServiceProvider.GetAuthenticationService();
        }

        [Test]
        public async Task LoginTest()
        {
            var result = await _authenticationService.LogInAsync("Yana", "PassWOrd");
            
            Assert.True(result.IsSuccess);
            Assert.True(_authenticationService.IsAuthorized);
            Assert.NotNull(result.SuccessResult);
            Assert.NotNull(_authenticationService.CurrentMember);
        }

        [Test]
        public  async Task LogoutTest()
        {
            var result = await _authenticationService.LogOutAsync();
            
            Assert.True(result.IsSuccess);
            Assert.True(!_authenticationService.IsAuthorized);
            Assert.Null(_authenticationService.CurrentMember);
        }
    }
}