using Robot.UI.Services.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace Robot.UI.FindEsp.Model
{
    public class ESPEcho : ESP
    {
        public ESPEcho(UdpReceiveResult echo) : base(echo)
        {
        }

        public string Name => System.Text.Encoding.Default.GetString(this.Buffer.Skip(3).ToArray());
    }
}
