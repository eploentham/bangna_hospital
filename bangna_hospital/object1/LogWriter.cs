using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class LogWriter
    {
        private string m_exePath = string.Empty;
        /*
         * type = g,e,w
         * g = general
         * e = error
         * w = warn
         * */
        public LogWriter(String type, string logMessage)
        {
            LogWrite(type,logMessage);
        }
        public LogWriter()
        {
            
        }
        private void LogWrite(String type,string logMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                checkLogFile();
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    Log(type, logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void checkLogFile()
        {
            if (File.Exists(m_exePath + "\\" + "log.txt"))
            {
                long len = new FileInfo(m_exePath + "\\" + "log.txt").Length;
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                if (len > 6000000)
                {
                    File.Delete(m_exePath + "\\" + "log.txt");
                    using (StreamWriter sw = File.CreateText(m_exePath + "\\" + "log.txt"))
                    {
                        sw.WriteLine("Log " + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                    }
                }
            }
            else
            {
                using (StreamWriter sw = File.CreateText(m_exePath + "\\" + "log.txt"))
                {
                    sw.WriteLine("Log " + System.DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss"));
                }
            }
        }
        public void WriteLog(String type, string logMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                checkLogFile();
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))
                {
                    Log(type, logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(String type,string logMessage, TextWriter txtWriter)
        {
            try
            {
                //txtWriter.Write("\r\nLog Entry : ");
                //txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToShortDateString(), type+"#"+DateTime.Now.ToShortTimeString() + "#" + logMessage);
                //txtWriter.WriteLine("  :");
                //txtWriter.Write("  :{0}", logMessage);
                //txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
