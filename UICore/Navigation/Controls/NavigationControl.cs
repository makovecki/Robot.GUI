using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace UICore.Navigation.Controls
{
    public class NavigationControl:Control
    {
        public NavigationControl()
        {
            DefaultStyleKey = typeof(NavigationControl);
        }



        public INavigationViewModel Navigation
        {
            get { return (INavigationViewModel)GetValue(NavigationProperty); }
            set { SetValue(NavigationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Navigation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NavigationProperty =
            DependencyProperty.Register("Navigation", typeof(INavigationViewModel), typeof(NavigationControl), new PropertyMetadata(null));


    }
}
