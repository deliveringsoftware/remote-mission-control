using AzureDevops.Client.Services.Projects.Models;
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
using Xamarin.Forms;

namespace AzureDevops.ViewModels
{
    public class PersonalAccessTokenLoginPageViewModel : BaseViewModel
    {

        private readonly IAzureDevopsClientService azureDevopsClientService;

        private string _organization;
        private string _personalAccessToken;

        public PersonalAccessTokenLoginPageViewModel(INavigationService navigationService
            , IPageDialogService pageDialogService
            , IDialogService dialogService
            , ITrackService trackService
            , IAzureDevopsClientService azureDevopsClientService)
            : base(navigationService, pageDialogService, dialogService, trackService)
        {
            Title = Constants.LABEL_LOGIN;

            LoginCommand = new DelegateCommand(async () => await Login())
                .ObservesCanExecute(() => IsNotBusy);

            OpenUrlCommand = new DelegateCommand(async () => await OpenUrl())
                .ObservesCanExecute(() => IsNotBusy);
            this.azureDevopsClientService = azureDevopsClientService;
        }

        public string Organization
        {
            get => this._organization;
            set => this.SetProperty(ref this._organization, value);
        }

        public string PersonalAccessToken
        {
            get => this._personalAccessToken;
            set => this.SetProperty(ref this._personalAccessToken, value);
        }

        public ICommand LoginCommand { get; }
        public ICommand OpenUrlCommand { get; }


        private async Task Login()
        {
            await ExecuteTask(async () =>
            {
                azureDevopsClientService.RegisterAzureDevopsClient(this.Organization, this.PersonalAccessToken);

                var result = await azureDevopsClientService.Client.Projects.ListAll();

                if (result.HasError)
                    dialogService.ShowToast($"Error... {result.ErrorDescription}");
                else
                {
                    await NavigateToProjectsPage();
                }

            }, Constants.LABEL_LOADING, "PersonalAccessTokenLoginViewModel.Login");
        }

        private Task NavigateToProjectsPage()
            => navigationService.NavigateAsync($"../{nameof(ProjectsPage)}");

        private Task OpenUrl()
            => Browser.OpenAsync(new Uri(Constants.URL_PERSONAL_ACCESS_TOKEN), BrowserLaunchMode.SystemPreferred);

        private bool CanExecuteLogin()
            => !string.IsNullOrEmpty(Organization) &&
               IsNotBusy &&
               !string.IsNullOrEmpty(PersonalAccessToken);
    }
}
