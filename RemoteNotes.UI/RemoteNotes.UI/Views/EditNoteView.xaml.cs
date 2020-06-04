﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemoteNotes.UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditNoteView : ContentPage
	{
		public EditNoteView ()
		{
			InitializeComponent ();
			NavigationPage.SetHasNavigationBar(this, false);
		}
	}
}