using System.Collections.ObjectModel;
using System.Windows.Input;
using UICore.App.Themes;
using UICore.Buttons;
using UICore.Dialogs;
using UICore.Navigation;
using UICore.Pages.Settings;
using UICore.Presentation;

namespace UICore.MainWindow
{
    public class MainWindowViewModel : Observable, IMainWindowViewModel
    {

        public MainWindowViewModel(IWindowService windowService, INavigationViewModel navigation,  INavigationService navigationService) 
        {
            TopButtons = new ObservableCollection<ButtonViewModel>();
            TopButtons.Add(new ButtonSystemViewModel(Make.UICommand.Do(() => navigationService.NavigateTo<SettingsViewModel,SettingsView>()), "SETTINGS"));
            WindowCloseCommand = Make.UICommand.Do(() => CloseWindow(windowService));
            WindowMaximizeCommand = Make.UICommand.Do(() => MaximizeWindow(windowService));
            WindowMinimizeCommand = Make.UICommand.Do(() => MinimizeWindow(windowService));        
            this.Navigation = navigation;
        }
        private void MinimizeWindow(IWindowService windowService)
        {
            windowService.Minimize();
        }

        private void MaximizeWindow(IWindowService windowService)
        {
            windowService.Maximize();
        }

        private void CloseWindow(IWindowService windowService)
        {
            windowService.Close();
        }

        public ICommand WindowCloseCommand { get; set; }
        public ICommand WindowMaximizeCommand { get; set; }
        public ICommand WindowMinimizeCommand { get; set; }


        private IDialogModel? dialog;
        public IDialogModel? Dialog
        {
            get { return dialog; }
            set { dialog = value; OnPropertyChanged(); }
        }

        private INavigationViewModel? navigation;

        public INavigationViewModel? Navigation
        {
            get { return navigation; }
            set { navigation = value; OnPropertyChanged(); }
        }
        public ObservableCollection<ButtonViewModel> TopButtons { get; set; }
    }
}
