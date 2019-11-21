using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UICore.Dialogs;
using UICore.Presentation;

namespace Robot.UI.Dialogs.FindESP
{
    public class FindESPViewModel : DialogViewModel<FindESPViewModel>
    {
        public FindESPViewModel()
        {
            CloseCommand = Make.UICommand.Do(() => Close());
            RightButtons.Add(new UICore.Buttons.ButtonViewModel(CloseCommand, "Close"));
        }
        public ICommand CloseCommand { get; set; }

        
    }

    
}
