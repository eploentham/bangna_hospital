using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class PrakunM01DB
    {
        public PrakunM01 prakM01;
        ConnectDB conn;
        public PrakunM01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            prakM01 = new PrakunM01();
            prakM01.SocialID = "SocialID";
            prakM01.Social_Card_no = "Social_Card_no";
            prakM01.TitleName = "TitleName";
            prakM01.FirstName = "FirstName";
            prakM01.LastName = "LastName";
            prakM01.FullName = "FullName";
            prakM01.PrakanCode = "PrakanCode";
            prakM01.Prangnant = "Prangnant";
            prakM01.StartDate = "StartDate";
            prakM01.EndDate = "EndDate";
            prakM01.FLAG = "FLAG";
            prakM01.UploadDate = "UploadDate";

            prakM01.table = "PRAKUN_M01";
            prakM01.pkField = "SocialID";
        }
        public DataTable selectSSOBySearch(String hn)
        {
            DataTable dt = new DataTable();
            String sql = "", wherehn = "";
            Patient ptt = new Patient();
            String[] txt = hn.Split(' ');
            if (txt.Length > 0)
            {
                if ((txt.Length > 1) && ((txt[1].Trim().Length > 0)))
                {
                    wherehn = " (prak01.FirstName like '" + txt[0].Trim() + "%') and (prak01.LastName like '" + txt[1].Trim() + "%')  ";
                }
                else if ((txt.Length == 1))
                {
                    if (long.TryParse(txt[0].Trim(), out long chk))//work_permit1
                    {
                        wherehn = " (prak01.SocialID like '" + txt[0].Trim() + "%') or (prak01.Social_Card_no like '" + txt[0].Trim() + "%')  ";
                    }
                    else
                    {
                        wherehn = " (prak01.FirstName like '" + txt[0].Trim() + "%') or (prak01.LastName like '" + txt[0].Trim() + "%') or (prak01.Social_Card_no like '" + txt[0].Trim() + "%') ";
                    }
                }
                else
                {
                    wherehn = " (prak01.FirstName like '" + txt[0].Trim() + "%') or (prak01.LastName like '" + txt[1].Trim() + "%')  ";
                }
            }
            else
            {
                wherehn = " prak01.SocialID like '" + hn + "%' or (prak01.Social_Card_no like '" + hn + "%') or (prak01.FirstName like '" + hn + "%') or (prak01.LastName like '" + hn + "%' ";
            }
            sql = "Select prak01.SocialID,prak01.Social_Card_no,prak01.FullName,prak01.PrakanCode,convert(VARCHAR(20),prak01.StartDate,23) as StartDate,convert(VARCHAR(20),prak01.EndDate,23) as EndDate,convert(VARCHAR(20),isnull(prak01.UploadDate,''),23) as UploadDate " +
                "From  PRAKUN_M01 prak01 " +
                " Where " + wherehn + " " +
                "Order By prak01.SocialID ";
            dt = conn.selectData(conn.connMainHIS, sql);
            return dt;
        }
        public PrakunM01 selectOne()
        {
            PrakunM01 pkunM01 = new PrakunM01();
            DataTable dt = new DataTable();
            String reqno = "", sql = "";
            sql = "Select top 1 convert(varchar(20),UploadDate,20) as UploadDate " +
                "From " + prakM01.table + " " ;
            dt = conn.selectData(sql);
            if(dt.Rows.Count > 0)
            {
                pkunM01.UploadDate = dt.Rows[0]["UploadDate"].ToString();
            }
            return pkunM01;
        }
        public PrakunM01 selectByPID(String pid)
        {
            PrakunM01 pkunM01 = new PrakunM01();
            DataTable dt = new DataTable();
            String reqno = "", sql = "";
            sql = "Select SocialID, Social_Card_no, TitleName, FirstName, LastName, FullName, PrakanCode, Prangnant, convert(varchar(20),StartDate,23) as StartDate, convert(varchar(20),EndDate,23) as EndDate, BirthDay, convert(varchar(20),UploadDate,20) as UploadDate, FLAG " +
                "From "+ prakM01.table + " " +
                "Where SocialID = '" + pid + "' ";
            dt = conn.selectData(sql);
            pkunM01 = setPrakunM01(dt);
            return pkunM01;
        }
        public PrakunM01 selectByCardNo(String pid)
        {
            PrakunM01 pkunM01 = new PrakunM01();
            DataTable dt = new DataTable();
            String reqno = "", sql = "";
            sql = "Select SocialID, Social_Card_no, TitleName, FirstName, LastName, FullName, PrakanCode, Prangnant, convert(varchar(20),StartDate,23) as StartDate, convert(varchar(20),EndDate,23) as EndDate, BirthDay, convert(varchar(20),UploadDate,20) as UploadDate, FLAG " +
                "From " + prakM01.table + " " +
                "Where Social_Card_no = '" + pid + "' ";
            dt = conn.selectData(sql);
            pkunM01 = setPrakunM01(dt);
            return pkunM01;
        }
        public PrakunM01 setPrakunM01(DataTable dt)
        {
            PrakunM01 pkunM01 = new PrakunM01();
            if (dt.Rows.Count > 0)
            {
                pkunM01.SocialID = dt.Rows[0]["SocialID"].ToString();
                pkunM01.Social_Card_no = dt.Rows[0]["Social_Card_no"].ToString();
                pkunM01.TitleName = dt.Rows[0]["TitleName"].ToString();
                pkunM01.FirstName = dt.Rows[0]["FirstName"].ToString();
                pkunM01.LastName = dt.Rows[0]["LastName"].ToString();
                pkunM01.FullName = dt.Rows[0]["FullName"].ToString();
                pkunM01.PrakanCode = dt.Rows[0]["PrakanCode"].ToString();
                pkunM01.Prangnant = dt.Rows[0]["Prangnant"].ToString();
                pkunM01.StartDate = dt.Rows[0]["StartDate"].ToString();
                pkunM01.EndDate = dt.Rows[0]["EndDate"].ToString();
                pkunM01.FLAG = dt.Rows[0]["FLAG"].ToString();
                pkunM01.UploadDate = dt.Rows[0]["UploadDate"].ToString();
            }
            else
            {
                setPrakunM01(pkunM01);
            }
            return pkunM01;
        }
        public PrakunM01 setPrakunM01(PrakunM01 p)
        {
            p.SocialID = "";
            p.Social_Card_no = "";
            p.TitleName = "";
            p.FirstName = "";
            p.LastName = "";
            p.FullName = "";
            p.PrakanCode = "";
            p.Prangnant = "";
            p.StartDate = "";
            p.EndDate = "";
            p.FLAG = "";
            p.UploadDate = "";
            return p;
        }
    }
}
