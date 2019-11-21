using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using UICore.App.Themes;
using UICore.Dialogs;
using UICore.Exceptions.Dialogs;
using UICore.Exceptions.Model;

namespace UICore.App
{
    public class AppService : IAppService
    {
        private readonly IDialogService dialogService;
        private readonly IThemeService themeService;

        public AppService(IDialogService dialogService, IThemeService themeService)
        {
            this.dialogService = dialogService;
            this.themeService = themeService;
            IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();
            MailAddress = configuration.GetSection("AppConfig")["MailAddress"];
        }

        public string MailAddress { get; private set; }

        public void CopyToClipBoard(string detail)
        {
            Clipboard.SetText(detail);
        }

        public void Exit()
        {
            Application.Current.Shutdown();
        }

        public void ProcessException(Exception exception, string description)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() => dialogService.ShowDialogAsync<ExceptionDialog, ExceptionDialogViewModel>("Whoops !", new CoreException(exception, description))), null);
            
        }

        public void Restart()
        {
            System.Diagnostics.Process.Start(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            Application.Current.Shutdown();
        }

        public void SendMail(string description, string detail)
        {
            string mailto = string.Format("mailto:{0}?Subject={1}&Body={2}", MailAddress, description, detail);
            mailto = Uri.EscapeUriString(mailto);
            System.Diagnostics.Process.Start(new ProcessStartInfo(mailto) { UseShellExecute = true });
        }

        public void SetTheme(ThemeType theme)
        {
            themeService.SetTheme(theme);
        }

        public ThemeType Theme => themeService.Theme;
    }
}
