using AzureDevops.Client.Services.Projects.Models;
using AzureDevops.Services;
using AzureDevops.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AzureDevops.ViewModels
{
    public class ProjectsPageViewModel : BaseViewModel
    {
        public ProjectsPageViewModel(INavigationService navigationService
            , IPageDialogService pageDialogService
            , IDialogService dialogService
            , ITrackService trackService)
            : base(navigationService, pageDialogService, dialogService, trackService)
        {
            Title = Constants.LABEL_PROJECTS;

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

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            trackService.Event("ProjectsPageViewModel.InitializeAsync");

            var projects = parameters["Projects"] as IEnumerable<Project>;
            
            if (projects != null)
            {
                Projects = new ObservableCollection<Project>(projects);
            }

            return Task.CompletedTask;
        }
    }
}