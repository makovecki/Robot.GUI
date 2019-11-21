using System;
using System.Collections.Generic;
using System.Text;

namespace UICore.Dispatcher
{
    public interface IDispatcher
    {
        void BeginInvoke(Action action);
    }
}
