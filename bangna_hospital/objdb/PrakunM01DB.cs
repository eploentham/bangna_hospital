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

            prakM01.table = "PRAKUN_M01";
            prakM01.pkField = "SocialID";
        }
        public PrakunM01 selectByPID(String pid)
        {
            PrakunM01 pharT01 = new PrakunM01();
            DataTable dt = new DataTable();
            String reqno = "", sql = "";
            sql = "Select * " +
                "From "+ prakM01.table + " " +
                "Where Social_Card_no = '" + pid + "' ";
            dt = conn.selectData(sql);
            pharT01 = setPrakunM01(dt);
            return pharT01;
        }
        public PrakunM01 setPrakunM01(DataTable dt)
        {
            PrakunM01 pharT01 = new PrakunM01();
            if (dt.Rows.Count > 0)
            {
                pharT01.SocialID = dt.Rows[0]["SocialID"].ToString();
                pharT01.Social_Card_no = dt.Rows[0]["Social_Card_no"].ToString();
                pharT01.TitleName = dt.Rows[0]["TitleName"].ToString();
                pharT01.FirstName = dt.Rows[0]["FirstName"].ToString();
                pharT01.LastName = dt.Rows[0]["LastName"].ToString();
                pharT01.FullName = dt.Rows[0]["FullName"].ToString();
                pharT01.PrakanCode = dt.Rows[0]["PrakanCode"].ToString();
                pharT01.Prangnant = dt.Rows[0]["Prangnant"].ToString();
                pharT01.StartDate = dt.Rows[0]["StartDate"].ToString();
                pharT01.EndDate = dt.Rows[0]["EndDate"].ToString();
            }
            else
            {
                setPrakunM01(pharT01);
            }
            return pharT01;
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
            return p;
        }
    }
}
