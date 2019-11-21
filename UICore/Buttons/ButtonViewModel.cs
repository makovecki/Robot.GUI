using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace UICore.Buttons
{
    public class ButtonViewModel
    {
        public ButtonViewModel(ICommand command, string name)
        {
            Command = command;
            this.name = name;
        }

        public ICommand Command { get; set; }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

    }
}
