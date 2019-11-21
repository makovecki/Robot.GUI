using System;
using System.Collections.Generic;
using System.Text;
using UICore.Navigation.Model;

namespace UICore.Navigation
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel, TView>(object? parameter = null) where TViewModel : class, IViewModel where TView : class;
        bool CanBrowseBack();
        void BrowseBack();
        void BrowseBackTo(NavigationItem crumb);
    }
}
