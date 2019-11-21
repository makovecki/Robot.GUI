using Robot.UI.FindEsp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using UICore.Presentation;

namespace Robot.UI.ESPController.Settings
{
    public class SettingsViewModel : Observable
    {
        public SettingsViewModel(ESP esp)
        {
            name = esp.Name;
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

    }
}
