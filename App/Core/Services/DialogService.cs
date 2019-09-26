using System;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace AzureDevops.Services
{
    internal class DialogService : IDialogService
    {
        public void ShowAlert(string message, string title, string okText)
            => UserDialogs.Instance
                           .Alert(message, title, okText);

        public void ShowConfirm(string message, string title, string okText, string cancelText, Action<bool> onAction)
            => UserDialogs.Instance
                           .Confirm(new ConfirmConfig
                           {
                               Message = message,
                               Title = title,
                               OnAction = onAction,
                               OkText = okText,
                               CancelText = cancelText
                           });

        public void ShowToast(string message, int duration = 2000)
            => UserDialogs.Instance
                          .Toast(message, new TimeSpan(0, 0, 0, 0, duration));

        public void ShowLoading(string message, Action action)
        {
            using (var loading = UserDialogs.Instance.Loading(message))
            {
                action();
                loading.Hide();
            }
        }

        public async Task ShowLoading(string message, Func<Task> function)
        {
            using (var loading = UserDialogs.Instance.Loading(message))
            {
                await function().ConfigureAwait(false);
                loading.Hide();
            }
        }

        public void ShowDate(string title,
                             Action<DateTime> onAction,
                             string okText,
                             string cancelText,
                             DateTime? selectedDate = null,
                             DateTime? minDate = null,
                             DateTime? maxDate = null)
        {
            var loading = UserDialogs.Instance.DatePrompt(new DatePromptConfig
            {
                Title = title,
                CancelText = cancelText,
                OkText = okText,
                IsCancellable = true,
                OnAction = (c) => onAction(c.SelectedDate),
                SelectedDate = selectedDate,
                MaximumDate = maxDate,
                MinimumDate = minDate
            });
        }

        public void ShowTime(string title,
                             Action<TimeSpan> onAction,
                             string okText,
                             string cancelText,
                             TimeSpan? selectedTime = null,
                             int? minMinutesTimeOfDay = null,
                             int? maxMinutesTimeOfDay = null)
        {
            var loading = UserDialogs.Instance.TimePrompt(new TimePromptConfig
            {
                Title = title,
                CancelText = cancelText,
                OkText = okText,
                IsCancellable = true,
                Use24HourClock = true,
                MinuteInterval = 1,
                SelectedTime = selectedTime,
                OnAction = (c) => onAction(c.SelectedTime),
                MaximumMinutesTimeOfDay = maxMinutesTimeOfDay,
                MinimumMinutesTimeOfDay = minMinutesTimeOfDay
            });
        }
    }
}
