using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using RemoteNotes.Service.Domain.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RemoteNotes.UI.ViewModels
{
    public class UsersCollectionViewModel : ViewModelBase
    {
        private readonly IUserDialogs _userDialogs;

        public UsersCollectionViewModel(INavigationService navigationService, IUserDialogs userDialogs) : base(navigationService)
        {
            _userDialogs = userDialogs;
        }

        private ObservableCollection<Member> _usersCollection;
        public ObservableCollection<Member> UsersCollection
        {
            get { return _usersCollection; }
            set { SetProperty(ref _usersCollection, value); }
        }

       // public ICommand DeactivateCommand => new DelegateCommand<Member>(OnDeactivateCommandAsync);
        public ICommand UserTappedCommand => new DelegateCommand<Member>(OnUserTappedCommandAsync);

        public override async void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            await Task.Yield();

            using (_userDialogs.Loading())
            {
                var allUsers = await GetAllUsersAsync();

                UsersCollection = new ObservableCollection<Member>(allUsers);
            }
        }

        public async Task<IEnumerable<Member>> GetAllUsersAsync()
        {
            //var result = new List<Note>();
            //var allNotesResult = await _noteService.GetAllAsync(_currentMember.MemberId);

            //if (allNotesResult.IsSuccess)
            //{
            //    result = new List<Note>(allNotesResult.SuccessResult);
            //}
            //else
            //{
            //    await _userDialogs.AlertAsync("Cant get all notes", "Error", "OK");
            //}

            //return result;

            var result = new List<Member>();

            if (true)
            {
                result.Add(new Member { MemberId = 1, FirstName = "Imya1", LastName = "Familiya1", NickName = "Nick1", DateOfBirth = DateTime.Now, Email = "some@th.com1" });
                result.Add(new Member { MemberId = 1, FirstName = "Imya2", LastName = "Familiya2", NickName = "Nick2", DateOfBirth = DateTime.Now, Email = "some@th.com2" });
            }
            else
            {
                await _userDialogs.AlertAsync("Cant get all notes", "Error", "OK");
            }

            return result;

        }

        //private async void OnDeactivateCommandAsync(Member tappedMember)
        //{
            
        //}

        private async void OnUserTappedCommandAsync(Member member)
        {
            Debug.WriteLine("i'm there!");
        }
    }
}
