using System;
using System.Windows;
using System.Windows.Media;

namespace UICore.App.Themes
{
    public class ThemeService : IThemeService
    {
        [ThreadStatic]
        private static ResourceDictionary? resourceDictionary;

        public ThemeType Theme { get; private set; }

        public ThemeService()
        {
            
        }

        private static void LoadThemeResources(ThemeType theme)
        {
            SetResource(ThemeResourceKey.ControlDisabledOpacity.ToString(), 0.4d);
            SetResource(ThemeResourceKey.Border.ToString(), new SolidColorBrush(ColorFromHex("#FFCCCCCC")));
            switch (theme)
            {
                case ThemeType.Light:
                    SetResource(ThemeResourceKey.Foreground.ToString(), ColorFromHex("#FF333333"));
                    SetResource(ThemeResourceKey.Background.ToString(), ColorFromHex("#FFFFFFFF"));
                    
                    break;
                case ThemeType.Dark:
                    SetResource(ThemeResourceKey.Foreground.ToString(), ColorFromHex("#FFEEEEEE"));
                    SetResource(ThemeResourceKey.Background.ToString(), ColorFromHex("#FF222529"));
                    break;
                default:
                    break;
            }
            SetResource(ThemeResourceKey.ContentBackground.ToString(), new SolidColorBrush((Color)(GetResource(ThemeResourceKey.Background)?? Colors.Transparent)));
            SetResource(ThemeResourceKey.ContentForeground.ToString(), new SolidColorBrush((Color)(GetResource(ThemeResourceKey.Foreground)?? Colors.Transparent)));
        }

        internal static Color ColorFromHex(string colorHex)
        {
            return ((Color?)ColorConverter.ConvertFromString(colorHex)) ?? Colors.Transparent;
        }
        public static object? GetResource(ThemeResourceKey resourceKey)
        {
            return resourceDictionary?.Contains(resourceKey.ToString())==true ? ResourceDictionary[resourceKey.ToString()] : null;
        }
        private static void SetResource(object key, object resource)
        {
            if (resourceDictionary!= null) resourceDictionary[key] = resource;
        }

        internal static ResourceDictionary ResourceDictionary
        {
            get
            {
                if (resourceDictionary != null)
                {
                    return resourceDictionary;
                }

                resourceDictionary = new ResourceDictionary();
                LoadThemeResources(ThemeType.Light);
                return resourceDictionary;
            }
        }

        public void SetTheme(ThemeType theme)
        {
            LoadThemeResources(theme);
            Theme = theme;
        }
    }
}
