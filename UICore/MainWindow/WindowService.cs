using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UICore.MainWindow
{
    public class WindowService : IWindowService
    {
        private Window GetCurrentWindow()
        {
            return Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) ?? Application.Current.MainWindow;
        }
        public void Close()
        {
            GetCurrentWindow()?.Close();
        }

        public async Task<Window> GetCurrentWindowWithViewModelAsync()
        {
            var currentWindow = GetCurrentWindow();
            
                
            object vm = currentWindow.DataContext;
            while (vm==null)
            {
                await Task.Delay(10);
                vm = currentWindow.DataContext;
            }
            return currentWindow;
            
            
        }

        public void Maximize()
        {
            var window = GetCurrentWindow();
            window.WindowState = (window.WindowState == WindowState.Normal) ? WindowState.Maximized : WindowState.Normal;
        }

        public void Minimize()
        {
            GetCurrentWindow().WindowState = WindowState.Minimized;
        }
    }
}
