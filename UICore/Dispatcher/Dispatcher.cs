using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace UICore.Dispatcher
{
    public class Dispatcher : IDispatcher
    {
        private System.Windows.Threading.Dispatcher _dispatcher;

        public Dispatcher()
        {
            _dispatcher = Application.Current.Dispatcher;
        }

        public void BeginInvoke(Action action)
        {
            _dispatcher.BeginInvoke(action);
        }
    }
}
