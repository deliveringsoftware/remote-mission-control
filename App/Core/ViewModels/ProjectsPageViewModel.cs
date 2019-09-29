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

namespace AzureDevops.ViewModels
{
    public class ProjectsPageViewModel : BaseViewModel
    {
        private readonly IAzureDevopsClientService azureDevopsClientService;

        public ProjectsPageViewModel(INavigationService navigationService
            , IPageDialogService pageDialogService
            , IDialogService dialogService
            , ITrackService trackService
            , IAzureDevopsClientService azureDevopsClientService)
            : base(navigationService, pageDialogService, dialogService, trackService)
        {
            Title = Constants.LABEL_PROJECTS;

            this.azureDevopsClientService = azureDevopsClientService;

            LoadProjectFeaturesCommand = new DelegateCommand<Project>(async (project) => await LoadProjectFeatures(project))
                .ObservesCanExecute(() => IsNotBusy);
        }

        private async Task LoadProjectFeatures(Project project)
        {
            var navigationParameters = new NavigationParameters
            {
                { $"{nameof(Project)}", project }
            };

            await navigationService.NavigateAsync($"{nameof(MainPage)}?selectedTab={nameof(PipelinesPage)}",
                navigationParameters);
        }

        private ObservableCollection<Project> projects = new ObservableCollection<Project>();

        public ObservableCollection<Project> Projects
        {
            get => projects;
            set => SetProperty(ref projects, value);
        }

        public ICommand LoadProjectFeaturesCommand { get; }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            trackService.Event("ProjectsPageViewModel.InitializeAsync");

            await ExecuteTask(async () =>
            {
                var result = await azureDevopsClientService.Client.Projects.ListAll();

                if (result.HasError)
                    dialogService.ShowToast($"Error... {result.ErrorDescription}");

                var projects = result.Data.Value.OrderBy(p => p.Name);
                Projects = new ObservableCollection<Project>(projects);
            });
        }
    }
}