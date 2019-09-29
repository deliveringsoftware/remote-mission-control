using AzureDevops.Client.Services.Builds.Models;
using AzureDevops.Client.Services.Definitions.Models;
using AzureDevops.Client.Services.Projects.Models;
using AzureDevops.Services;
using AzureDevops.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AzureDevops.ViewModels.Pipelines
{
    public class PipelinesPageViewModel : BaseViewModel
    {
        private readonly IAzureDevopsClientService azureDevopsClientService;

        public PipelinesPageViewModel(INavigationService navigationService
            , IPageDialogService pageDialogService
            , IDialogService dialogService
            , ITrackService trackService
            , IAzureDevopsClientService azureDevopsClientService)
            : base(navigationService, pageDialogService, dialogService, trackService)
        {
            Title = Constants.LABEL_PIPELINES;

            this.azureDevopsClientService = azureDevopsClientService;

            RefreshBuildsCommand = new DelegateCommand(async () => await RefreshBuilds())
                .ObservesCanExecute(() => IsNotBusy);

            QueueBuildCommand = new DelegateCommand(async () => await QueueBuild())
                .ObservesCanExecute(() => IsNotBusy);

            ShowBuildDetailsCommand = new DelegateCommand<Build>(async (build) => await ShowBuildDetails(build))
                  .ObservesCanExecute(() => IsNotBusy);
        }

        private Definition definition;

        public Definition Definition
        {
            get => definition;
            set => SetProperty(ref definition, value);
        }

        private Project project;

        public Project Project
        {
            get => project;
            set => SetProperty(ref project, value);
        }

        private ObservableCollection<Definition> definitions = new ObservableCollection<Definition>();

        public ObservableCollection<Definition> Definitions
        {
            get => definitions;
            set => SetProperty(ref definitions, value);
        }

        private ObservableCollection<Build> builds = new ObservableCollection<Build>();

        public ObservableCollection<Build> Builds
        {
            get => builds;
            set => SetProperty(ref builds, value);
        }

        public ICommand RefreshBuildsCommand { get; }
        public ICommand QueueBuildCommand { get; }
        public ICommand ShowBuildDetailsCommand { get; }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            var projectKey = $"{nameof(Project)}";

            trackService.Event("PipelinesViewModel.InitializeAsync");

            if (parameters.ContainsKey(projectKey))
            {
                Project = parameters[projectKey] as Project;

                await ExecuteTask(async () => await LoadDefinitions());
            }
        }

        private async Task RefreshBuilds()
        {
            trackService.Event("PipelinesViewModel.RefreshBuilds");
            await ExecuteTask(async () => await LoadBuilds());
        }

        private async Task QueueBuild()
        {
            trackService.Event("PipelinesViewModel.QueueBuil");

            await ExecuteTask(async () =>
            {
                var result = await azureDevopsClientService.Client.Builds.Queue(Definition);
                if (result.HasError)
                    dialogService.ShowToast($"Error... {result.ErrorDescription}");
                else
                    Builds.Insert(0, result.Data);
            });
        }

        private async Task LoadDefinitions()
        {
            var result = await azureDevopsClientService.Client.Definitions.ListAll(Project.Name);

            if (result.HasError)
                dialogService.ShowToast($"Error... {result.ErrorDescription}");
            else
            {
                Definitions = new ObservableCollection<Definition>(result.Data.Value.OrderByDescending(d => d.Id));
                Definition = Definitions.FirstOrDefault();

                await LoadBuilds();
            }
        }

        private async Task LoadBuilds()
        {
            var result = await azureDevopsClientService.Client.Builds.ListAll(Project.Name, Definition?.Id);
            if (result.HasError)
                dialogService.ShowToast($"Error... {result.ErrorDescription}");
            else
            {
                Builds = new ObservableCollection<Build>(result.Data.Value);
            }
        }

        private async Task ShowBuildDetails(Build build)
        {
            var navigationParameters = new NavigationParameters
            {
                { $"{nameof(Project)}", project },
                { $"{nameof(Build)}", build }
            };

            await navigationService.NavigateAsync($"{nameof(BuildDetailsPage)}", navigationParameters);
        }
    }
}