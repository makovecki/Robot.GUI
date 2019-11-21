using System;
using System.Collections.Generic;
using System.Text;

namespace UICore.Navigation
{
    public interface IViewModel
    {
        public string Name { get; }
        public bool IsSingleInstance { get; }
    }
}
