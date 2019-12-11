using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Robot.UI.FindEsp.Model
{
    public class ESP
    {
        public ESP(UdpReceiveResult echo)
        {
            Name = System.Text.Encoding.Default.GetString(echo.Buffer.Skip(3).ToArray());
            Ip = echo.RemoteEndPoint.Address;
            Port = echo.RemoteEndPoint.Port;
        }

        public string Name { get; private set; }
        public IPAddress Ip { get; private set; }
        public int Port { get; private set; }

    }
}
