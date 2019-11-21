using System;
using System.Collections.Generic;
using System.Text;
using UICore.App.Themes;

namespace UICore.App
{
    public interface IAppService
    {
        void ProcessException(Exception exception, string description);
        void CopyToClipBoard(string detail);
        void SendMail(string description, string detail);
        void Restart();
        void Exit();
        void SetTheme(ThemeType theme);
    }
}
