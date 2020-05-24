using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.object1
{
    public class MessageClint
    {
        TcpClient tcpClient;
        public MessageClint(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
            messageClint();
        }
        private void messageClint()
        {
            String line = "";
            NetworkStream serverSockStream = tcpClient.GetStream();
            StreamWriter serverStreamWriter = new StreamWriter(serverSockStream);

            if (serverSockStream.DataAvailable)
            {
                //Thread n_clint;

                //ParameterizedThreadStart start = new ParameterizedThreadStart(messageClicnt);
                //n_clint = new Thread();
                //n_clint.IsBackground = true;
                //n_clint.Start(tcpClient);
            }
            using (StreamReader serverStreamReader = new StreamReader(serverStreamWriter.BaseStream))
            {
                line = serverStreamReader.ReadToEnd();
            }

            //lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Date Time : " + System.DateTime.Now.ToString() + "Receive : " + line); });
            //Application.DoEvents();
            String fileName = "", path = "";
            path = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            new LogWriter("e", "FrmXrayPACsAdd path " + path);
            if (!Directory.Exists(path + "message"))
            {
                Directory.CreateDirectory(path + "message");
            }
            //lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Date Time : " + System.DateTime.Now.ToString() + "Directory.CreateDirectory : " + path + "\\message\\" + fileName); });
            //Application.DoEvents();
            fileName = DateTime.Now.Ticks.ToString() + ".txt";
            new LogWriter("e", "FrmXrayPACsAdd fileName " + path + "message\\" + fileName);
            FileStream stream = new FileStream(path + "message\\" + fileName, FileMode.CreateNew);
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(line);
            }
            stream.Close();
            //lboxServer.Invoke((MethodInvoker)delegate { lboxServer.Items.Add("Date Time : " + System.DateTime.Now.ToString() + "write file success "); });
            //Application.DoEvents();

            //NetworkStream clientSockStream = tcpClient.GetStream();
            //StreamWriter clientStreamWriter = new StreamWriter(clientSockStream);
            serverStreamWriter.WriteLine("ACK");
        }
    }
}
