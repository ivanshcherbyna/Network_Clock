using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NET_CLOCK_Client
{
    public partial class Form1 : Form
    {
        static private Socket listeningSoket;
        static private int port;
       // private string message;
        private string ipAdress;
        Task task;
        public Form1()
        {
            InitializeComponent();

            //////connect
            ipAdress = "127.0.0.1";
            //Load += Connect;
            task = new Task(Connect);
            

        }

        public void Connect()
        {
            try
            {
                port = 1024;
               
                IPEndPoint localIP = new IPEndPoint(IPAddress.Parse(ipAdress), port);
                
                listeningSoket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                listeningSoket.Connect(localIP);
                /*
                 byte[] data;
                 data = new byte[256]; //bufer sor answer
                 StringBuilder builder = new StringBuilder();
                 int bytes = 0;
                 do
                 {
                     listeningSoket.Accept();
                     bytes = listeningSoket.Receive(data, data.Length, SocketFlags.None);
                     builder.Append(Encoding.Default.GetString(data, 0, bytes));
                 }
                 while (listeningSoket.Available > 0);

                 textBox1.Text = builder.ToString();
               */
                if (listeningSoket.Connected)
                {
                    String strSend = "GET";
                    listeningSoket.Send(System.Text.Encoding.ASCII.GetBytes(strSend));
                    
                    int l;
                     do
                    {
                        
                        byte[] buffer = new byte[256];
                        
                        l = listeningSoket.Receive(buffer);
                            
                            textBox1.Text = System.Text.Encoding.ASCII.GetString(buffer, 0, l);
                        
                       
                    } while (l > 0);
                }
                else
                    MessageBox.Show("Error");

            }
            catch (Exception ex) { MessageBox.Show(ex.TargetSite.ToString()); }
            finally
            {
                //listeningSoket.Shutdown(SocketShutdown.Both);
                //listeningSoket.Close();
            }
        }

        public void readNetStream()
        {

        }

        private void btnGetTime_Click(object sender, EventArgs e)
        {
            try { task.Start(); }
            catch (Exception ex) { MessageBox.Show(ex.Message);}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
