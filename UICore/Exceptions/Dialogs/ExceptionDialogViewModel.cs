using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UICore.App;
using UICore.Dialogs;
using UICore.Exceptions.Model;
using UICore.Presentation;

namespace UICore.Exceptions.Dialogs
{
    public class ExceptionDialogViewModel : DialogViewModel<ExceptionDialogViewModel>
    {
        public ExceptionDialogViewModel(CoreException exception, IAppService appService)
        {
            CloseCommand = Make.UICommand.Do(() => Close());
            CopyCommand = Make.UICommand.Do(() => appService.CopyToClipBoard(Detail));
            RestartCommand = Make.UICommand.Do(() => appService.Restart());
            ExitCommand = Make.UICommand.Do(() => appService.Exit());
            ReportCommand = Make.UICommand.Do(() => appService.SendMail(exception.Description,Detail));

            RightButtons.Add(new UICore.Buttons.ButtonViewModel(CloseCommand, "Continue"));
            RightButtons.Add(new UICore.Buttons.ButtonViewModel(RestartCommand, "Restart"));
            RightButtons.Add(new UICore.Buttons.ButtonViewModel(ExitCommand, "Exit"));

            LeftButtons.Add(new UICore.Buttons.ButtonViewModel(CopyCommand, "Copy"));
            LeftButtons.Add(new UICore.Buttons.ButtonViewModel(ReportCommand, "Report"));

            Detail = exception.Exception.Message+ "\n" + exception.Exception.GetBaseException().StackTrace;

        }

        

        public ICommand CloseCommand { get; set; }
        public ICommand CopyCommand { get; set; }
        public ICommand RestartCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public ICommand ReportCommand { get; set; }

        public string Detail { get; set; }
    }
}
