using System;
using System.Threading.Tasks;
using NUnit.Framework;
using RemoteNotes.Service.Authentication;
using RemoteNotes.Service.Domain.Requests;
using RemoteNotes.Service.User;
using RemoteNotes.Tests.Unit.Data;

namespace RemoteNotes.Tests.Unit
{
    public class UsersTests
    {
        private IAuthenticationService _authenticationService;
        private IUserService _userService;

        private int MemberId => _authenticationService.CurrentMember.MemberId;
        
        [SetUp]
        public async Task Setup()
        {
            _authenticationService = ServiceProvider.GetAuthenticationService();
            var result = await _authenticationService.LogInAsync("Yana", "PassWOrd");
            _userService = ServiceProvider.GetUserService();
        }

        [Test]
        public async Task SaveMemberInfoTests()
        {
            var saveResult = await _userService.SaveMemberInfoAsync(new SaveMemberInfoRequest()
            {
                DateOfBirth = DateTime.Now,
                Email = "email@example.com",
                FirstName = "Yana",
                LastName = "Kazakova",
                Interests = "Some her interests",
                MemberId = MemberId,
                NickName = "Cat",
            });
            
            Assert.True(saveResult.IsSuccess);
            Assert.True(_authenticationService.IsAuthorized);
            Assert.NotNull(saveResult.SuccessResult);
            Assert.NotNull(_authenticationService.CurrentMember);
            Assert.True(saveResult.SuccessResult.MemberId == MemberId);
            Assert.True(saveResult.SuccessResult.Email == "email@example.com");
            Assert.True(saveResult.SuccessResult.FirstName == "Yana");
            Assert.True(saveResult.SuccessResult.LastName == "Kazakova");
            Assert.True(saveResult.SuccessResult.Interests == "Some her interests");
            Assert.True(saveResult.SuccessResult.NickName == "Cat");
        }
        

        [Test]
        public async Task SavePersonalInfoTests()
        {
            var saveResult = await _userService.SavePersonalInfoAsync(new SavePersonalInfoRequest()
            {
                DateOfBirth = DateTime.Now,
                FirstName = "Gleb",
                LastName = "Kazakevich",
                MemberId = MemberId
            });
            
            Assert.True(saveResult.IsSuccess);
            Assert.True(_authenticationService.IsAuthorized);
            Assert.NotNull(_authenticationService.CurrentMember);
            Assert.True(_authenticationService.CurrentMember.MemberId == MemberId);
            Assert.True(_authenticationService.CurrentMember.LastName == "Kazakevich");
            Assert.True(_authenticationService.CurrentMember.FirstName == "Gleb");
        }
    }
}