using System;
using System.Collections.Generic;
using System.Text;
using UICore.App.Themes;
using UICore.Navigation;
using UICore.Presentation;

namespace UICore.Pages.Settings
{
    public class SettingsViewModel: Observable,IViewModel
    {
        private readonly IThemeService themeService;

        public SettingsViewModel( IThemeService themeService)
        {
            Name = "Settings";
            this.themeService = themeService;
            IsSingleInstance = true;
        }
        public string Name { get; set; }

        public bool IsSingleInstance { get; private set; }

        public ThemeType CurrentTheme
        {
            get { return themeService.Theme; }
            set { themeService.SetTheme(value); OnPropertyChanged(); }
        }

    }
}
