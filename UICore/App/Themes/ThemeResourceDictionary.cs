using System.Windows;

namespace UICore.App.Themes
{
    public class ThemeResourceDictionary : ResourceDictionary
    {
        public ThemeResourceDictionary()
        {
            MergedDictionaries.Add(ThemeService.ResourceDictionary);
        }
    }
}
