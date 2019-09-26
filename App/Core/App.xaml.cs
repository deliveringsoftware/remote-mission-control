using System;
using System.Threading.Tasks;
using AzureDevops.Services;
using AzureDevops.ViewModels;
using AzureDevops.ViewModels.Board;
using AzureDevops.ViewModels.Pipelines;
using AzureDevops.ViewModels.Repos;
using AzureDevops.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;

namespace AzureDevops
{
    public partial class App : PrismApplication
    {
        public App() : this(null)
        {
        }

        public App(IPlatformInitializer platformInitializer)
            : base(platformInitializer, true)
        {
        }

        public App(IPlatformInitializer platformInitializer, bool setFormsDependencyResolver)
            : base(platformInitializer, setFormsDependencyResolver)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            await InitializeApplication();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterNavigation(containerRegistry);
            RegisterServices(containerRegistry);
        }

        protected override void OnStart()
        {
            RegisterAppCenter();
            base.OnStart();
        }

        private async Task InitializeApplication()
        {
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(PersonalAccessTokenLoginPage)}");
        }

        private void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IAzureDevopsClientService>(new AzureDevopsClientService());
            containerRegistry.Register<IDialogService, DialogService>();
            containerRegistry.Register<ITrackService, TrackService>();
        }

        private void RegisterNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterForNavigation<ProjectsPage, ProjectsPageViewModel>();
            containerRegistry.RegisterForNavigation<PersonalAccessTokenLoginPage, PersonalAccessTokenLoginPageViewModel>();
            containerRegistry.RegisterForNavigation<BoardPage, BoardPageViewModel>();
            containerRegistry.RegisterForNavigation<ReposPage, ReposPageViewModel>();
            containerRegistry.RegisterForNavigation<BuildLogPage, BuildLogPageViewModel>();
            containerRegistry.RegisterForNavigation<BuildDetailsPage, BuildDetailsPageViewModel>();
            containerRegistry.RegisterForNavigation<PipelinesPage, PipelinesPageViewModel>();
            containerRegistry.RegisterForNavigation<TestPage, TestPageViewModel>();

        }

        private void RegisterAppCenter()
        {
            AppCenter.Start($"android={Constants.APPCENTER_ANDROID};ios={Constants.APPCENTER_IOS};",
                            typeof(Analytics),
                            typeof(Crashes));
        }
    }
}
