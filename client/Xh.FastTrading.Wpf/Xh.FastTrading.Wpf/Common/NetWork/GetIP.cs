using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Xh.FastTrading.Wpf.Model;

namespace Xh.FastTrading.Wpf.Common.NetWork
{
    public class GetIP
    {
        public string GetLocalIP() 
        {
            IPAddress local_ip = null;
            try
            {
                IPAddress[] iPs;
                iPs = Dns.GetHostAddresses(Dns.GetHostName());
                local_ip = iPs.First(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
                return local_ip.ToString();
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public int GetFirstAvailablePort() 
        {
            const int MAX_PORT = 65535;
            const int BEGIN_RORT = 5000;
            for (int i = BEGIN_RORT; i < MAX_PORT; i++)
            {
                if (PortIsAvailable(i))
                {
                    return i;
                }
            }
            return -1;
        }
        public  IList PortIsUsed() 
        {
            IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipsTCP = iPGlobalProperties.GetActiveTcpListeners();
            IPEndPoint[] ipsUDP = iPGlobalProperties.GetActiveUdpListeners();
            TcpConnectionInformation[] tcpConnections = iPGlobalProperties.GetActiveTcpConnections();

            IList allPorts = new ArrayList();
            foreach (IPEndPoint ep in ipsTCP) allPorts.Add(ep.Port);
            foreach (IPEndPoint ep in ipsUDP) allPorts.Add(ep.Port);
            foreach (TcpConnectionInformation conn in tcpConnections) allPorts.Add(conn.LocalEndPoint.Port);
            return allPorts;
        }

        public bool PortIsAvailable(int port) 
        {
            bool isAvailable = true;
            IList portUsed = PortIsUsed();
            foreach (int P in portUsed)
            {
                if (P == port)
                {
                    isAvailable = false;
                    break;
                }
            }
            return isAvailable;
        }

    }
}
    