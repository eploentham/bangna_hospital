using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bangna_hospital.object1
{
    public class LogFile
    {
        private string m_exePath = string.Empty;
        public LogFile(string logMessage)
        {
            LogWrite(logMessage);
        }
        public void LogWrite(string logMessage)
        {
            m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                double len = new FileInfo(m_exePath + "\\" + "log.txt").Length;
                string[] sizes = { "B", "KB", "MB", "GB", "TB" };
                //if (len > 1000)
                //{
                //    File.Delete(m_exePath + "\\" + "log.txt");
                //}
                if (len > 300000)
                {
                    File.Delete(m_exePath + "\\" + "log.txt");
                }
                using (StreamWriter w = File.AppendText(m_exePath + "\\" + "log.txt"))

                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("log error " + ex.Message, "");
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                //txtWriter.Write("\r\nLog Entry : ");
                //txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                //DateTime.Now.ToLongDateString());
                //txtWriter.WriteLine("  :");
                //txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine(logMessage);
                //txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
                MessageBox.Show("log error " + ex.Message, "");
            }
        }
    }
}
