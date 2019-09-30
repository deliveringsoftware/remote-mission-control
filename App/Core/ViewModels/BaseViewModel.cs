using AzureDevops.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;

namespace AzureDevops.ViewModels
{
    public abstract class BaseViewModel : BindableBase, IInitializeAsync
    {
        protected readonly INavigationService navigationService;
        protected readonly IPageDialogService pageDialogService;
        protected readonly IDialogService dialogService;
        protected readonly ITrackService trackService;

        protected BaseViewModel(INavigationService navigationService
           , IPageDialogService pageDialogService
           , IDialogService dialogService
           , ITrackService trackService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            this.dialogService = dialogService;
            this.trackService = trackService;
        }

        private bool isBusy;

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public bool IsNotBusy => !IsBusy;

        private string title;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected async Task ExecuteTask(Func<Task> task)
        {
            await dialogService.ShowLoading(Constants.LABEL_LOADING, async () =>
            {
                try
                {
                    IsBusy = true;

                    await task();
                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.Write(ex);
#endif
                    trackService.Error(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }

        public virtual Task InitializeAsync(INavigationParameters parameters)
            => Task.CompletedTask;
    }
}