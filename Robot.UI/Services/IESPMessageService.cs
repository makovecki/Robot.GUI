using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Robot.UI.Services.Model;

namespace Robot.UI.Services
{
    public interface IESPMessageService
    {
        Task DiscoverESPAsync(int receiveTimeout = 100);
        void SaveDataAsync(ESP esp, string name);
        void AddListener(int messageType, Action<ESP> action);
    }
}
