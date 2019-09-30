using AzureDevops.Services;
using Prism.Navigation;
using Prism.Services;

namespace AzureDevops.ViewModels.Board
{
    public class BoardPageViewModel : BaseViewModel
    {
        public BoardPageViewModel(INavigationService navigationService
            , IPageDialogService pageDialogService
            , IDialogService dialogService
            , ITrackService trackService) :
            base(navigationService, pageDialogService, dialogService, trackService)
        {
            Title = Constants.LABEL_BOARD;
        }
    }
}