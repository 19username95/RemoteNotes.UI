using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism;
using Prism.Ioc;
using RemoteNotes.UI.Hubs.Authentication;
using RemoteNotes.UI.Hubs.Notes;
using RemoteNotes.UI.Hubs.User;
using RemoteNotes.UI.Service.Authentication;
using RemoteNotes.UI.Service.Note;
using RemoteNotes.UI.Service.Storage;
using RemoteNotes.UI.ViewModels;
using RemoteNotes.UI.Views;
using System.Threading.Tasks;
using RemoteNotes.UI.Service.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RemoteNotes.UI
{
    public partial class App
    {
        public static T Resolve<T>()
        {
            return Current.Container.Resolve<T>();
        }

        #region -- Services --

        private IAuthenticationService _authenticationService;
        public IAuthenticationService AuthenticationService => _authenticationService ?? (_authenticationService = Container.Resolve<IAuthenticationService>());

        #endregion

        public App(
            IPlatformInitializer initializer) 
            : base(initializer)
        {
        }

        #region -- Overrides --

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await SetupNavigationAsync();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //  Storage services
            containerRegistry.RegisterInstance<ISettings>(CrossSettings.Current);
            containerRegistry.RegisterInstance<IStorageService>(Container.Resolve<StorageService>());

            // Hubs
            containerRegistry.RegisterInstance<IUserHub>(Container.Resolve<UserHub>());
            containerRegistry.RegisterInstance<INotesHub>(Container.Resolve<NotesHub>());
            containerRegistry.RegisterInstance<IAuthenticationHub>(Container.Resolve<AuthenticationHub>());

            // Services
            containerRegistry.RegisterInstance<IMedia>(CrossMedia.Current);
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<INoteService>(Container.Resolve<NoteService>());
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());

            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CreateNoteView, CreateNoteViewModel>();
            containerRegistry.RegisterForNavigation<EditUserView, EditUserViewModel>();
            containerRegistry.RegisterForNavigation<EditNoteView, EditNoteViewModel>();
            containerRegistry.RegisterForNavigation<NoteView, NoteViewModel>();
            containerRegistry.RegisterForNavigation<RemoteNotesTabbedPage, RemoteNotesTabbedPageViewModel>();
            containerRegistry.RegisterForNavigation<UserInfoPage, UserInfoPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<NotesCollectionPage, NotesCollectionPageViewModel>();
        }

        #endregion

        #region -- Methods --

        private async Task SetupNavigationAsync()
        {
            if (AuthenticationService.IsAuthorized)
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(RemoteNotesTabbedPage)}");
            }
            else
            {
                await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}");
            }
        }

        #endregion
    }
}
