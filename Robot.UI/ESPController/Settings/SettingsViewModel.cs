using Robot.UI.FindEsp.Model;
using Robot.UI.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UICore.Presentation;

namespace Robot.UI.ESPController.Settings
{
    public class SettingsViewModel : Observable
    {
        public SettingsViewModel(ESP esp, IESPMessageService messageService)
        {
            name = esp.Name;
            SendCommand = Make.UICommand.Do(() => SendDataToEPS(esp, messageService));
        }

        private void SendDataToEPS(ESP esp, IESPMessageService messageService)
        {
            messageService.SaveDataAsync(esp, Name);
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        public ICommand SendCommand { get; set; }
    }
}
