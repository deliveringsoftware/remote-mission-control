using AzureDevops.Client.Services.Builds.Models;
using AzureDevops.Services;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace AzureDevops.ViewModels.Pipelines
{
    public class BuildLogPageViewModel : BaseViewModel
    {
        public BuildLogPageViewModel(INavigationService navigationService
            , IPageDialogService pageDialogService
            , IDialogService dialogService
            , ITrackService trackService)
            : base(navigationService, pageDialogService, dialogService, trackService)
        {
        }

        private ObservableCollection<string> logs = new ObservableCollection<string>();

        public ObservableCollection<string> Logs
        {
            get => logs;
            set => SetProperty(ref logs, value);
        }

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            var logKey = $"{nameof(Log)}";

            trackService.Event("BuildLogPageViewModel.InitializeAsync");

            if (parameters.ContainsKey(logKey))
            {
                var theLogs = parameters[logKey] as IEnumerable<string>;
                Logs = new ObservableCollection<string>(theLogs);
            }

            return Task.CompletedTask;
        }
    }
}