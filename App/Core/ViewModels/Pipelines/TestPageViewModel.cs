using System.Threading.Tasks;
using AzureDevops.Client.Services.Projects.Models;
using AzureDevops.Services;
using Prism.Navigation;
using Prism.Services;

namespace AzureDevops.ViewModels.Pipelines
{
    public class TestPageViewModel : BaseViewModel
    {
        private readonly Project _project;

        public TestPageViewModel(INavigationService navigationService
            , IPageDialogService pageDialogService
            , IDialogService dialogService, ITrackService trackService
            , Project project)
            : base(navigationService, pageDialogService, dialogService, trackService)
        {
            Title = Constants.LABEL_TESTS;
        }

        private Project project;
        public Project Project
        {
            get => project;
            set => SetProperty(ref project, value);
        }

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            var projectKey = $"{nameof(Project)}";

            if (parameters.ContainsKey(projectKey))
            {
                Project = parameters[projectKey] as Project;
            }
        }
    }
}
