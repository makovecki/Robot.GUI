using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Robot.UI.FindEsp.Model;

namespace Robot.UI.FindEsp.Services
{
    public class ESPMessageService : IESPMessageService
    {
        private readonly UdpClient client;

        public ESPMessageService()
        {
            client = new UdpClient(0);
            ListenClientsAsync();
        }

        private void ListenClientsAsync()
        {
            Task.Factory.StartNew( () =>
            {
                while (true)
                {
                    var echo = client.ReceiveAsync().Result;
                    var esp = new ESP(echo);
                    replys.Add(esp);
                }
            });
        }
        List<ESP> replys = new List<ESP>();
        public Task<List<ESP>> DiscoverESPAsync(int receiveTimeout = 100)
        {

            return Task.Factory.StartNew(() => 
            {
                replys.Clear();
                client.EnableBroadcast = true;
                
                var requestData = Encoding.ASCII.GetBytes("ping");
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress netip in host.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork))
                {
                    var ip = netip.GetAddressBytes();
                    ip[3] = 255;
                    client.Send(requestData, requestData.Length, new IPEndPoint(new IPAddress(ip), 1973));
                }
                
                Task.WaitAny(Task.Delay(receiveTimeout));
                
                
                return replys;
            });
        }
    }
}
