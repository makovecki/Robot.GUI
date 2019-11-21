using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace UICore.App.Themes
{
    public interface IThemeService
    {
        ThemeType Theme { get; }
        void SetTheme(ThemeType theme);
    }
}
