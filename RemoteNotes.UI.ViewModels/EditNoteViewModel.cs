using System;
using System.IO;
using System.Windows.Input;
using Acr.UserDialogs;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using RemoteNotes.Service.Domain.Data;
using RemoteNotes.Service.Note;
using Xamarin.Forms;

namespace RemoteNotes.UI.ViewModels
{
    public class EditNoteViewModel : ViewModelBase
    {
        private readonly INoteService _noteService;
        private readonly IUserDialogs _userDialogs;
        private readonly IMedia _mediaService;

        private Note _currentNote;
        private byte[] _photoBytes;

        public EditNoteViewModel(
            INavigationService navigationService,
            INoteService noteService,
            IUserDialogs userDialogs,
            IMedia mediaService) 
            : base(navigationService)
        {
            _userDialogs = userDialogs;
            _noteService = noteService;
            _mediaService = mediaService;
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

        public ICommand SaveCommand => new DelegateCommand(OnSaveCommandAsync);
        public ICommand CancelCommand => new DelegateCommand(OnCancelCommandAsync);
        public ICommand PickImageCommand => new DelegateCommand(OnPickImageCommandAsync);

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);

            if (parameters.TryGetValue("CurrentNote", out Note note))
            {
                _currentNote = note;
                _photoBytes = note.Photo;
                Topic = note.Topic;
                Text = note.Text;
                Photo = note.PhotoSource;
            }
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

        private async void OnSaveCommandAsync()
        {
            _currentNote.Text = Text;
            _currentNote.Topic = Topic;
            _currentNote.Photo = _photoBytes;
            _currentNote.ModifyTime = DateTime.Now;

            var savingResult = await _noteService.SaveAsync(_currentNote);

            if (savingResult.IsSuccess)
            {
                await _userDialogs.AlertAsync("Saving success!", "Info", "OK");
                await NavigationService.GoBackAsync(new NavigationParameters
                {
                    { "CurrentNote", savingResult.SuccessResult }
                });
            }
            else
            {
                await _userDialogs.AlertAsync("Saving failure!", "Error", "OK");
            }
        }

        private async void OnCancelCommandAsync()
        {
            await NavigationService.GoBackAsync();
        }
    }
}
