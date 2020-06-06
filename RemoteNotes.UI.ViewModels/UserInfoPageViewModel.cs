using Prism.Commands;
using Prism.Navigation;
using RemoteNotes.Service.Authentication;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.UI.Views;
using System.Windows.Input;

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

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

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
