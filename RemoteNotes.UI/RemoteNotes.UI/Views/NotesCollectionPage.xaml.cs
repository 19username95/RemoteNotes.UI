using Xamarin.Forms;

namespace RemoteNotes.UI.Views
{
    public partial class NotesCollectionPage : ContentPage
    {
        public NotesCollectionPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
