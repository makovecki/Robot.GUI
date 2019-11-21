using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UICore.Navigation.Model;

namespace UICore.Navigation.Controls
{
    public class BreadCrumbsControl: Control
    {
        public BreadCrumbsControl()
        {
            DefaultStyleKey = typeof(BreadCrumbsControl);
        }



        public ObservableCollection<NavigationItem> BreadCrumbs
        {
            get { return (ObservableCollection<NavigationItem>)GetValue(BreadCrumbsProperty); }
            set { SetValue(BreadCrumbsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BreadCrumbs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BreadCrumbsProperty =
            DependencyProperty.Register("BreadCrumbs", typeof(ObservableCollection<NavigationItem>), typeof(BreadCrumbsControl), new PropertyMetadata(null));



        public ICommand CrumbCommand
        {
            get { return (ICommand)GetValue(CrumbCommandProperty); }
            set { SetValue(CrumbCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CrumbCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CrumbCommandProperty =
            DependencyProperty.Register("CrumbCommand", typeof(ICommand), typeof(BreadCrumbsControl), new PropertyMetadata(null));


    }
}
