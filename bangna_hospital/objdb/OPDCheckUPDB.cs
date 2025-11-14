using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace bangna_hospital.objdb
{
    public class OPDCheckUPDB
    {
        //Config1 cf;
        public ConnectDB connMainHIS;
        private ConnectDB conn;
        public OPDCheckUP opdc;
        public OPDCheckUPDB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            //connMainHIS = new ConnectDB("mainhis");
            opdc = new OPDCheckUP();
            opdc.ABOGroup = "ab_group";
            opdc.Active = "active";
            opdc.Addr1 = "addr1";
            opdc.Addr2 = "addr2";
            opdc.Sex = "sex";
            opdc.Age = "age";
            opdc.AllergicOther = "AllergicOther";
            opdc.AllergicStatus = "AllergicStatus";
            opdc.BloodPressure = "bloodpressure";
            opdc.Breath = "breath";
            opdc.CBCAtrLyN = "CBCAtrLyN";
            opdc.CBCAtrLyR = "CBCAtrLyR";
            opdc.CBCAtrLyV = "CBCAtrLyV";
            opdc.CBCBasN = "CBCBasN";
            opdc.CBCBasR = "CBCBasR";
            opdc.CBCBasV = "CBCBasV";
            opdc.CBCEosN = "CBCEosN";
            opdc.CBCEosR = "CBCEosR";
            opdc.CBCEosV = "CBCEosV";
            opdc.CBCHbN = "CBCHbN";
            opdc.CBCHbR = "CBCHbR";
            opdc.CBCHbV = "CBCHbV";
            opdc.CBCHctN = "CBCHctN";
            opdc.CBCHctR = "CBCHctR";
            opdc.CBCHctV = "CBCHctV";
            opdc.CBCLyN = "CBCLyN";
            opdc.CBCLyR = "CBCLyR";
            opdc.CBCLyV = "CBCLyV";
            opdc.CBCMchcN = "CBCMchcN";
            opdc.CBCMchcR = "CBCMchcR";
            opdc.CBCMchcV = "CBCMchcV";
            opdc.CBCMchN = "CBCMchN";
            opdc.CBCMchR = "CBCMchR";
            opdc.CBCMchV = "CBCMchV";
            opdc.CBCMcvN = "CBCMcvN";
            opdc.CBCMcvR = "CBCMcvR";
            opdc.CBCMcvV = "CBCMcvV";
            opdc.CBCMonoN = "CBCMonoN";
            opdc.CBCMonoR = "CBCMonoR";
            opdc.CBCMonoV = "CBCMonoV";
            opdc.CBCOtherCellN = "CBCOtherCellN";
            opdc.CBCOtherCellR = "CBCOtherCellR";
            opdc.CBCOtherCellV = "CBCOtherCellV";
            opdc.CBCPlaCntN = "CBCPlaCntN";
            opdc.CBCPlaCntR = "CBCPlaCntR";
            opdc.CBCPlaCntV = "CBCPlaCntV";
            opdc.CBCPLaSmeN = "CBCPLaSmeN";
            opdc.CBCPlaSmeR = "CBCPlaSmeR";
            opdc.CBCPlaSmeV = "CBCPlaSmeV";
            opdc.CBCPmnN = "CBCPmnN";
            opdc.CBCPmnR = "CBCPmnR";
            opdc.CBCPmnV = "CBCPmnV";
            opdc.CBCRBCCntN = "CBCRBCCntN";
            opdc.CBCRBCCntR = "CBCRBCCntR";
            opdc.CBCRBCCntV = "CBCRBCCntV";
            opdc.CBCRBCMorN = "CBCRBCMorN";
            opdc.CBCRBCMorR = "CBCRBCMorR";
            opdc.CBCRBCMorV = "CBCRBCMorV";
            opdc.CBCResult = "CBCResult";
            opdc.CBCSuggest = "CBCSuggest";
            opdc.CBCWBCCntN = "CBCWBCCntN";
            opdc.CBCWBCCntR = "CBCWBCCntR";
            opdc.CBCWBCCntV = "CBCWBCCntV";
            opdc.CheckDate = "checkup_date";
            opdc.CholN = "CholN";
            opdc.CholR = "CholR";
            opdc.CholResult = "CholResult";
            opdc.CholSuggest = "CholSuggest";
            opdc.CholV = "CholV";
            opdc.CreatiN = "CreatiN";
            opdc.CreatiR = "CreatiR";
            opdc.CreatiResult = "CreatiResult";
            opdc.CreatiSuggest = "CreatiSuggest";
            opdc.CreatiV = "CreatiV";
            opdc.FBSN = "FBSN";
            opdc.FBSR = "FBSR";
            opdc.FBSResult = "FBSResult";
            opdc.FBSSuggest = "FBSSuggest";
            opdc.FBSV = "FBSV";
            opdc.Height = "height";
            opdc.HistOther = "HistOther";
            opdc.HistStatus = "HistStatus";
            opdc.HN = "hn";
            opdc.Id = "opdcheckup_id";
            opdc.Name = "name";
            opdc.OROther = "OROther";
            opdc.ORStatus = "ORStatus";
            opdc.Phone = "phone";
            opdc.Pulse = "pulse";
            opdc.RhGroup = "rh_group";
            opdc.Sex = "sex";
            opdc.SgotN = "SgotN";
            opdc.SgotR = "SgotR";
            opdc.SgotResult = "SgotResult";
            opdc.SgotSuggest = "SgotSuggest";
            opdc.SgotV = "SgotV";
            opdc.SgptN = "SgptN";
            opdc.SgptR = "SgptR";
            opdc.SgptV = "SgptV";
            opdc.Tempu = "tempu";
            opdc.VN = "vn";
            opdc.Weight = "weight";
            opdc.Result = "result";
            opdc.Suggest = "suggest";
            opdc.PreNo = "preno";

            opdc.table = "opdcheckup";
            opdc.pkField = "opdcheckup_id";
        }
        private OPDCheckUP setData(OPDCheckUP p, DataTable dt)
        {
            opdc.ABOGroup = dt.Rows[0][opdc.ABOGroup].ToString();
            opdc.Active = dt.Rows[0][opdc.Active].ToString();
            opdc.Addr1 = dt.Rows[0][opdc.Addr1].ToString();
            opdc.Addr2 = dt.Rows[0][opdc.Addr2].ToString();
            opdc.Sex = dt.Rows[0][opdc.Sex].ToString();
            opdc.Age = dt.Rows[0][opdc.Age].ToString();
            opdc.AllergicOther = dt.Rows[0][opdc.AllergicOther].ToString();
            opdc.AllergicStatus = dt.Rows[0][opdc.AllergicStatus].ToString();
            opdc.BloodPressure = dt.Rows[0][opdc.BloodPressure].ToString();
            opdc.Breath = dt.Rows[0][opdc.Breath].ToString();
            opdc.CBCAtrLyN = dt.Rows[0][opdc.CBCAtrLyN].ToString();
            opdc.CBCAtrLyR = dt.Rows[0][opdc.CBCAtrLyR].ToString();
            opdc.CBCAtrLyV = dt.Rows[0][opdc.CBCAtrLyV].ToString();
            opdc.CBCBasN = dt.Rows[0][opdc.CBCBasN].ToString();
            opdc.CBCBasR = dt.Rows[0][opdc.CBCBasR].ToString();
            opdc.CBCBasV = dt.Rows[0][opdc.CBCBasV].ToString();
            opdc.CBCEosN = dt.Rows[0][opdc.CBCEosN].ToString();
            opdc.CBCEosR = dt.Rows[0][opdc.CBCEosR].ToString();
            opdc.CBCEosV = dt.Rows[0][opdc.CBCEosV].ToString();
            opdc.CBCHbN = dt.Rows[0][opdc.CBCHbN].ToString();
            opdc.CBCHbR = dt.Rows[0][opdc.CBCHbR].ToString();
            opdc.CBCHbV = dt.Rows[0][opdc.CBCHbV].ToString();
            opdc.CBCHctN = dt.Rows[0][opdc.CBCHctN].ToString();
            opdc.CBCHctR = dt.Rows[0][opdc.CBCHctR].ToString();
            opdc.CBCHctV = dt.Rows[0][opdc.CBCHctV].ToString();
            opdc.CBCLyN = dt.Rows[0][opdc.CBCLyN].ToString();
            opdc.CBCLyR = dt.Rows[0][opdc.CBCLyR].ToString();
            opdc.CBCLyV = dt.Rows[0][opdc.CBCLyV].ToString();
            opdc.CBCMchcN = dt.Rows[0][opdc.CBCMchcN].ToString();
            opdc.CBCMchcR = dt.Rows[0][opdc.CBCMchcR].ToString();
            opdc.CBCMchcV = dt.Rows[0][opdc.CBCMchcV].ToString();
            opdc.CBCMchN = dt.Rows[0][opdc.CBCMchN].ToString();
            opdc.CBCMchR = dt.Rows[0][opdc.CBCMchR].ToString();
            opdc.CBCMchV = dt.Rows[0][opdc.CBCMchV].ToString();
            opdc.CBCMcvN = dt.Rows[0][opdc.CBCMcvN].ToString();
            opdc.CBCMcvR = dt.Rows[0][opdc.CBCMcvR].ToString();
            opdc.CBCMcvV = dt.Rows[0][opdc.CBCMcvV].ToString();
            opdc.CBCMonoN = dt.Rows[0][opdc.CBCMonoN].ToString();
            opdc.CBCMonoR = dt.Rows[0][opdc.CBCMonoR].ToString();
            opdc.CBCMonoV = dt.Rows[0][opdc.CBCMonoV].ToString();
            opdc.CBCOtherCellN = dt.Rows[0][opdc.CBCOtherCellN].ToString();
            opdc.CBCOtherCellR = dt.Rows[0][opdc.CBCOtherCellR].ToString();
            opdc.CBCOtherCellV = dt.Rows[0][opdc.CBCOtherCellV].ToString();
            opdc.CBCPlaCntN = dt.Rows[0][opdc.CBCPlaCntN].ToString();
            opdc.CBCPlaCntR = dt.Rows[0][opdc.CBCPlaCntR].ToString();
            opdc.CBCPlaCntV = dt.Rows[0][opdc.CBCPlaCntV].ToString();
            opdc.CBCPLaSmeN = dt.Rows[0][opdc.CBCPLaSmeN].ToString();
            opdc.CBCPlaSmeR = dt.Rows[0][opdc.CBCPlaSmeR].ToString();
            opdc.CBCPlaSmeV = dt.Rows[0][opdc.CBCPlaSmeV].ToString();
            opdc.CBCPmnN = dt.Rows[0][opdc.CBCPmnN].ToString();
            opdc.CBCPmnR = dt.Rows[0][opdc.CBCPmnR].ToString();
            opdc.CBCPmnV = dt.Rows[0][opdc.CBCPmnV].ToString();
            opdc.CBCRBCCntN = dt.Rows[0][opdc.CBCRBCCntN].ToString();
            opdc.CBCRBCCntR = dt.Rows[0][opdc.CBCRBCCntR].ToString();
            opdc.CBCRBCCntV = dt.Rows[0][opdc.CBCRBCCntV].ToString();
            opdc.CBCRBCMorN = dt.Rows[0][opdc.CBCRBCMorN].ToString();
            opdc.CBCRBCMorR = dt.Rows[0][opdc.CBCRBCMorR].ToString();
            opdc.CBCRBCMorV = dt.Rows[0][opdc.CBCRBCMorV].ToString();
            opdc.CBCResult = dt.Rows[0][opdc.CBCResult].ToString();
            opdc.CBCSuggest = dt.Rows[0][opdc.CBCSuggest].ToString();
            opdc.CBCWBCCntN = dt.Rows[0][opdc.CBCWBCCntN].ToString();
            opdc.CBCWBCCntR = dt.Rows[0][opdc.CBCWBCCntR].ToString();
            opdc.CBCWBCCntV = dt.Rows[0][opdc.CBCWBCCntV].ToString();
            opdc.CheckDate = dt.Rows[0][opdc.CheckDate].ToString();
            opdc.CholN = dt.Rows[0][opdc.CholN].ToString();
            opdc.CholR = dt.Rows[0][opdc.CholR].ToString();
            opdc.CholResult = dt.Rows[0][opdc.CholResult].ToString();
            opdc.CholSuggest = dt.Rows[0][opdc.CholSuggest].ToString();
            opdc.CholV = dt.Rows[0][opdc.CholV].ToString();
            opdc.CreatiN = dt.Rows[0][opdc.CreatiN].ToString();
            opdc.CreatiR = dt.Rows[0][opdc.CreatiR].ToString();
            opdc.CreatiResult = dt.Rows[0][opdc.CreatiResult].ToString();
            opdc.CreatiSuggest = dt.Rows[0][opdc.CreatiSuggest].ToString();
            opdc.CreatiV = dt.Rows[0][opdc.CreatiV].ToString();
            opdc.FBSN = dt.Rows[0][opdc.FBSN].ToString();
            opdc.FBSR = dt.Rows[0][opdc.FBSR].ToString();
            opdc.FBSResult = dt.Rows[0][opdc.FBSResult].ToString();
            opdc.FBSSuggest = dt.Rows[0][opdc.FBSSuggest].ToString();
            opdc.FBSV = dt.Rows[0][opdc.FBSV].ToString();
            opdc.Height = dt.Rows[0][opdc.Height].ToString();
            opdc.HistOther = dt.Rows[0][opdc.HistOther].ToString();
            opdc.HistStatus = dt.Rows[0][opdc.HistStatus].ToString();
            opdc.HN = dt.Rows[0][opdc.HN].ToString();
            opdc.Id = dt.Rows[0][opdc.Id].ToString();
            opdc.Name = dt.Rows[0][opdc.Name].ToString();
            opdc.OROther = dt.Rows[0][opdc.OROther].ToString();
            opdc.ORStatus = dt.Rows[0][opdc.ORStatus].ToString();
            opdc.Phone = dt.Rows[0][opdc.Phone].ToString();
            opdc.Pulse = dt.Rows[0][opdc.Pulse].ToString();
            opdc.RhGroup = dt.Rows[0][opdc.RhGroup].ToString();
            opdc.Sex = dt.Rows[0][opdc.Sex].ToString();
            opdc.SgotN = dt.Rows[0][opdc.SgotN].ToString();
            opdc.SgotR = dt.Rows[0][opdc.SgotR].ToString();
            opdc.SgotResult = dt.Rows[0][opdc.SgotResult].ToString();
            opdc.SgotSuggest = dt.Rows[0][opdc.SgotSuggest].ToString();
            opdc.SgotV = dt.Rows[0][opdc.SgotV].ToString();
            opdc.SgptN = dt.Rows[0][opdc.SgptN].ToString();
            opdc.SgptR = dt.Rows[0][opdc.SgptR].ToString();
            opdc.SgptV = dt.Rows[0][opdc.SgptV].ToString();
            opdc.Tempu = dt.Rows[0][opdc.Tempu].ToString();
            opdc.VN = dt.Rows[0][opdc.VN].ToString();
            opdc.Weight = dt.Rows[0][opdc.Weight].ToString();
            opdc.Result = dt.Rows[0][opdc.Result].ToString();
            opdc.Suggest = dt.Rows[0][opdc.Suggest].ToString();
            opdc.PreNo = dt.Rows[0][opdc.PreNo].ToString();
            return p;
        }
        public OPDCheckUP selectByPk(String id)
        {
            OPDCheckUP item = new OPDCheckUP();
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select * From " + opdc.table + " Where " + opdc.pkField + "='" + id + "'";
            dt = conn.selectData(sql);
            if (dt.Rows.Count > 0)
            {
                item = setData(item, dt);
            }
            return item;
        }
        public OPDCheckUP selectByHn(String hn)
        {
            OPDCheckUP item = new OPDCheckUP();
            String sql = "";
            DataTable dt = new DataTable();
            sql = "Select * From " + opdc.table + " Where " + opdc.HN + "='" + hn + "' and "+opdc.Active+"='1'";
            dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)
            {
                item = setData(item, dt);
            }
            return item;
        }
        private String insert(OPDCheckUP p)
        {
            String sql = "", chk = "";
            try
            {
                if (p.Id == "")
                {
                    p.Id = p.getGenID();
                }
                //p.monthId = "RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)";
                //p.yearId = "CAST(year(GETDATE()) AS NVARCHAR)";
                //p.dateCreate = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)"
                //    + "' '+ RIGHT('00' + CAST(hour(GETDATE()) AS NVARCHAR), 2)+':'+ RIGHT('00' + CAST(minute(GETDATE()) AS NVARCHAR), 2)+':'+ RIGHT('00' + CAST(second(GETDATE()) AS NVARCHAR), 2)";
                //p.RowNumber = selectMaxRowNumber(p.YearId);
                p.Active = "1";
                sql = "Insert Into " + opdc.table + "(" + opdc.pkField + "," + opdc.ABOGroup + "," + opdc.Active + "," +
                    opdc.Addr1 + "," + opdc.Addr2 + "," + opdc.Sex + "," +
                    opdc.Age + "," + opdc.AllergicOther + "," + opdc.AllergicStatus + "," +
                    opdc.BloodPressure + "," + opdc.Breath + "," + opdc.CBCAtrLyN + "," +
                    opdc.CBCAtrLyR + "," + opdc.CBCAtrLyV + "," + opdc.CBCBasN + "," +
                    opdc.CBCBasR + "," + opdc.CBCBasV + "," + opdc.CBCEosN + "," +
                    opdc.CBCEosR + "," + opdc.CBCEosV + "," + opdc.CBCHbN + "," +
                    opdc.CBCHbR + "," + opdc.CBCHbV + "," + opdc.CBCHctN + "," +
                    opdc.CBCHctR + "," + opdc.CBCHctV + "," + opdc.CBCLyN + "," +
                    opdc.CBCLyR + "," + opdc.CBCLyV + "," + opdc.CBCMchcN + "," +
                    opdc.CBCMchcR + "," + opdc.CBCMchcV + "," + opdc.CBCMchN + "," +
                    opdc.CBCMchR + "," + opdc.CBCMchV + "," + opdc.CBCMcvN + "," +
                    opdc.CBCMcvR + "," + opdc.CBCMcvV + "," + opdc.CBCMonoN + "," +
                    opdc.CBCMonoR + "," + opdc.CBCMonoV + "," + opdc.CBCOtherCellN + "," +
                    opdc.CBCOtherCellR + "," + opdc.CBCOtherCellV + "," + opdc.CBCPlaCntN + "," +
                    opdc.CBCPlaCntR + "," + opdc.CBCPlaCntV + "," + opdc.CBCPLaSmeN + "," +
                    opdc.CBCPlaSmeR + "," + opdc.CBCPlaSmeV + "," + opdc.CBCPmnN + "," +
                    opdc.CBCPmnR + "," + opdc.CBCPmnV + "," + opdc.CBCRBCCntN + "," +
                    opdc.CBCRBCCntR + "," + opdc.CBCRBCCntV + "," + opdc.CBCRBCMorN + "," +
                    opdc.CBCRBCMorR + "," + opdc.CBCRBCMorV + "," + opdc.CBCResult + "," +
                    opdc.CBCSuggest + "," + opdc.CBCWBCCntN + "," + opdc.CBCWBCCntR + "," +
                    opdc.CBCWBCCntV + "," + opdc.CheckDate + "," + opdc.CholN + "," +
                    opdc.CholR + "," + opdc.CholResult + "," + opdc.CholSuggest + "," +
                    opdc.CholV + "," + opdc.CreatiN + "," + opdc.CreatiR + "," +
                    opdc.CreatiResult + "," + opdc.CreatiSuggest + "," + opdc.CreatiV + "," +
                    opdc.FBSN + "," + opdc.FBSR + "," + opdc.FBSResult + "," +
                    opdc.FBSSuggest + "," + opdc.FBSV + "," + opdc.Height + "," +
                    opdc.HistOther + "," + opdc.HistStatus + "," + opdc.HN + "," +
                    opdc.Name + "," + opdc.OROther + "," + opdc.ORStatus + "," +
                    opdc.Phone + "," + opdc.Pulse + "," + opdc.RhGroup + "," +
                    opdc.Sex + "," + opdc.SgotN + "," + opdc.SgotR + "," +
                    opdc.SgotResult + "," + opdc.SgotSuggest + "," + opdc.SgotV + "," +
                    opdc.SgptN + "," + opdc.SgptR + "," + opdc.SgptV + "," +
                    opdc.Tempu + "," + opdc.VN + "," + opdc.Weight +
                    opdc.Result + "," + opdc.Suggest + "," + opdc.PreNo + ") " +
                    "Values('" + p.Id + "','" + p.ABOGroup + "','" + p.Active + "','" +
                    p.Addr1 + "','" + p.Addr2 + "','" + p.Sex + "','" +
                    p.Age + "','" + p.AllergicOther + "','" + p.AllergicStatus + "','" +
                    p.BloodPressure + "','" + p.Breath + "','" + p.CBCAtrLyN + "','" +
                    p.CBCAtrLyR + "','" + p.CBCAtrLyV + "','" + p.CBCBasN + "','" +
                    p.CBCBasR + "','" + p.CBCBasV + "','" + p.CBCEosN + "','" +
                    p.CBCEosR + "','" + p.CBCEosV + "','" + p.CBCHbN + "','" +
                    p.CBCHbR + "','" + p.CBCHbV + "','" + p.CBCHctN + "','" +
                    p.CBCHctR + "','" + p.CBCHctV + "','" + p.CBCLyN + "','" +
                    p.CBCLyR + "','" + p.CBCLyV + "','" + p.CBCMchcN + "','" +
                    p.CBCMchcR + "','" + p.CBCMchcV + "','" + p.CBCMchN + "','" +
                    p.CBCMchR + "','" + p.CBCMchV + "','" + p.CBCMcvN + "','" +
                    p.CBCMcvR + "','" + p.CBCMcvV + "','" + p.CBCMonoN + "','" +
                    p.CBCMonoR + "','" + p.CBCMonoV + "','" + p.CBCOtherCellN + "','" +
                    p.CBCOtherCellR + "','" + p.CBCOtherCellV + "','" + p.CBCPlaCntN + "','" +
                    p.CBCPlaCntR + "','" + p.CBCPlaCntV + "','" + p.CBCPLaSmeN + "','" +
                    p.CBCPlaSmeR + "','" + p.CBCPlaSmeV + "','" + p.CBCPmnN + "','" +
                    p.CBCPmnR + "','" + p.CBCPmnV + "','" + p.CBCRBCCntN + "','" +
                    p.CBCRBCCntR + "','" + p.CBCRBCCntV + "','" + p.CBCRBCMorN + "','" +
                    p.CBCRBCMorR + "','" + p.CBCRBCMorV + "','" + p.CBCResult + "','" +
                    p.CBCSuggest + "','" + p.CBCWBCCntN + "','" + p.CBCWBCCntR + "','" +
                    p.CBCWBCCntV + "','" + p.CheckDate + "','" + p.CholN + "','" +
                    p.CholR + "','" + p.CholResult + "','" + p.CholSuggest + "','" +
                    p.CholV + "','" + p.CreatiN + "','" + p.CreatiR + "','" +
                    p.CreatiResult + "','" + p.CreatiSuggest + "','" + p.CreatiV + "','" +
                    p.FBSN + "','" + p.FBSR + "','" + p.FBSResult + "','" +
                    p.FBSSuggest + "','" + p.FBSV + "','" + p.Height + "','" +
                    p.HistOther + "','" + p.HistStatus + "','" + p.HN + "','" +
                    p.Name + "','" + p.OROther + "','" + p.ORStatus + "','" +
                    p.Phone + "','" + p.Pulse + "','" + p.RhGroup + "','" +
                    p.Sex + "','" + p.SgotN + "','" + p.SgotR + "','" +
                    p.SgotResult + "','" + p.SgotSuggest + "','" + p.SgotV + "','" +
                    p.SgptN + "','" + p.SgptR + "','" + p.SgptV + "','" +
                    p.Tempu + "','" + p.VN + "','" + p.Weight +"','"+
                    p.Result + "','" + p.Suggest + "','" + p.PreNo + "') ";
                chk = conn.ExecuteNonQuery(sql);
                //chk = p.RowNumber;
                chk = p.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "insert OPDCheckUP");
            }

            return chk;
        }
        public String update(OPDCheckUP p)
        {
            String sql = "", chk = "";
            try
            {
                //p.dateModi = " CAST(year(GETDATE()) AS NVARCHAR)+'-'+ RIGHT('00' + CAST(month(GETDATE()) AS NVARCHAR), 2)+'-'+ RIGHT('00' + CAST(day(GETDATE()) AS NVARCHAR), 2)";
                sql = "Update " + opdc.table + " Set " +// opdc.AddrE + "='" + p.AddrE + "'," +
                    opdc.Addr1 + "='" + p.Addr1 + "'," +
                    opdc.Addr2 + "='" + p.Addr2 + "'," +
                    opdc.Sex + "='" + p.Sex + "'," +
                    opdc.Age + "='" + p.Age + "'," +
                    opdc.AllergicOther + "='" + p.AllergicOther + "'," +
                    opdc.AllergicStatus + "='" + p.AllergicStatus + "'," +
                    opdc.BloodPressure + "='" + p.BloodPressure + "'," +
                    opdc.Breath + "='" + p.Breath + "'," +
                    opdc.CBCAtrLyN + "='" + p.CBCAtrLyN + "'," +
                    opdc.CBCAtrLyR + "='" + p.CBCAtrLyR + "'," +
                    opdc.CBCAtrLyV + "='" + p.CBCAtrLyV + "'," +
                    opdc.CBCBasN + "='" + p.CBCBasN + "'," +
                    opdc.CBCBasR + "='" + p.CBCBasR + "'," +
                    opdc.CBCBasV + "='" + p.CBCBasV + "'," +
                    opdc.CBCEosN + "='" + p.CBCEosN + "'," +
                    opdc.CBCEosR + "='" + p.CBCEosR + "'," +
                    opdc.CBCEosV + "='" + p.CBCEosV + "'," +
                    opdc.CBCHbN + "='" + p.CBCHbN + "'," +
                    opdc.CBCHbR + "='" + p.CBCHbR + "'," +
                    opdc.CBCHbV + "='" + p.CBCHbV + "'," +
                    opdc.CBCHctN + "='" + p.CBCHctN + "'," +
                    opdc.CBCHctR + "='" + p.CBCHctR + "'," +
                    opdc.CBCHctV + "='" + p.CBCHctV + "'," +
                    opdc.CBCLyN + "='" + p.CBCLyN + "'," +
                    opdc.CBCLyR + "='" + p.CBCLyR + "'," +
                    opdc.CBCLyV + "='" + p.CBCLyV + "'," +
                    opdc.CBCMchcN + "='" + p.CBCMchcN + "'," +
                    opdc.CBCMchcR + "='" + p.CBCMchcR + "'," +
                    opdc.CBCMchcV + "='" + p.CBCMchcV + "'," +
                    opdc.CBCMchN + "='" + p.CBCMchN + "'," +
                    opdc.CBCMchR + "='" + p.CBCMchR + "'," +
                    opdc.CBCMchV + "='" + p.CBCMchV + "'," +
                    opdc.CBCMcvN + "='" + p.CBCMcvN + "'," +
                    opdc.CBCMcvR + "='" + p.CBCMcvR + "'," +
                    opdc.CBCMcvV + "='" + p.CBCMcvV + "'," +
                    opdc.CBCMonoN + "='" + p.CBCMonoN + "'," +
                    opdc.CBCMonoR + "='" + p.CBCMonoR + "'," +
                    opdc.CBCMonoV + "='" + p.CBCMonoV + "'," +
                    opdc.CBCOtherCellN + "='" + p.CBCOtherCellN + "'," +
                    opdc.CBCOtherCellR + "='" + p.CBCOtherCellR + "'," +
                    opdc.CBCOtherCellV + "='" + p.CBCOtherCellV + "'," +
                    opdc.CBCPlaCntN + "='" + p.CBCPlaCntN + "'," +
                    opdc.CBCPlaCntR + "='" + p.CBCPlaCntR + "'," +
                    opdc.CBCPlaCntV + "='" + p.CBCPlaCntV + "'," +
                    opdc.CBCPLaSmeN + "='" + p.CBCPLaSmeN + "'," +
                    opdc.CBCPlaSmeR + "='" + p.CBCPlaSmeR + "'," +
                    opdc.CBCPlaSmeV + "='" + p.CBCPlaSmeV + "'," +
                    opdc.CBCPmnN + "='" + p.CBCPmnN + "'," +
                    opdc.CBCPmnR + "='" + p.CBCPmnR + "'," +
                    opdc.CBCPmnV + "='" + p.CBCPmnV + "'," +
                    opdc.CBCRBCCntN + "='" + p.CBCRBCCntN + "'," +
                    opdc.CBCRBCCntR + "='" + p.CBCRBCCntR + "'," +
                    opdc.CBCRBCCntV + "='" + p.CBCRBCCntV + "'," +
                    opdc.CBCRBCMorN + "='" + p.CBCRBCMorN + "'," +
                    opdc.CBCRBCMorR + "='" + p.CBCRBCMorR + "'," +
                    opdc.CBCRBCMorV + "='" + p.CBCRBCMorV + "'," +
                    opdc.CBCResult + "='" + p.CBCResult + "'," +
                    opdc.CBCSuggest + "='" + p.CBCSuggest + "'," +
                    opdc.CBCWBCCntN + "='" + p.CBCWBCCntN + "'," +
                    opdc.CBCWBCCntR + "='" + p.CBCWBCCntR + "'," +
                    opdc.CBCWBCCntV + "='" + p.CBCWBCCntV + "'," +
                    opdc.CheckDate + "='" + p.CheckDate + "'," +
                    opdc.CholN + "='" + p.CholN + "'," +
                    opdc.CholR + "='" + p.CholR + "'," +
                    opdc.CholResult + "='" + p.CholResult + "'," +
                    opdc.CholSuggest + "='" + p.CholSuggest + "'," +
                    opdc.CholV + "='" + p.CholV + "'," +
                    opdc.CreatiN + "='" + p.CreatiN + "'," +
                    opdc.CreatiR + "='" + p.CreatiR + "'," +
                    opdc.CreatiResult + "='" + p.CreatiResult + "'," +
                    opdc.CreatiSuggest + "='" + p.CreatiSuggest + "'," +
                    opdc.CreatiV + "='" + p.CreatiV + "'," +
                    opdc.FBSN + "='" + p.FBSN + "'," +
                    opdc.FBSR + "='" + p.FBSR + "'," +
                    opdc.FBSResult + "='" + p.FBSResult + "'," +
                    opdc.FBSSuggest + "='" + p.FBSSuggest + "'," +
                    opdc.FBSV + "='" + p.FBSV + "'," +
                    opdc.Height + "='" + p.Height + "'," +
                    opdc.HistOther + "='" + p.HistOther + "'," +
                    opdc.HistStatus + "='" + p.HistStatus + "'," +
                    opdc.HN + "='" + p.HN + "'," +
                    //opdc.Id + "='" + p.AddrE + "'," +
                    opdc.Name + "='" + p.Name + "'," +
                    opdc.OROther + "='" + p.OROther + "'," +
                    opdc.ORStatus + "='" + p.ORStatus + "'," +
                    opdc.Phone + "='" + p.Phone + "'," +
                    opdc.Pulse + "='" + p.Pulse + "'," +
                    opdc.RhGroup + "='" + p.RhGroup + "'," +
                    opdc.Sex + "='" + p.Sex + "'," +
                    opdc.SgotN + "='" + p.SgotN + "'," +
                    opdc.SgotR + "='" + p.SgotR + "'," +
                    opdc.SgotResult + "='" + p.SgotResult + "'," +
                    opdc.SgotSuggest + "='" + p.SgotSuggest + "'," +
                    opdc.SgotV + "='" + p.SgotV + "'," +
                    opdc.SgptN + "='" + p.SgptN + "'," +
                    opdc.SgptR + "='" + p.SgptR + "'," +
                    opdc.SgptV + "='" + p.SgptV + "'," +
                    opdc.Tempu + "='" + p.Tempu + "'," +
                    opdc.VN + "='" + p.VN + "'," +
                    opdc.PreNo + "='" + p.PreNo + "'," +
                    opdc.Weight + "='" + p.Weight + "' " +

                "Where " + opdc.pkField + "='" + p.Id + "'";
                chk = conn.ExecuteNonQuery(sql);
                chk = p.Id;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString(), "update OPDCheckUP");
            }
            return chk;
        }
        public String insertOPDCheckUP(OPDCheckUP p)
        {
            OPDCheckUP item = new OPDCheckUP();
            String chk = "";
            item = selectByPk(p.Id);
            if (item.Id == "")
            {
                chk = insert(p);
            }
            else
            {
                chk = update(p);
            }
            return chk;
        }
        public String VoidOPDCheckUP(String Id)
        {
            String sql = "", chk = "";
            sql = "Update " + opdc.table + " Set " + opdc.Active + " = '3' " +
                "Where " + opdc.pkField + "='" + Id + "'";
            try
            {
                chk = conn.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
            }

            return chk;
        }
    }
}
