using AzureDevops.Client.Services.Builds.Models;
using AzureDevops.Client.Services.Projects.Models;
using AzureDevops.Models;
using AzureDevops.Services;
using AzureDevops.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AzureDevops.ViewModels.Pipelines
{
    public class BuildDetailsPageViewModel : BaseViewModel
    {
        private readonly IAzureDevopsClientService azureDevopsClientService;

        public BuildDetailsPageViewModel(INavigationService navigationService
            , IPageDialogService pageDialogService
            , IDialogService dialogService
            , ITrackService trackService
            , IAzureDevopsClientService azureDevopsClientService)
            : base(navigationService, pageDialogService, dialogService, trackService)
        {
            ShowLogsCommand = new DelegateCommand<Job>(async (job) => await ShowLogs(job))
                .ObservesCanExecute(() => IsNotBusy);

            this.azureDevopsClientService = azureDevopsClientService;

            this.RefreshJobsCommand = new DelegateCommand(async () => await LoadTimeline());
        }

        private Build build;

        public Build Build
        {
            get => build;
            set => SetProperty(ref build, value);
        }

        private Project project;

        public Project Project
        {
            get => project;
            set => SetProperty(ref project, value);
        }

        public DelegateCommand RefreshJobsCommand { get; }

        private ObservableCollection<Job> jobs = new ObservableCollection<Job>();

        public ObservableCollection<Job> Jobs
        {
            get => jobs;
            set => SetProperty(ref jobs, value);
        }

        public ICommand ShowLogsCommand { get; }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            trackService.Event("BuildDetailsPageViewModel.InitializeAsync");

            var projectKey = $"{nameof(Project)}";
            var buildKey = $"{nameof(Build)}";

            if (parameters.ContainsKey(projectKey) && parameters.ContainsKey(buildKey))
            {
                Project = parameters[projectKey] as Project;
                Build = parameters[buildKey] as Build;

                Title = string.Format(Constants.LABEL_BUILD_VERSION, Build.BuildNumber);

                await LoadTimeline();
            }
        }

        private async Task LoadTimeline()
        {
            trackService.Event("BuildDetailsPageViewModel.LoadTimeline");

            await ExecuteTask(async () => {

                var result = await azureDevopsClientService.Client.Builds.GetTimeline(Project.Name, Build.Id);

                if (!result.HasError)
                {
                    if (result.Data is null)
                    {
                        dialogService.ShowToast(Constants.ERROR_MSG_NO_BUILDS_LOGS);
                    }
                    else
                    {
                        Jobs = new ObservableCollection<Job>(Job.CreateJobs(result.Data.Records));
                    }
                }
                else
                {
                    dialogService.ShowToast(Constants.ERROR_MSG_UNABLE_TO_LOAD_JOBS);
                }
            });
        }

        private async Task ShowLogs(Job job)
        {
            trackService.Event("BuildDetailsPageViewModel.ShowLogs");

            if (!job.LogId.HasValue)
            {
                return;
            }

            await ExecuteTask(async () =>
            {
                var result = await azureDevopsClientService.Client.Builds.GetLogs(Project.Name, Build.Id, job.LogId.Value);

                if (!result.HasError)
                {
                    var navigationParameters = new NavigationParameters
                    {
                        { $"{nameof(Log)}", result.Data.Value },
                    };

                    await navigationService.NavigateAsync($"{nameof(BuildLogPage)}", navigationParameters, true);
                }
                else
                {
                    dialogService.ShowToast(Constants.ERROR_MSG_UNABLE_TO_LOAD_LOGS);
                }
            });
        }
    }
}