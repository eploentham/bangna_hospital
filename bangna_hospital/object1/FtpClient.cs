using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.object1
{
    /*
     * 0002     scan แล้วเก็บข้อมูลไม่ครบ
     */
    public class FtpClient
    {
        string host = null;
        string user = null;
        string pass = null, ProxyProxyType="", ProxyHost="", ProxyPort="";
        FtpWebRequest ftpRequest = null;
        FtpWebResponse ftpResponse = null;
        Stream ftpStream = null;
        int bufferSize = 2048;
        Boolean ftpUsePassive = false;

        /* Construct Object */
        public FtpClient(string hostIP, string userName, string password)
        {
            host = hostIP; user = userName; pass = password;
        }
        public FtpClient(string hostIP, string userName, string password, Boolean ftpUsePassive)
        {
            host = hostIP; user = userName; pass = password;
            this.ftpUsePassive = ftpUsePassive;
        }
        public FtpClient(string hostIP, string userName, string password, Boolean ftpUsePassive, String ProxyProxyType, String ProxyHost, String ProxyPort)
        {
            host = hostIP; user = userName; pass = password;
            this.ftpUsePassive = ftpUsePassive;
            this.ProxyProxyType = ProxyProxyType;
            this.ProxyHost = ProxyHost;
            this.ProxyPort = ProxyPort;

        }
        /* Download File */
        public MemoryStream download(String remoteFile)
        {
            MemoryStream stream = new MemoryStream();
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Get the FTP Server's Response Stream */
                ftpStream = ftpResponse.GetResponseStream();
                /* Open a File Stream to Write the Downloaded File */
                //FileStream localFileStream = new FileStream(localFile, FileMode.Create);

                /* Buffer for the Downloaded Data */
                byte[] byteBuffer = new byte[bufferSize];
                int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);

                /* Download the File by Writing the Buffered Data Until the Transfer is Complete */
                try
                {
                    while (bytesRead > 0)
                    {
                        //localFileStream.Write(byteBuffer, 0, bytesRead);
                        stream.Write(byteBuffer, 0, bytesRead);
                        bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);

                    }
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
                /* Resource Cleanup */
                //localFileStream.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return stream;
        }
        public MemoryStream download4K(String remoteFile)
        {
            MemoryStream stream = new MemoryStream();
            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpStream = ftpResponse.GetResponseStream();
                byte[] byteBuffer = new byte[1024000];
                int bytesRead = ftpStream.Read(byteBuffer, 0, 1024000);
                try
                {
                    while (bytesRead > 0)
                    {
                        //localFileStream.Write(byteBuffer, 0, bytesRead);
                        stream.Write(byteBuffer, 0, bytesRead);
                        bytesRead = ftpStream.Read(byteBuffer, 0, 1024000);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return stream;
        }
        /* Upload File */
        public Boolean upload(string remoteFile, Stream localFile)
        {
            Boolean chk = false;
            String err = "";
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                err = "00";
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                if (ProxyProxyType.Equals("1"))
                {
                    ftpRequest.Proxy = new WebProxy();
                    int chk1 = 0;
                    if ((ProxyHost.Length > 0) && (int.TryParse(ProxyPort, out chk1)))
                    {
                        ftpRequest.Proxy = new WebProxy(ProxyHost, chk1);
                    }
                }
                err = "01";
                //ftpRequest.Proxy = new WebProxy();
                //MessageBox.Show("host " + host + "/" + remoteFile, "localFile " + localFile);
                //MessageBox.Show("Proxy " + ftpRequest.Proxy, "localFile "+ localFile);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */

                /* Open a File Stream to Read the File for Upload */
                if (localFile.Length <=0) return false;
                err = "02";
                //FileStream localFileStream = new FileStream(localFile, FileMode.Open, FileAccess.Read);
                //FileStream localFileStream = (FileStream)localFile;
                err = "03";
                ftpStream = ftpRequest.GetRequestStream();
                err = "04";
                /* Buffer for the Downloaded Data */
                byte[] byteBuffer = new byte[bufferSize];
                //int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                int bytesSent = localFile.Read(byteBuffer, 0, bufferSize);
                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                err = "05";
                try
                {
                    while (bytesSent != 0)
                    {
                        ftpStream.Write(byteBuffer, 0, bytesSent);
                        bytesSent = localFile.Read(byteBuffer, 0, bufferSize);
                    }
                }
                catch (Exception ex)
                {
                    new LogWriter("e", "upload localFile " + localFile + ex.ToString() + " Error ftp upload write file  ");      //+0002
                    Console.WriteLine(ex.ToString());
                }
                /* Resource Cleanup */
                //localFileStream.Close();
                ftpStream.Close();
                ftpRequest = null;
                chk = true;
            }
            catch (Exception ex)
            {
                //String status = ((FtpWebResponse)ex.Response).StatusDescription;
                new LogWriter("e", err + " upload localFile " + localFile + ex.ToString() + " remote file " + remoteFile + "\nError ftp upload  ");
                MessageBox.Show("" + ex.ToString(), "Error ftp upload  ");
                Console.WriteLine(ex.ToString());
                chk = false;
            }
            return chk;
        }
        public Boolean upload(string remoteFile, string localFile)
        {
            Boolean chk = false;
            String err = "";
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                err = "00";
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                if (ProxyProxyType.Equals("1"))
                {
                    ftpRequest.Proxy = new WebProxy();
                    int chk1 = 0;
                    if ((ProxyHost.Length > 0) && (int.TryParse(ProxyPort, out chk1)))
                    {
                        ftpRequest.Proxy = new WebProxy(ProxyHost, chk1);                        
                    }
                }
                err = "01";
                //ftpRequest.Proxy = new WebProxy();
                //MessageBox.Show("host " + host + "/" + remoteFile, "localFile " + localFile);
                //MessageBox.Show("Proxy " + ftpRequest.Proxy, "localFile "+ localFile);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */

                /* Open a File Stream to Read the File for Upload */
                if (!File.Exists(localFile)) return false;
                err = "02";
                FileStream localFileStream = new FileStream(localFile, FileMode.Open, FileAccess.Read);
                err = "03";
                ftpStream = ftpRequest.GetRequestStream();
                err = "04";
                /* Buffer for the Downloaded Data */
                byte[] byteBuffer = new byte[bufferSize];
                int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                err = "05";
                try
                {
                    while (bytesSent != 0)
                    {
                        ftpStream.Write(byteBuffer, 0, bytesSent);
                        bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                    }
                }
                catch (Exception ex) 
                {
                    new LogWriter("e", "upload localFile " + localFile + ex.ToString() + " Error ftp upload write file  ");      //+0002
                    Console.WriteLine(ex.ToString()); 
                }
                finally
                {
                    localFileStream.Close();
                    localFileStream.Dispose();
                    ftpStream.Close();
                    ftpStream.Dispose();
                }
                /* Resource Cleanup */
                ftpRequest = null;
                chk = true;
            }
            catch (Exception ex)
            {
                //String status = ((FtpWebResponse)ex.Response).StatusDescription;
                new LogWriter("e", err+" upload localFile " + localFile + ex.ToString()+" remote file "+ remoteFile + "\nError ftp upload  ");
                MessageBox.Show(""+ ex.ToString(), "Error ftp upload  ");
                Console.WriteLine(ex.ToString());
                chk = false;
            }
            return chk;
        }
        public void upload(string remoteFile, string localFile, String webproxy, int port)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
                /* Log in to the FTP Server with the User Name and Password Provided */
                WebProxy wproxy = new WebProxy(webproxy, port);
                ftpRequest.Proxy = wproxy;

                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                if (ProxyProxyType.Equals("1"))
                {
                    ftpRequest.Proxy = new WebProxy();
                    ftpRequest.Proxy = new WebProxy(ProxyHost, int.Parse(ProxyPort));
                }
                //ftpRequest.Proxy = new WebProxy();
                //MessageBox.Show("host " + host + "/" + remoteFile, "localFile " + localFile);
                //MessageBox.Show("Proxy " + ftpRequest.Proxy, "localFile "+ localFile);
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                /* Establish Return Communication with the FTP Server */

                /* Open a File Stream to Read the File for Upload */
                if (!File.Exists(localFile)) return;
                FileStream localFileStream = new FileStream(localFile, FileMode.Open, FileAccess.Read);
                ftpStream = ftpRequest.GetRequestStream();
                /* Buffer for the Downloaded Data */
                byte[] byteBuffer = new byte[bufferSize];
                int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                /* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
                try
                {
                    while (bytesSent != 0)
                    {
                        ftpStream.Write(byteBuffer, 0, bytesSent);
                        bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                /* Resource Cleanup */
                localFileStream.Close();
                ftpStream.Close();
                ftpRequest = null;
            }
            catch (Exception ex)
            {
                //String status = ((FtpWebResponse)ex.Response).StatusDescription;
                MessageBox.Show("" + ex.ToString(), "Error ftp upload -> WebProxy");
                Console.WriteLine(ex.ToString());
            }
            return;
        }
        /* Delete File */
        public void delete(string deleteFile)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + deleteFile);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                //if (ProxyProxyType.Equals("1"))
                //{
                //    ftpRequest.Proxy = new WebProxy();
                //    ftpRequest.Proxy = new WebProxy(ProxyHost, int.Parse(ProxyPort));
                //}
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Resource Cleanup */
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("" + ex.ToString(), "Error ftp delete -> ");
                Console.WriteLine(ex.ToString());
            }
            return;
        }
        /* Delete Dir*/
        public void deleteDir(string deleteFile)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + deleteFile);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Resource Cleanup */
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return;
        }
        /* Rename File */
        public void rename(string currentFileNameAndPath, string newFileName)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + currentFileNameAndPath);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.Rename;
                /* Rename the File */
                ftpRequest.RenameTo = newFileName;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Resource Cleanup */
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return;
        }

        /* Create a New Directory on the FTP Server */
        public void createDirectory(string newDirectory)
        {
            try
            {
                /* Create an FTP Request */
                //ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + newDirectory+"/");
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "//" + newDirectory);
                //ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/images/");
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                //ftpRequest.Proxy = new WebProxy();
                //ftpRequest.UsePassive = true;
                //if (ProxyProxyType.Equals("1"))
                //{
                //    //ftpRequest.Proxy = new WebProxy();
                //    //MessageBox.Show("ProxyHost  " + ProxyHost, "");
                //    if (ProxyHost.Length == 0)
                //    {
                //        //MessageBox.Show("ProxyHost  "+ ProxyHost, "");
                //    }
                //    else
                //    {
                //        int chk = 0;
                //        if (int.TryParse(ProxyPort, out chk))
                //        {
                //            ftpRequest.Proxy = new WebProxy(ProxyHost, chk);
                //            //MessageBox.Show("ProxyPort  " + ProxyPort+ " ProxyHost "+ ProxyHost, "");
                //        }
                //        else
                //        {
                //            //MessageBox.Show("ProxyPort  " + ProxyPort, "");
                //        }
                //    }
                //}
                //else
                //{
                //    //MessageBox.Show("ProxyProxyType NO  ", "");
                //}
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                //ftpRequest.EnableSsl = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Resource Cleanup */
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex)
            {
                //String status = ((FtpWebResponse)ex.Response).StatusDescription;
                //MessageBox.Show(" " + ex.ToString(), "Error createDirectory -> ");
                //new LogWriter("e", "createDirectory newDirectory " + host + "//" + newDirectory +" "+ ex.ToString());
                //Console.WriteLine(ex.ToString());
            }
            return;
        }

        /* Get the Date/Time a File was Created */
        public string getFileCreatedDateTime(string fileName)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Establish Return Communication with the FTP Server */
                ftpStream = ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                StreamReader ftpReader = new StreamReader(ftpStream);
                /* Store the Raw Response */
                string fileInfo = null;
                /* Read the Full Response Stream */
                try { fileInfo = ftpReader.ReadToEnd(); }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                /* Resource Cleanup */
                ftpReader.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
                /* Return File Created Date Time */
                return fileInfo;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return "";
        }

        /* Get the Size of a File */
        public string getFileSize(string fileName)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Establish Return Communication with the FTP Server */
                ftpStream = ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                StreamReader ftpReader = new StreamReader(ftpStream);
                /* Store the Raw Response */
                string fileInfo = null;
                /* Read the Full Response Stream */
                try { while (ftpReader.Peek() != -1) { fileInfo = ftpReader.ReadToEnd(); } }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                /* Resource Cleanup */
                ftpReader.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
                /* Return File Size */
                return fileInfo;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return "";
        }

        /* List Directory Contents File/Folder Name Only */
        public string[] directoryListSimple(string directory)
        {
            string[] directoryList;
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Establish Return Communication with the FTP Server */
                ftpStream = ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                StreamReader ftpReader = new StreamReader(ftpStream);
                /* Store the Raw Response */
                string directoryRaw = null;
                /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
                try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                /* Resource Cleanup */
                ftpReader.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
                Application.DoEvents();
                /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
                directoryList = directoryRaw.Split('|');
                if(directoryList.Length > 0) { return  directoryList; }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return new string[] { "" };
        }
        public List<String> directoryListSimple1(string directory)
        {
            List<String> directoryList = new List<string>();
            try
            {
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpStream = ftpResponse.GetResponseStream();
                StreamReader ftpReader = new StreamReader(ftpStream);
                string directoryRaw = null;
                try { while (ftpReader.Peek() != -1) {directoryList.Add(ftpReader.ReadLine()); Application.DoEvents(); } }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                ftpReader.Close();                ftpStream.Close();                ftpResponse.Close();                ftpRequest = null;
                Application.DoEvents();
                //foreach(String txt in directoryRaw.Split('|'))  directoryList.Add(txt);                
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return directoryList;
        }
        public List<String> directoryList(string directory)
        {
            List<String> directoryList = new List<string>();
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + directory);
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                //List<string> directories = new List<string>();
                string line = streamReader.ReadLine();
                while (!string.IsNullOrEmpty(line))
                {
                    directoryList.Add(line);
                    line = streamReader.ReadLine();
                }
                streamReader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return directoryList;
        }
        /* List Directory Contents in Detail (Name, Size, Created, etc.) */
        public string[] directoryListDetailed(string directory)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = ftpUsePassive;
                ftpRequest.KeepAlive = true;
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Establish Return Communication with the FTP Server */
                ftpStream = ftpResponse.GetResponseStream();
                /* Get the FTP Server's Response Stream */
                StreamReader ftpReader = new StreamReader(ftpStream);
                /* Store the Raw Response */
                string directoryRaw = null;
                /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
                try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                /* Resource Cleanup */
                ftpReader.Close();
                ftpStream.Close();
                ftpResponse.Close();
                ftpRequest = null;
                /* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
                try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            /* Return an Empty string Array if an Exception Occurs */
            return new string[] { "" };
        }
    }
}
