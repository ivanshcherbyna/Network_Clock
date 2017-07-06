using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NET_CLOCK
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        
            public static void Main(string[] args)
        {
            string clear = "";
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            Socket ns = s;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint ep = new IPEndPoint(ip, 1024);


            s.Bind(ep);
            s.Listen(10);

            try
            {
                while (true)
                {
                    ns = s.Accept();

                    Console.WriteLine(ns.RemoteEndPoint.ToString());
                    while (true)
                    {
                        
                        ns.Send(System.Text.Encoding.ASCII.GetBytes(DateTime.Now.ToLongTimeString()));
                        Thread.Sleep(500);
                        ns.Send(System.Text.Encoding.ASCII.GetBytes(clear));
                        Thread.Sleep(500);
                    }

                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ns.Shutdown(SocketShutdown.Both);
                ns.Close();
            }


        }
    }
}
    

