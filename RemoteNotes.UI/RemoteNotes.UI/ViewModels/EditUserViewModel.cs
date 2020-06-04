using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Acr.UserDialogs;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using RemoteNotes.UI.Model;
using RemoteNotes.UI.Service.Authentication;
using RemoteNotes.UI.Service.User;
using Xamarin.Forms;

namespace RemoteNotes.UI.ViewModels
{
    class EditUserViewModel : ViewModelBase
    {
        private readonly IMedia _mediaService;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IUserDialogs _userDialogs;

        private byte[] _photoBytes;

        public EditUserViewModel(
            INavigationService navigationService,
            IMedia media,
            IAuthenticationService authenticationService,
            IUserService userService,
            IUserDialogs userDialogs) 
            : base(navigationService)
        {
            _mediaService = media;
            _authenticationService = authenticationService;
            _userService = userService;
            _userDialogs = userDialogs;
        }

        private string _FirstName;
        public string FirstName
        {
            get => _FirstName;
            set => SetProperty(ref _FirstName, value);
        }

        private string _LastName;
        public string LastName
        {
            get => _LastName;
            set => SetProperty(ref _LastName, value);
        }

        private DateTime _DateOfBirth;
        public DateTime DateOfBirth
        {
            get => _DateOfBirth;
            set => SetProperty(ref _DateOfBirth, value);
        }

        private string _NickName;
        public string NickName
        {
            get => _NickName;
            set => SetProperty(ref _NickName, value);
        }

        private string _Email;
        public string Email
        {
            get => _Email;
            set => SetProperty(ref _Email, value);
        }

        private string _Interests;
        public string Interests
        {
            get => _Interests;
            set => SetProperty(ref _Interests, value);
        }

        private DateTime _ModifyTime;
        public DateTime ModifyTime
        {
            get => _ModifyTime;
            set => SetProperty(ref _ModifyTime, value);
        }

        private ImageSource _Photo;
        public ImageSource Photo
        {
            get => _Photo;
            set => SetProperty(ref _Photo, value);
        }

        public ICommand SaveCommand => new DelegateCommand(OnSaveCommandAsync);
        public ICommand CancelCommand => new DelegateCommand(OnCancelCommandAsync);
        public ICommand PickImageCommand => new DelegateCommand(OnPickImageCommandAsync);

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            var currentMember = _authenticationService.CurrentMember;

            FirstName = currentMember.FirstName;
            LastName = currentMember.LastName;
            DateOfBirth = currentMember.DateOfBirth;
            NickName = currentMember.NickName;
            Email = currentMember.Email;
            Interests = currentMember.Interests;
            _photoBytes = currentMember.Photo;
            Photo = currentMember.PhotoSource;
        }

         private async void OnPickImageCommandAsync()
        {
            try
            {
                var pickedImage = await _mediaService.PickPhotoAsync();
                
                Photo = ImageSource.FromStream(pickedImage.GetStream);

                using (var stream = pickedImage.GetStream())
                {
                    using (BinaryReader br = new BinaryReader(stream))
                    {
                        _photoBytes = br.ReadBytes((int)stream.Length);
                    }
                }
            }
            catch (Exception)
            {

            }
            
        }

        private async void OnCancelCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }

        private async void OnSaveCommandAsync()
        {
            var saveUserRequest = new SaveMemberInfoRequest
            {
                MemberId = _authenticationService.CurrentMember.MemberId,
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                NickName = NickName,
                Email = Email,
                Interests = Interests,
                Photo = _photoBytes
            };

            var saveResult = await _userService.SaveMemberInfoAsync(saveUserRequest);

            if (saveResult.IsSuccess)
            {
                await _userDialogs.AlertAsync("Saving success!", "Info", "OK");
                await NavigationService.GoBackAsync();
            }
            else
            {
                await _userDialogs.AlertAsync("Saving failure!", "Error", "OK");
            }
        }
    }
}
