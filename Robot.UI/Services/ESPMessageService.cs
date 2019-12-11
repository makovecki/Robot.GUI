using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Robot.UI.FindEsp.Model;

namespace Robot.UI.Services
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
            Task.Factory.StartNew(() =>
           {
               while (true)
               {
                   var echo = client.ReceiveAsync().Result;
                   var esp = new ESP(echo);
                   replys.Add(esp);
               }
           });
        }
        ConcurrentBag<ESP> replys = new ConcurrentBag<ESP>();
        public Task<ConcurrentBag<ESP>> DiscoverESPAsync(int receiveTimeout = 100)
        {

            return Task.Factory.StartNew(() =>
            {
                replys.Clear();
                client.EnableBroadcast = true;

                var requestData = new byte[] { 0, 0, 4 }.Concat(Encoding.ASCII.GetBytes("ping")).ToArray();
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

        public async Task<bool> SaveDataAsync(ESP esp, string name)
        {
            
            replys.Clear();
            var message = new byte[] { 1 }.Concat(BitConverter.GetBytes((Int16)name.Length).Reverse()).Concat(Encoding.ASCII.GetBytes(name)).ToArray();
            client.Send(message, message.Length, new IPEndPoint(esp.Ip, 1973));
            while(replys.Count==0)
            {
                await Task.Delay(100);
            }

            return true;
            
        }
    }
}
