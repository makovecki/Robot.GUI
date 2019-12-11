using Robot.UI.FindEsp;
using Robot.UI.Services;
using System;
using System.Threading.Tasks;
using System.Windows;
using UICore.App;
using UICore.App.Themes;
using UICore.Dispatcher;
using UICore.IoC;
using UICore.MainWindow;
using UICore.Navigation;

namespace Robot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var window = new MainWindow();

            IIoCService iocService = new IoCService
            {
                RegisterComponents = (ioc) =>
                {
                    ioc.RegisterType<ESPMessageService>().As<IESPMessageService>();
                }
            };

            iocService.Build();
            iocService.Resolve<IAppService>().SetTheme(ThemeType.Light);
            window.DataContext = iocService.Resolve<IMainWindowViewModel>();
            window.Show();

            iocService.Resolve<INavigationService>().NavigateTo<FindESPViewModel, FindESPView>();

            AppDomain.CurrentDomain.UnhandledException += (s, e) => iocService.Resolve<IAppService>().ProcessException((Exception)e.ExceptionObject, "Domain exception");
            Dispatcher.UnhandledException += (s, e) =>
            {
                e.Handled = true;
                iocService.Resolve<IAppService>().ProcessException(e.Exception, "Dispatcher exception");

            };
            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                if (e.Exception != null)
                {
                    iocService.Resolve<IAppService>().ProcessException(e.Exception, "Unobserved exception");
                    e.SetObserved();
                }
            };
        }

    }
}
