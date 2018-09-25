using System;
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
using System.IO;

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
            if (textBox1.Text == "") {
                return;
            }
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = ipHost.AddressList[0];
            IPEndPoint endpoint = new IPEndPoint(ip, 8080);

            byte[] bytes = new byte[1024 * 500];
            try {
                Socket client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                client.Connect(endpoint);

                byte[] pre_msg = Encoding.ASCII.GetBytes("1");
                byte[] msg = Encoding.ASCII.GetBytes(textBox1.Text.ToString());

                int prebytesSent = client.Send(pre_msg);
                int prebytesRec = client.Receive(bytes);

                int bytesSent = client.Send(msg);
                int bytesRec = client.Receive(bytes);

                textBox2.Text += Encoding.ASCII.GetString(bytes);
                textBox2.AppendText(Environment.NewLine);
            }
            catch (ArgumentNullException ane) {  
                Console.WriteLine("ArgumentNullException : {0}",ane.ToString());  
            } catch (SocketException se) {  
                Console.WriteLine("SocketException : {0}",se.ToString());  
            } catch (Exception ue) {  
                Console.WriteLine("Unexpected exception : {0}", ue.ToString());  
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Bitmap pic_image = null;
            OpenFileDialog image = new OpenFileDialog();
            image.Filter = "Image Files (*.jpg, *.png) | *.jpg; *.png";
            if (image.ShowDialog() == DialogResult.OK)
            {
                pic_image = new Bitmap(image.FileName);
            }
            else
            {
                return;
            }
            MemoryStream ms = new MemoryStream();
            pic_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] pic_im_Bytes = ms.ToArray();

            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = ipHost.AddressList[0];
            IPEndPoint endpoint = new IPEndPoint(ip, 8080);

            byte[] bytes = new byte[1024 * 500];
            try
            {
                Socket client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                client.Connect(endpoint);
                byte[] pre_msg = Encoding.ASCII.GetBytes("2");

                int prebytesSent = client.Send(pre_msg);
                int prebytesRec = client.Receive(bytes);

                int bytesSent = client.Send(pic_im_Bytes);
                int bytesRec = client.Receive(bytes);

                textBox2.Text += Encoding.ASCII.GetString(bytes);
                textBox2.AppendText(Environment.NewLine);
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception ue)
            {
                Console.WriteLine("Unexpected exception : {0}", ue.ToString());
            }
            ms.Close();
            ms.Dispose();
        }
    }
}
