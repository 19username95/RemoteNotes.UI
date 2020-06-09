using Prism.Commands;
using Prism.Navigation;
using RemoteNotes.Service.Domain.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RemoteNotes.UI.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private Member _selectedMember;
        public UserViewModel(INavigationService navigationService) : base(navigationService)
        {

        }

        private int _memberId;
        public int MemberId
        {
            get => _memberId;
            set => SetProperty(ref _memberId, value);
        }
        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }
        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _nickName;
        public string NickName
        {
            get => _nickName;
            set => SetProperty(ref _nickName, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        private DateTime _modifyTime;
        public DateTime ModifyTime
        {
            get => _modifyTime;
            set => SetProperty(ref _modifyTime, value);
        }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value);
        }


        public ICommand GoBackCommand => new DelegateCommand(OnGoBackCommandAsync);

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.TryGetValue("SelectedMember", out Member member))
            {
                _selectedMember = member;
                MemberId = member.MemberId;
                FirstName = member.FirstName;
                LastName = member.LastName;
                NickName = member.NickName;
                Email = member.Email;
                ModifyTime = member.ModifyTime;
                IsActive = member.IsActive;
            }
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue("SelectedMember", out Member member))
            {
                _selectedMember = member;
                MemberId = member.MemberId;
                FirstName = member.FirstName;
                LastName = member.LastName;
                Email = member.Email;
                ModifyTime = member.ModifyTime;
                IsActive = member.IsActive;
            }
        }
        private async void OnGoBackCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
