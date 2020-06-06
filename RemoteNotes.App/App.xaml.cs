using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism;
using Prism.Ioc;
using RemoteNotes.Service;
using RemoteNotes.Service.Authentication;
using RemoteNotes.Service.Client.Stub;
using RemoteNotes.UI.ViewModels;
using RemoteNotes.UI.Views;
using System.Threading.Tasks;
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
            containerRegistry.Register<HubModule>();
            containerRegistry.Register<ServiceModule>();

            containerRegistry.RegisterInstance<ISettings>(CrossSettings.Current);
            containerRegistry.RegisterInstance<IMedia>(CrossMedia.Current);
            containerRegistry.RegisterInstance<IUserDialogs>(UserDialogs.Instance);

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
