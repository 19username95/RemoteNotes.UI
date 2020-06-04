using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RemoteNotes.UI.Service.Authentication;
using RemoteNotes.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace RemoteNotes.UI.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
	{
        private readonly IAuthenticationService _authenticationService;

        public LoginPageViewModel(
            INavigationService navigationService,
            IAuthenticationService authenticationService)
            : base(navigationService)
        {
            _authenticationService = authenticationService;

            IsLoginInvalid = true;
#if DEBUG
            Username = "Jana";
            Password = "1231";
#endif
        }

        #region Properties

        private bool _isLoginInvalid;
        public bool IsLoginInvalid
        {
            get { return _isLoginInvalid; }
            set { SetProperty(ref _isLoginInvalid, value); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public ICommand LogInCommand => new DelegateCommand(OnLogInCommandAsync);

        #endregion

        #region -- Overrides --

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(Username) ||
                propertyName == nameof(Password))
            {
                IsLoginInvalid = false;
            }
        }

        #endregion

        #region -- Methods --

        private async void OnLogInCommandAsync()
        {
            var loginResult = await _authenticationService.LogInAsync(Username, Password);

            if (loginResult.IsSuccess)
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(RemoteNotesTabbedPage)}");
            }
            else
            {
                IsLoginInvalid = true;
            }
        }

        #endregion
    }
}
