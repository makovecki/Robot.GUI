using System.Collections.Generic;
using System.Windows;
using UICore.IoC;
using UICore.Navigation.Model;

namespace UICore.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IIoCService iocService;

        private readonly Stack<NavigationItem> stack = new Stack<NavigationItem>();


        public NavigationService(IIoCService iocService)
        {
            this.iocService = iocService;
        }

        public INavigationViewModel? NavigationViewModel { get; set; }
        public void BrowseBack()
        {
            NavigationViewModel?.SetCurrent(stack.Pop());
            if (NavigationViewModel?.Current != null) NavigationViewModel?.RemoveBreadCrumb(NavigationViewModel.Current);
        }

        public void BrowseBackTo(NavigationItem crumb)
        {
            NavigationItem? last = null;
            while (last != crumb)
            {
                last = stack.Pop();
                NavigationViewModel?.RemoveBreadCrumb(last);
            }
            NavigationViewModel?.SetCurrent(last);

        }

        public bool CanBrowseBack() => stack.Count > 0;


        public void NavigateTo<TViewModel, TView>(object? parameter=null) where TViewModel : class, IViewModel where TView : class
        {
            if (NavigationViewModel?.Current?.ViewModel.GetType() == typeof(TViewModel) && NavigationViewModel.Current?.View.GetType() == typeof(TView) && NavigationViewModel.Current?.ViewModel.IsSingleInstance == true)
                return;

            var view = iocService.ResolveTemporaryType<TView>(parameter) as FrameworkElement;
            var viewModel = iocService.ResolveTemporaryType<TViewModel>(parameter) as IViewModel;
            if (view != null)
            {
                view.DataContext = viewModel;
                var item = new NavigationItem(view, viewModel);

                if (NavigationViewModel?.Current != null)
                {
                    stack.Push(NavigationViewModel.Current);
                    NavigationViewModel.AddBreadCrumb(NavigationViewModel.Current);
                }
                NavigationViewModel?.SetCurrent(item);
            }
            
        }
    }
}
