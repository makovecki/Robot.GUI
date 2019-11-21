using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace UICore.Navigation.Model
{
    public class NavigationItem
    {
        public NavigationItem(FrameworkElement view, IViewModel viewModel)
        {
            View = view;
            ViewModel = viewModel;
        }

        public FrameworkElement View { get; set; }
        public IViewModel ViewModel { get; set; }

        public string Name => ViewModel.Name;
    }
}
