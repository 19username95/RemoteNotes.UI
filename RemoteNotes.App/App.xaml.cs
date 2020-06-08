using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism;
using Prism.Ioc;
using RemoteNotes.Service.Authentication;
using RemoteNotes.UI.ViewModels;
using RemoteNotes.UI.Views;
using System.Threading.Tasks;
using RemoteNotes.Service.Client.Contract;
using RemoteNotes.Service.Note;
using RemoteNotes.Service.Storage;
using RemoteNotes.Service.User;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RemoteNotes.App
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

        public App(IPlatformInitializer initializer) 
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
            containerRegistry.RegisterInstance<ISettings>(CrossSettings.Current);
            containerRegistry.RegisterInstance<IMedia>(CrossMedia.Current);
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);

            containerRegistry.Register<IFrontServiceClient, FrontServiceClient>();

            containerRegistry.RegisterInstance<IStorageService>(Container.Resolve<StorageService>());
            containerRegistry.RegisterInstance<IAuthenticationService>(Container.Resolve<AuthenticationService>());
            containerRegistry.RegisterInstance<INoteService>(Container.Resolve<NoteService>());
            containerRegistry.RegisterInstance<IUserService>(Container.Resolve<UserService>());

            // Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<RemoteNotesTabbedPage>();
            containerRegistry.RegisterForNavigation<CreateNoteView, CreateNoteViewModel>();
            containerRegistry.RegisterForNavigation<EditUserView, EditUserViewModel>();
            containerRegistry.RegisterForNavigation<EditNoteView, EditNoteViewModel>();
            containerRegistry.RegisterForNavigation<NoteView, NoteViewModel>();
            containerRegistry.RegisterForNavigation<UserInfoPage, UserInfoPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<NotesCollectionPage, NotesCollectionPageViewModel>();
            containerRegistry.RegisterForNavigation<UsersCollectionPage, UsersCollectionViewModel>();
            containerRegistry.RegisterForNavigation<UserView, UserViewModel>();
        }

        #endregion

        #region -- Methods --

        private async Task SetupNavigationAsync()
        {
            if (AuthenticationService.IsAuthorized)
            {
                if (AuthenticationService.CurrentMember.AccessLevel!=4)
                {
                    await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(RemoteNotesTabbedPage)}");
                }
                else
                {
                    await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(UsersCollectionPage)}");
                }
            }
            else
            {
                var e = await NavigationService.NavigateAsync($"/{nameof(NavigationPage)}/{nameof(LoginPage)}");
            }
        }

        #endregion
    }
}
