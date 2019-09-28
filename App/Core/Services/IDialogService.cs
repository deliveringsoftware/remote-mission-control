using System;
using System.Threading.Tasks;

namespace AzureDevops.Services
{
    public interface IDialogService
    {
        void ShowAlert(string message, string title, string okText);

        void ShowConfirm(string message, string title, string okText, string cancelText, Action<bool> onAction);

        void ShowToast(string message, int duration = 2000);

        void ShowLoading(string message, Action action);

        Task ShowLoading(string message, Func<Task> function);

        void ShowDate(string title,
                      Action<DateTime> onAction,
                      string okText,
                      string cancelText,
                      DateTime? selectedDate = null,
                      DateTime? minDate = null,
                      DateTime? maxDate = null);

        void ShowTime(string title,
                             Action<TimeSpan> onAction,
                             string okText,
                             string cancelText,
                             TimeSpan? selectedTime = null,
                             int? minMinutesTimeOfDay = null,
                             int? maxMinutesTimeOfDay = null);
    }
}