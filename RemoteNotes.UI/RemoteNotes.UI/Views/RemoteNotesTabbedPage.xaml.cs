using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace RemoteNotes.UI.Views
{
    public partial class RemoteNotesTabbedPage : Xamarin.Forms.TabbedPage
    {
        public RemoteNotesTabbedPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom)
                         .SetBarItemColor(Color.Black)
                         .SetBarSelectedItemColor(Color.Red);
        }
    }
}
