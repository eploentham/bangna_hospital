using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class AipnDB
    {
        ConnectDB conn;

        public AipnDB(ConnectDB c)
        {
            this.conn = c;
            initConfig();
        }
        private void initConfig()
        {

        }
        public DataTable selectAipn(String aipnid)
        {
            DataTable dt = new DataTable();
            String sql = "select aipn.* " +
                "From aipn_t_aipn  aipn " +
                " Where aipn.aipn_id = '" + aipnid + "' " +
                "Order By aipn.aipn_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectAipnByErr1(String invid)
        {
            DataTable dt = new DataTable();
            String sql = "select billi.* " +
                "From aipn_t_invoice_billitems billi  " +
                " Where billi.invoice_id = '"+ invid + "' and billi.codesys = 'TMT' " +
                "Order By billi.invoice_billitems_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectAipnByErr()
        {
            DataTable dt = new DataTable();
            String sql = "select inv.* " +
                "From aipn_t_aipn  aipn " +
                "inner join aipn_t_invoice inv on aipn.aipn_id = inv.aipn_id " +
                " Where aipn.date_create >= '2024-04-11' and inv.active = '1' " +
                "Order By aipn.aipn_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectAipnByAnno(String anno)
        {
            DataTable dt = new DataTable();
            String sql = "select aipn.* " +
                "From aipn_t_aipn  aipn " +
                " Where aipn.an = '" + anno + "' " +
                "Order By aipn.aipn_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectClaimAuth(String aipnid)
        {
            DataTable dt = new DataTable();
            String sql = "select claim.aipn_claimauth_id,claim.aipn_id,claim.authcode,claim.authdt,claim.upayplan,claim.servicetype,isnull(claim.servicesubtype,'') as servicesubtype,isnull(claim.projectcode,'') as projectcode,isnull(claim.userreserve,'') as userreserve,isnull(claim.hmain,'') as hmain,isnull(claim.hcare,'') as hcare,isnull(claim.careas,'') as careas,isnull(claim.eventcode,'') as eventcode " +
                "From aipn_t_claimauth  claim " +
                " Where claim.aipn_id = '" + aipnid + "' " +
                "Order By claim.aipn_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectCoinsurance(String aipnid)
        {
            DataTable dt = new DataTable();
            String sql = "select coin.* " +
                "From aipn_t_coinsurance  coin " +
                " Where coin.aipn_id = '" + aipnid + "' " +
                "Order By coin.aipn_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectIPADT(String aipnid)
        {
            DataTable dt = new DataTable();
            String sql = "select ipadt.* " +
                "From aipn_t_ipadt  ipadt " +
                " Where ipadt.aipn_id = '" + aipnid + "' " +
                "Order By ipadt.aipn_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectIPDx(String aipnid)
        {
            DataTable dt = new DataTable();
            String sql = "select ipdx.* " +
                "From aipn_t_ipdx  ipdx " +
                " Where ipdx.aipn_id = '" + aipnid + "' " +
                "Order By ipdx.aipn_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectIPOp(String aipnid)
        {
            DataTable dt = new DataTable();
            String sql = "select ipop.* " +
                "From aipn_t_ipop  ipop " +
                " Where ipop.aipn_id = '" + aipnid + "' and ipop.sequence1 != '' " +
                "Order By ipop.aipn_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectInvoice(String aipnid)
        {
            DataTable dt = new DataTable();
            String sql = "select inv.* " +
                "From aipn_t_invoice  inv " +
                " Where inv.aipn_id = '" + aipnid + "' and inv.active = '1' " +
                "Order By inv.aipn_id,invoice desc ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectBillItems(String an_no, String ancnt)
        {
            //ใน table ไม่มี field aipn_id
            DataTable dt = new DataTable();
            String sql = "select bitems.* " +
                "From aipn_t_invoice_billitems  bitems " +
                //"inner join aipn_t_invoice inv on inv.invoice_id = bitems.invoice_id " +
                " Where bitems.an_no = '" + an_no + "' and bitems.an_cnt = '"+ancnt+"' and active = '1' " +
                //" Where bitems.an_no = '" + an_no + "' and bitems.an_cnt = '" + ancnt + "' and bitems.active = '1' " +
                "Order By bitems.invoice_billitems_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectBillItemsByAipn(String aipnid)
        {
            DataTable dt = new DataTable();
            String sql = "select bitems.* " +
                "From aipn_t_invoice_billitems  bitems " +
                "inner join aipn_t_invoice inv on inv.invoice_id = bitems.invoice_id " +
                " Where inv.aipn_id = '" + aipnid + "' and bitems.active = '1'  and inv.active = '1' " +
                "Order By inv.aipn_id ";
            dt = conn.selectData(conn.connSsnData, sql);

            return dt;
        }
        public DataTable selectAipnIdByStatusMakeText(String anno, Boolean sendMulti, Boolean statusNoAdd)
        {
            DataTable dt = new DataTable();
            String id = "", wheresessionno="";
            if (anno.Length > 0)
            {
                wheresessionno = " and an = '" + anno+"' ";//   รายการเก่า รายการเดียว
            }
            else if (sendMulti)
            {
                wheresessionno = " and sessionno != '' ";//   รายการเก่า หลายรายการ
            }
            else if (statusNoAdd)
            {
                wheresessionno = " and sessionno != '' ";// ทำเหมือน status ใหม่ ใช่ตอนแรก  ตาม สกส
            }
            else
            {
                wheresessionno = " and sessionno = '' ";// รายการใหม่
            }
            String sql = "select aipn.aipn_id, aipn.an_no, aipn.an_cnt " +
                "From aipn_t_aipn  aipn " +
                " Where aipn.status_send = '1' " +wheresessionno+" "
                +"Order By aipn.aipn_id ";
            //dt = conn.selectData( sql);
            new LogWriter("d", "AipnDB selectAipnIdByStatusMakeText " + sql);
            dt = conn.selectData(conn.connSsnData, sql);
            return dt;
        }
        public String selectMaxSessionNo()
        {
            DataTable dt = new DataTable();
            String id = "";
            String sql = "Select top 1 max(sessionno) as sessionno,aipn_claim_zip_id " +
                "from ssn_data.dbo.aipn_t_claim_zip Group By aipn_claim_zip_id " +
                "Order By aipn_claim_zip_id desc ;";
            //dt = conn.selectData( sql);
            dt = conn.selectData(conn.connSsnData, sql);
            if (dt.Rows.Count > 0)
            {
                id = dt.Rows[0]["sessionno"].ToString();
            }
            return id;
        }
        public String insertClaimZip(String hmain)
        {
            String sql = "", re = "", chk="";
            try
            {
                sql = "Insert Into ssn_data.dbo.aipn_t_claim_zip ("
                    + "hcode, doctype, sessionno, zip_file, status_claim, active) "
                    + "Values('" + hmain + "','AIPN',(select max(sessionno)+1 from ssn_data.dbo.aipn_t_claim_zip),'','0','1') ";
                chk = conn.ExecuteNonQuery(conn.connSsnData, sql);
                re = selectMaxSessionNo();
                //sql = "Update ssn_data.dbo.aipn_t_aipn set sessionno = '"+ re+"' Where ";
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                re = ex.Message;
            }
            finally
            {
               
            }
            return re;
        }
        public String updateSessionNoStatusMakeText(String sessionno, String aipnid)
        {
            String sql = "", re = "", chk = "";
            try
            {
                sql = "Update ssn_data.dbo.aipn_t_aipn set sessionno = '" + sessionno + "', status_send = '2' Where aipn_id = '"+ aipnid+"' ";
                chk = conn.ExecuteNonQuery(conn.connSsnData, sql);
                re = selectMaxSessionNo();
                //sql = "Update ssn_data.dbo.aipn_t_aipn set sessionno = '"+ re+"' Where ";
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                re = ex.Message;
            }
            finally
            {

            }
            return re;
        }
        public String updateSessionNoStatusSendEmail(String sessionno, String aipnid)
        {
            String sql = "", re = "", chk = "";
            try
            {
                sql = "Update ssn_data.dbo.aipn_t_aipn set  status_send = '4' Where aipn_id = '" + aipnid + "' ";
                chk = conn.ExecuteNonQuery(conn.connSsnData, sql);
                re = selectMaxSessionNo();
                //sql = "Update ssn_data.dbo.aipn_t_aipn set sessionno = '"+ re+"' Where ";
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                re = ex.Message;
            }
            finally
            {

            }
            return re;
        }
        public String updateInvBillItemsStdCode(String itemsid, String stdcode)
        {
            String sql = "", re = "", chk = "";
            try
            {
                sql = "Update ssn_data.dbo.aipn_t_invoice_billitems set  stdcode = '"+ stdcode + "' Where invoice_billitems_id = '" + itemsid + "' ";
                chk = conn.ExecuteNonQuery(conn.connSsnData, sql);
            }
            catch (Exception ex)
            {
                sql = ex.Message + " " + ex.InnerException;
                re = ex.Message;
            }
            finally
            {

            }
            return re;
        }
    }
}
