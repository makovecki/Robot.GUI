using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Robot.UI.FindEsp.Model;

namespace Robot.UI.Services
{
    public interface IESPMessageService
    {
        Task<ConcurrentBag<ESP>> DiscoverESPAsync(int receiveTimeout = 100);
        Task<bool> SaveDataAsync(ESP esp, string name);
    }
}
