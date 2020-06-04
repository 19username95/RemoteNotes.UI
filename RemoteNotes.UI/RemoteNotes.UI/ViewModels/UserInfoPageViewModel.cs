using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RemoteNotes.UI.Model;
using RemoteNotes.UI.Service.Authentication;
using RemoteNotes.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace RemoteNotes.UI.ViewModels
{
	public class UserInfoPageViewModel : ViewModelBase
	{
        private readonly IAuthenticationService _authenticationService;

        public UserInfoPageViewModel(
            INavigationService navigationService,
            IAuthenticationService authenticationService)
            : base(navigationService)
        {
            _authenticationService = authenticationService;
        }

        private Member _currentMember;
        public Member CurrentMember
        {
            get => _currentMember;
            set => SetProperty(ref _currentMember, value);
        }

        public ICommand LogoutCommand => new DelegateCommand(OnLogoutCommandAsync);
        public ICommand EditProfileCommand => new DelegateCommand(OnEditProfileCommandAsync);

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            CurrentMember = _authenticationService.CurrentMember;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            CurrentMember = _authenticationService.CurrentMember;
        }

        private async void OnEditProfileCommandAsync()
        {
            await NavigationService.NavigateAsync($"{nameof(EditUserView)}");
        }

        private async void OnLogoutCommandAsync()
        {
            await _authenticationService.LogOutAsync();
            await NavigationService.NavigateAsync($"/{nameof(LoginPage)}");
        }
    }
}
