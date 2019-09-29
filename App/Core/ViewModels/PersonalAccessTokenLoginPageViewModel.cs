using AzureDevops.Services;
using AzureDevops.Views;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;

namespace AzureDevops.ViewModels
{
    public class PersonalAccessTokenLoginPageViewModel : BaseViewModel
    {
        private readonly IAzureDevopsClientService azureDevopsClientService;

        public PersonalAccessTokenLoginPageViewModel(
              INavigationService navigationService
            , IPageDialogService pageDialogService
            , IDialogService dialogService
            , ITrackService trackService
            , IAzureDevopsClientService azureDevopsClientService)
            : base(navigationService, pageDialogService, dialogService, trackService)
        {
            Title = Constants.LABEL_LOGIN;

            LoginCommand = new DelegateCommand(async () => await Login())
                .ObservesProperty(() => IsNotBusy)
                .ObservesProperty(() => Organization)
                .ObservesProperty(() => PersonalAccessToken)
                .ObservesCanExecute(() => CanExecuteLogin);

            OpenUrlCommand = new DelegateCommand(async () => await OpenUrl(), () => IsNotBusy);

            this.azureDevopsClientService = azureDevopsClientService;
        }

        private string organization;

        public string Organization
        {
            get => organization;
            set => SetProperty(ref organization, value);
        }

        private string personalAccessToken;

        public string PersonalAccessToken
        {
            get => personalAccessToken;
            set => SetProperty(ref personalAccessToken, value);
        }

        private bool CanExecuteLogin
            => IsNotBusy &&
               !string.IsNullOrEmpty(Organization) &&
               !string.IsNullOrEmpty(PersonalAccessToken);

        public ICommand LoginCommand { get; }
        public ICommand OpenUrlCommand { get; }

        private async Task Login()
        {
            trackService.Event("PersonalAccessTokenLoginViewModel.Login");

            await ExecuteTask(async () =>
            {
                azureDevopsClientService.RegisterAzureDevopsClient(Organization, PersonalAccessToken);

                var result = await azureDevopsClientService.Client.Projects.ListAll();

                if (!result.HasError)
                {
                    await NavigateToProjectsPage(result.Data.Value.OrderByDescending(p => p.LastUpdateTime));
                }
                else
                {
                    dialogService.ShowToast(Constants.ERROR_MSG_PAT_UNABLE_TO_CONNECT_TO_AZURE_DEVOPS);
                }
            });
        }

        private async Task NavigateToProjectsPage(IEnumerable<Client.Services.Projects.Models.Project> projects)
        {
            var parameters = new NavigationParameters();
            parameters.Add("Projects", projects);
            await navigationService.NavigateAsync($"../{nameof(ProjectsPage)}", parameters);
        }

        private Task OpenUrl()
            => Browser.OpenAsync(new Uri(Constants.URL_PERSONAL_ACCESS_TOKEN), BrowserLaunchMode.SystemPreferred);
    }
}