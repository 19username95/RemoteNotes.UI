using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemoteNotes.UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditUserView : ContentPage
	{
		public EditUserView ()
		{
			InitializeComponent ();
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}