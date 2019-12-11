using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Robot.UI.Services.Model
{
    public class ESP
    {
        public ESP(UdpReceiveResult echo)
        {
            Buffer = echo.Buffer;
            Ip = echo.RemoteEndPoint.Address;
            Port = echo.RemoteEndPoint.Port;
        }

        public byte[] Buffer { get; private set; }
        public IPAddress Ip { get; private set; }
        public int Port { get; private set; }
        public int MessageType => Buffer[0];

    }
}
