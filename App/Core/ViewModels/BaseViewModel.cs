using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureDevops.Services;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace AzureDevops.ViewModels
{
    public abstract class BaseViewModel : BindableBase, IInitializeAsync
    {
        protected readonly INavigationService navigationService;
        protected readonly IPageDialogService pageDialogService;
        protected readonly IDialogService dialogService;
        protected readonly ITrackService trackService;

        private bool isLoaded;

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

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }


        public async Task Loading()
        {
            if (isLoaded) return;
            isLoaded = true;
            await this.OnLoading();
        }

        public async Task Unloading()
        {
            if (isLoaded)
                await this.OnUnloading();
        }

        protected virtual Task OnLoading() => Task.CompletedTask;
        protected virtual Task OnUnloading() => Task.CompletedTask;

        protected async Task ExecuteTask(Func<Task> task,
                                         string text = "Loading",
                                         string eventName = "",
                                         IDictionary<string, string> properties = null)
        {

            await Task.CompletedTask;

            await dialogService.ShowLoading(text, async () =>
            {
                try
                {
                    if (isBusy)
                        return;

                    IsBusy = true;

                    if (!string.IsNullOrEmpty(eventName))
                        trackService.Event(eventName, properties);

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
