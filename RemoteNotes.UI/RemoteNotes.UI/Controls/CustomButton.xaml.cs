using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemoteNotes.UI.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomButton : ContentView
    {
        public CustomButton()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty ButtonLabelProperty = BindableProperty.Create(
            nameof(ButtonLabel),
            typeof(string),
            typeof(CustomButton),
            default(string),
            BindingMode.OneWay);
        public string ButtonLabel
        {
            get => (string)GetValue(ButtonLabelProperty);
            set => SetValue(ButtonLabelProperty, value);
        }

        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create(nameof(TappedCommand), typeof(ICommand), typeof(CustomButton), null);
        public ICommand TappedCommand
        {
            get { return (ICommand)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ButtonLabelProperty.PropertyName)
            {
                buttonLabel.Text = ButtonLabel;
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            // just a little animation :)
            await this.ScaleTo(0.95, 60);
            await this.ScaleTo(1, 60);

            TappedCommand?.Execute(BindingContext);
        }
    }
}