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
using Robot.UI.Services.Model;

namespace Robot.UI.Services
{
    public class ESPMessageService : IESPMessageService
    {
        private readonly UdpClient client;
        private readonly Dictionary<int, List<Action<ESP>>> listeners = new Dictionary<int, List<Action<ESP>>>();
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
                   var esp = ResolveESPMessageType(client.ReceiveAsync().Result);
                   if (listeners.TryGetValue(esp.MessageType, out List<Action<ESP>>? messageListeners)) messageListeners.ForEach(listener => listener(esp));
               }
           });
        }

        private ESP ResolveESPMessageType(UdpReceiveResult result)
        {
            switch (result.Buffer[0])
            {
                case 0: return new ESPEcho(result);
                default:
                    return new ESP(result);
            }
        }
        
        public Task DiscoverESPAsync(int receiveTimeout = 100)
        {
            return Task.Factory.StartNew(() =>
            {
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
            });
        }

        public void SaveDataAsync(ESP esp, string name)
        {
            var message = new byte[] { 1 }.Concat(BitConverter.GetBytes((Int16)name.Length).Reverse()).Concat(Encoding.ASCII.GetBytes(name)).ToArray();
            client.Send(message, message.Length, new IPEndPoint(esp.Ip, 1973));
        }

        public void AddListener(int messageType, Action<ESP> action)
        {
            if (listeners.TryGetValue(messageType, out List<Action<ESP>>? messageListeners)) messageListeners.Add(action);
            else listeners.Add(messageType, new List<Action<ESP>> { action });
        }
    }
}
