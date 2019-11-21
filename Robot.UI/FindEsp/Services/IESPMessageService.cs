using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Robot.UI.FindEsp.Model;

namespace Robot.UI.FindEsp.Services
{
    public interface IESPMessageService
    {
        Task<List<ESP>> DiscoverESPAsync(int receiveTimeout = 100);
    }
}
