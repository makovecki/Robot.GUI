using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using UICore.Navigation.Model;

namespace UICore.Navigation
{
    public interface INavigationViewModel
    {
        NavigationItem? Current { get; set; }
        void SetCurrent(NavigationItem item);
        void AddBreadCrumb(NavigationItem current);
        void RemoveBreadCrumb(NavigationItem current);
    }
}
