﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace client_protocol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = ipHost.AddressList[0];
            IPEndPoint endpoint = new IPEndPoint(ip, 8080);

            byte[] bytes = new byte[1024];
            try {
                Socket client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                client.Connect(endpoint);
                textBox2.Text += "Client connected to RE\n";

                byte[] msg = Encoding.ASCII.GetBytes(textBox1.Text.ToString()+"<EOF>");

                int bytesSent = client.Send(msg);
                textBox2.Text += "Client sending message\n";

                int bytesRec = client.Receive(bytes);
                textBox2.Text += "Client message received: ";
                textBox2.Text += Encoding.ASCII.GetString(bytes);

                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch (ArgumentNullException ane) {  
                Console.WriteLine("ArgumentNullException : {0}",ane.ToString());  
            } catch (SocketException se) {  
                Console.WriteLine("SocketException : {0}",se.ToString());  
            } catch (Exception ue) {  
                Console.WriteLine("Unexpected exception : {0}", ue.ToString());  
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}