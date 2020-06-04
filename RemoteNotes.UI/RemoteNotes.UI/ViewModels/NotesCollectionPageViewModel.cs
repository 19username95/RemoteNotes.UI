using Acr.UserDialogs;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RemoteNotes.UI.Model;
using RemoteNotes.UI.Service.Authentication;
using RemoteNotes.UI.Service.Note;
using RemoteNotes.UI.Service.User;
using RemoteNotes.UI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RemoteNotes.UI.ViewModels
{
	public class NotesCollectionPageViewModel : ViewModelBase
	{
        private readonly IUserDialogs _userDialogs;
        private readonly INoteService _noteService;
        private readonly IAuthenticationService _authenticationService;

        private Member _currentMember;

        public NotesCollectionPageViewModel(
            INavigationService navigationService,
            INoteService noteService,
            IUserDialogs userDialogs,
            IAuthenticationService authenticationService)
            : base(navigationService)
        {
            _noteService = noteService;
            _userDialogs = userDialogs;
            _authenticationService = authenticationService;
        }

        private ObservableCollection<Note> _notesCollection;
        public ObservableCollection<Note> NotesCollection
        {
            get { return _notesCollection; }
            set { SetProperty(ref _notesCollection, value); }
        }

        public ICommand NoteTappedCommand => new DelegateCommand<Note>(OnNoteTappedCommandAsync);
        public ICommand CreateCommand => new DelegateCommand(OnCreateCommandAsync);

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            _currentMember = _authenticationService.CurrentMember;
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            await Task.Yield();

            using (_userDialogs.Loading())
            {
                var allNotes = await GetAllNotesAsync();

                NotesCollection = new ObservableCollection<Note>(allNotes);
            }
        }

        public async Task<IEnumerable<Note>> GetAllNotesAsync()
        {
            var result = new List<Note>();
            var allNotesResult = await _noteService.GetAllAsync(_currentMember.MemberId);

            if (allNotesResult.IsSuccess)
            {
                result = new List<Note>(allNotesResult.SuccessResult);
            }
            else
            {
                await _userDialogs.AlertAsync("Cant get all notes", "Error", "OK");
            }

            return result;
        }
        
        private async void OnNoteTappedCommandAsync(Note tappedNote)
        {
            await NavigationService.NavigateAsync(nameof(NoteView), new NavigationParameters
            {
                { "CurrentNote", tappedNote }
            });
        }

        private async void OnCreateCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(CreateNoteView));
        }
    }
}
