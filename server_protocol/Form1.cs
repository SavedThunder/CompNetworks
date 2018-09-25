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

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static byte[] ReceiveVarData(Socket s)
        {
            int total = 0;
            int recv;
            byte[] datasize = new byte[4];

            recv = s.Receive(datasize, 0, 4, 0);
            int size = BitConverter.ToInt32(datasize, 0);
            int dataleft = size;
            byte[] data = new byte[size];


            while (total < size)
            {

                    recv = s.Receive(data, total, dataleft, 0);
                    if (recv == 0)
                    {
                        break;
                    }
                    total += recv;
                    dataleft -= recv;
                
            }
            return data;
        }

        private void StartCon_Click(object sender, EventArgs e)
        {
            string clapBack = "What the smurf did you just smurfing say about me, you little smurf? " +
                "I'll have you know I graduated top of my class at UC, and I've been involved in numerous HackAThons on 4 hours of sleep, " +
                "and I have over 300 confirmed seg faults. I am trained in Fortran and I'm the top coder in the entire class of 2020. " +
                "You are nothing to me but just another bug. " +
                "I will wipe you the smurf out with precision the likes of which has never been seen before on this Earth, mark my smurfing words. " +
                "You think you can get away with saying that crap to me over GroupMe? Think again, smurfer. " +
                "As we speak I am contacting my secret network of hackers across the USA and your IP is being traced right now so you better prepare for the storm, maggot. " +
                "The storm that wipes out the pathetic little thing you call your career. You're smurfing dead, kiddo. ";
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ip = ipHost.AddressList[0];
            IPEndPoint endpoint = new IPEndPoint(ip, 8080);

            Socket receiver = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            byte[] bytes = new byte[1024 * 500];
            string message = null;

            try
            {
                receiver.Bind(endpoint);    //Throws exception if address/port already in use
                receiver.Listen(5);   //Will only allow 5 clients in communication queue

                while (true)
                {
                    Socket clientHandler;
                    while (true)
                    {
                        clientHandler = receiver.Accept();
                        message = null;
                        int recieved = clientHandler.Receive(bytes);
                        clientHandler.Send(Encoding.ASCII.GetBytes("OK"));
                        message = ASCIIEncoding.ASCII.GetString(bytes, 0, recieved);
                        if (message == "1")
                        {
                            message = null;
                            recieved = clientHandler.Receive(bytes);
                            message += ASCIIEncoding.ASCII.GetString(bytes, 0, recieved);
                            if (message.IndexOf("<EOF>") > -1)
                            {
                                break;
                            }
                            MainText.Text = message;
                            Application.DoEvents();
                            byte[] msg = null;

                            if (message == "Jerk")
                            {
                                msg = Encoding.ASCII.GetBytes(clapBack);
                            }
                            else
                            {
                                msg = Encoding.ASCII.GetBytes("Delivered");
                            }
                            clientHandler.Send(msg);

                        }
                        else if (message == "2")
                        {
                            recieved = clientHandler.Receive(bytes);
                            int z = 0;
                            pictureBox1.Image = null;
                            while (true)
                            {
                                try
                                {
                                    bytes = ReceiveVarData(clientHandler);
                                }
                                catch(Exception ex)
                                {
                                    MainText.Text = ex.ToString();
                                    Application.DoEvents();
                                }
                                MemoryStream ms = new MemoryStream(bytes);
                                //MainText.Text = "RIght before the try";
                                //Application.DoEvents();
                                try
                                {
                                    //MainText.Text = "We in da try";
                                    //Application.DoEvents();
                                    //Image bmp = Image.FromStream(ms);
                                    System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                                    Image bmp = (Image)converter.ConvertFrom(bytes);
                                    MainText.Text = "After bmp =";
                                    Application.DoEvents();
                                    pictureBox1.Image = bmp;
                                    MainText.Text = "Made the pic into the box";
                                    Application.DoEvents();
                                    break;
                                }
                                catch (ArgumentException ex)
                                {
                                    MainText.Text = ex.ToString();
                                    Application.DoEvents();
                                }
                                if (bytes.Length == 0)
                                {
                                    receiver.Listen(10);
                                }
                            }
                            //clientHandler.Send(Encoding.ASCII.GetBytes("OK"));
                            break;
                        }
                    }

                    //clientHandler.Shutdown(SocketShutdown.Both);
                    //clientHandler.Close();
                }

            }
            catch (Exception ex)
            {
                MainText.Text = ex.ToString();
                Console.Read();
            }
        }

        private void MainText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
