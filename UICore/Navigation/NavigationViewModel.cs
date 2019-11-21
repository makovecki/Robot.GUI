using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using UICore.Navigation.Model;
using UICore.Presentation;

namespace UICore.Navigation
{
    public class NavigationViewModel : Observable,INavigationViewModel
    {

        public NavigationViewModel(INavigationService navigationService)
        {
            BreadCrumbs = new ObservableCollection<NavigationItem>();
            BackCommand = Make.UICommand.When(() => navigationService.CanBrowseBack()).Do(() => BrowseBack(navigationService));
            CrumbCommand = Make.UICommand.Do<NavigationItem>((crumb) => SelectedCrumb(crumb, navigationService));
        }

        private void SelectedCrumb(NavigationItem crumb, INavigationService navigationService)
        {
            navigationService.BrowseBackTo(crumb);
        }

        private void BrowseBack(INavigationService navigationService)
        {
            navigationService.BrowseBack();
        }

        public void RefreshBackButton()
        {
            Make.UICommand.Refresh(BackCommand);
        }

        public void SetCurrent(NavigationItem item)
        {
            Current = item;
            RefreshBackButton();
        }

        public void AddBreadCrumb(NavigationItem current)
        {
            BreadCrumbs.Add(current);
        }

        public void RemoveBreadCrumb(NavigationItem current)
        {
            BreadCrumbs.Remove(current);
        }

        private NavigationItem? current;
        public NavigationItem? Current
        {
            get { return current; }
            set { current = value; OnPropertyChanged(); }
        }

        public ICommand BackCommand { get; set; }

        public ObservableCollection<NavigationItem> BreadCrumbs { get; set; }

        public ICommand CrumbCommand { get; set; }


    }
}
