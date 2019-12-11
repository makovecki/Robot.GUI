
using Robot.UI.ESPController;
using Robot.UI.FindEsp.Model;
using Robot.UI.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UICore.Dispatcher;
using UICore.Navigation;
using UICore.Presentation;

namespace Robot.UI.FindEsp
{
    public class FindESPViewModel : Observable,IViewModel
    {
        private readonly IESPMessageService messageService;

        public FindESPViewModel(IESPMessageService messageService, IDispatcher dispatcher, INavigationService navigationService)
        {
            Name = "Connection";
            FoundEsps = new ObservableCollection<ESP>();
            
            CancelCommand = Make.UICommand.Do(()=> { IsSearching = false; });
            StartSearchCommand = Make.UICommand.Do(() => ESPPingAsync(dispatcher));
            ConnectCommand = Make.UICommand<ESP>.Do((esp) => Connect(navigationService,esp));
            this.messageService = messageService;
            
            ESPPingAsync(dispatcher);

        }

        public bool IsSingleInstance => false;
        private void Connect(INavigationService navigationService, ESP esp)
        {
            IsSearching = false;
            navigationService.NavigateTo<ESPControllerViewModel, ESPControllerView>(esp);
        }

        private bool isSearching;

        public bool IsSearching
        {
            get { return isSearching; }
            set { isSearching = value; OnPropertyChanged(); }
        }

        public ICommand StartSearchCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ConnectCommand { get; set; }

        public ObservableCollection<ESP> FoundEsps { get; set; }
        private async void ESPPingAsync(IDispatcher dispatcher)
        {
            FoundEsps.Clear();
            IsSearching = true;
            while (true)
            {
                if (!IsSearching) break;    
                var newesps = await messageService.DiscoverESPAsync();
                SynchronizeESPs(newesps);
            }


        }

        private void SynchronizeESPs(ConcurrentBag<ESP> newesps)
        {
            newesps.ToList().ForEach(e =>
            {
               if (e!= null && !FoundEsps.Any(esp=>esp.Ip.Equals(e.Ip))) FoundEsps.Add(e);
            });
        }

        public string Name { get; set; }
    }
}
