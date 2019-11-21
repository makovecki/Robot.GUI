using Robot.UI.ESPController.Settings;
using Robot.UI.FindEsp.Model;
using System;
using System.Collections.Generic;
using System.Text;
using UICore.IoC;
using UICore.Navigation;
using UICore.Presentation;

namespace Robot.UI.ESPController
{
    public class ESPControllerViewModel : Observable, IViewModel
    {
        public ESPControllerViewModel(ESP esp, IIoCService ioc)
        {
            Name = "Controller for " + esp.Name;
            SettingsVM = ioc.ResolveTemporaryType<SettingsViewModel>(esp);
        }
        public string Name { get; private set; }

        public SettingsViewModel SettingsVM { get; private set; }

        public bool IsSingleInstance => false;
    }
}
