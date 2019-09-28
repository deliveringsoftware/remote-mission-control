using AzureDevops.Services;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;

namespace AzureDevops.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel(INavigationService navigationService
            , IPageDialogService pageDialogService
            , IDialogService dialogService
            , ITrackService trackService)
            : base(navigationService, pageDialogService, dialogService, trackService)
        {
        }

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            return base.InitializeAsync(parameters);
        }
    }
}