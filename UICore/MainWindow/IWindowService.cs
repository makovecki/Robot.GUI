using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UICore.MainWindow
{
    public interface IWindowService
    {
        void Close();
        void Maximize();
        void Minimize();
        Task<Window> GetCurrentWindowWithViewModelAsync();
    }
}
