using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace UICore.Buttons
{
    public class ButtonSystemViewModel : ButtonViewModel
    {
        public ButtonSystemViewModel(ICommand command, string name) : base(command, name)
        {
        }
    }
}
