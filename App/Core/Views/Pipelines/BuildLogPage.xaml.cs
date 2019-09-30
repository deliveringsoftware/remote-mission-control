using Xamarin.Forms;

namespace AzureDevops.Views
{
    public partial class BuildLogPage : ContentPage
    {
        public BuildLogPage()
        {
            InitializeComponent();

            close.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                })
            });
        }
    }
}