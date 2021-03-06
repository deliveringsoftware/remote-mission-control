﻿using AzureDevops.Client.Services.Projects.Models;
using AzureDevops.Services;
using Prism.Navigation;
using Prism.Services;
using System.Threading.Tasks;

namespace AzureDevops.ViewModels.Repos
{
    public class ReposPageViewModel : BaseViewModel
    {
        public ReposPageViewModel(INavigationService navigationService
            , IPageDialogService pageDialogService
            , IDialogService dialogService
            , ITrackService trackService)
            : base(navigationService, pageDialogService, dialogService, trackService)
        {
            Title = Constants.LABEL_REPOS;
        }

        private Project project;

        public Project Project
        {
            get => project;
            set => SetProperty(ref project, value);
        }

        public override Task InitializeAsync(INavigationParameters parameters)
        {
            var projectKey = $"{nameof(Project)}";

            if (parameters.ContainsKey(projectKey))
            {
                Project = parameters[projectKey] as Project;
            }

            return Task.CompletedTask;
        }
    }
}