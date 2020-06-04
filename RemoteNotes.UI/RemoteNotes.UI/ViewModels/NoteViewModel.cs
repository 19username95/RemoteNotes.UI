using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Prism.Commands;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using RemoteNotes.UI.Model;
using RemoteNotes.UI.Service.Note;
using RemoteNotes.UI.Views;
using Xamarin.Forms;

namespace RemoteNotes.UI.ViewModels
{
    public class NoteViewModel : ViewModelBase
    {
        private readonly INoteService _noteService;
        private readonly IUserDialogs _userDialogs;

        private Note _currentNote;
        
        public NoteViewModel(
            INavigationService navigationService,
            IUserDialogs userDialogs,
            INoteService noteService) 
            : base(navigationService)
        {
            _noteService = noteService;
            _userDialogs = userDialogs;
        }

        private string _topic;
        public string Topic
        {
            get => _topic;
            set => SetProperty(ref _topic, value);
        }

        private string _text;
        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        private ImageSource _photo;
        public ImageSource Photo
        {
            get => _photo;
            set => SetProperty(ref _photo, value);
        }
        
        private DateTime _modifyTime;
        public DateTime ModifyTime
        {
            get => _modifyTime;
            set => SetProperty(ref _modifyTime, value);
        }
        
        private DateTime _PublishTime;
        public DateTime PublishTime
        {
            get => _PublishTime;
            set => SetProperty(ref _PublishTime, value);
        }

        public ICommand EditCommand => new DelegateCommand(OnEditCommandAsync);
        public ICommand DeleteCommand => new DelegateCommand(OnDeleteCommandAsync);

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);

            if (parameters.TryGetValue("CurrentNote", out Note note))
            {
                _currentNote = note;
                Photo = note.PhotoSource;
                Text = note.Text;
                Topic = note.Topic;
                PublishTime = note.PublishTime;
                ModifyTime = note.ModifyTime;
            }
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.TryGetValue("CurrentNote", out Note note))
            {
                _currentNote = note;
                Photo = note.PhotoSource;
                Text = note.Text;
                Topic = note.Topic;
                PublishTime = note.PublishTime;
                ModifyTime = note.ModifyTime;
            }
        }

        private async void OnEditCommandAsync()
        {
            await NavigationService.NavigateAsync(nameof(EditNoteView), new Prism.Navigation.NavigationParameters
            {
                { "CurrentNote", _currentNote }
            });
        }

        private async void OnDeleteCommandAsync()
        {
            var deletingResult = await _noteService.RemoveAsync(_currentNote.Id);

            if (deletingResult.IsSuccess)
            {
                await _userDialogs.AlertAsync("Delete success!", "Info", "Ok");
                await NavigationService.GoBackAsync();
            }
            else
            {
                await _userDialogs.AlertAsync("Delete failure!", "Error", "Ok");
            }
        }
    }
}
