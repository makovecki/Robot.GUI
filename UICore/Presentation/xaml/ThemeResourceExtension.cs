using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using UICore.App.Themes;

namespace UICore.Presentation.xaml
{
    public sealed class ThemeResourceExtension : DynamicResourceExtension
    {
        public new ThemeResourceKey ResourceKey
        {
            get
            {
                Enum.TryParse(base.ResourceKey.ToString(), out ThemeResourceKey resourceKey);
                return resourceKey;
            }
            set => base.ResourceKey = value.ToString();
        }
    }
}
