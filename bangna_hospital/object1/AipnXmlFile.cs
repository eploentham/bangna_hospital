using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class AipnXmlFile
    {
        public String DocClass = "", DocSysID = "", serviceEvent = "", authorID = "", authorName = "", effectiveTime = "";
        String split = "|";
        public AipnXmlFile()
        {

        }
        public String genHeader(String hcode, String efftime, String authorname)
        {
            String txt = "";
            txt = "<Header>"+Environment.NewLine;
            txt += "<DocClass>IPClaim</DocClass>" + Environment.NewLine;
            txt += @"<DocSysID version=""2.1"">AIPN</DocSysID>" + Environment.NewLine;
            txt += "<serviceEvent>ADT</serviceEvent>" + Environment.NewLine;
            txt += "<authorID>" + hcode + "</authorID>" + Environment.NewLine;
            txt += "<authorName>"+ authorname + "</authorName>" + Environment.NewLine;
            txt += "<effectiveTime>"+ efftime + "</effectiveTime>" + Environment.NewLine;
            txt += "</Header>" + Environment.NewLine;
            return txt;
        }
        public String genClaimAuth(String UPayPlan, String ServiceType, String ProjectCode, String EventCode, String Hmain, String Hcare, String CareAs, String ServiceSubType)
        {
            String txt = "";
            txt = "<ClaimAuth>" + Environment.NewLine;
            txt += "<AuthCode></AuthCode>" + Environment.NewLine;
            txt += "<AuthDT></AuthDT>"+Environment.NewLine;
            txt += "<UPayPlan>" + UPayPlan + "</UPayPlan>" + Environment.NewLine;
            txt += "<ServiceType>" + ServiceType + "</ServiceType>" + Environment.NewLine;
            txt += "<ProjectCode>" + ProjectCode + "</ProjectCode>" + Environment.NewLine;
            txt += "<EventCode>" + EventCode + "</EventCode>" + Environment.NewLine;
            txt += "<UserReserve>" + UPayPlan + "</UserReserve>" + Environment.NewLine;
            txt += "<Hmain>" + Hmain + "</Hmain>" + Environment.NewLine;
            txt += "<Hcare>" + Hcare + "</Hcare>" + Environment.NewLine;
            txt += "<CareAs>" + CareAs + "</CareAs>" + Environment.NewLine;
            txt += "<ServiceSubType>" + ServiceSubType + "</ServiceSubType>" + Environment.NewLine;
            txt += "</ClaimAuth>" + Environment.NewLine;
            return txt;
        }
        public String genPrefixAN(String an1)
        {
            String[] anno = an1.Split('.');
            String ancnt = "", prefixAn = "", anno1="";
            anno1 = "000000" + anno[0];
            anno1 = anno1.Substring(anno1.Length - 6);
            //new LogWriter("d", "genPrefixAN an1[1] " + anno[1]);
            ancnt = "0000" + anno[1];
            ancnt = ancnt.Substring(ancnt.Length - 4);
            //new LogWriter("d", "genPrefixAN ancnt " + ancnt);
            prefixAn = anno1 + "" + ancnt;
            return prefixAn;
        }
        public String genPrefixAN(String an, String ancnt1)
        {
            String anno = "", ancnt="", prefixAn="";
            anno = "000000" + an;
            anno = anno.Substring(anno.Length - 6);
            ancnt = "00" + ancnt1;
            ancnt = ancnt.Substring(ancnt.Length - 2);
            prefixAn = anno + "" + ancnt;
            return prefixAn;
        }
        public String genIPADT(String an, DataTable ipadt)
        {
            String txt = "", adt = "";
            String anno = "", prefixAn = "", ancnt = "", hcare = "", hn = "";
            txt = "<IPADT>" + Environment.NewLine;
            try
            {
                //prefixAn = genPrefixAN(ipadt.Rows[0]["ipadt_an"].ToString().ToString(),"");
                anno = an;
                adt += anno + split;
                adt += ipadt.Rows[0]["ipadt_hn"].ToString() + split;
                adt += ipadt.Rows[0]["idtype"].ToString() + split;
                adt += ipadt.Rows[0]["pidpat"].ToString() + split;
                adt += ipadt.Rows[0]["title1"].ToString() + split;
                adt += ipadt.Rows[0]["namepat"].ToString() + split;
                adt += ipadt.Rows[0]["dob"].ToString() + split;
                adt += ipadt.Rows[0]["sex"].ToString() + split;
                adt += ipadt.Rows[0]["marriage"].ToString() + split;
                adt += ipadt.Rows[0]["changwat"].ToString() + split;
                adt += ipadt.Rows[0]["amphur"].ToString() + split;
                adt += ipadt.Rows[0]["nation"].ToString() + split;
                adt += ipadt.Rows[0]["admtype"].ToString() + split;
                adt += ipadt.Rows[0]["admsource"].ToString() + split;
                adt += ipadt.Rows[0]["dtadm"].ToString() + split;
                adt += ipadt.Rows[0]["dtdisch"].ToString() + split;
                adt += ipadt.Rows[0]["leaveday"].ToString() + split;
                adt += ipadt.Rows[0]["dischstat"].ToString() + split;
                adt += ipadt.Rows[0]["dischtype"].ToString() + split;
                adt += ipadt.Rows[0]["admwt"].ToString() + split;
                adt += ipadt.Rows[0]["dischward"].ToString() + split;
                adt += ipadt.Rows[0]["dept"].ToString() + Environment.NewLine;
            }
            catch (Exception ex)
            {

            }
            txt += adt + "</IPADT>" + Environment.NewLine;
            return txt;
        }
        public String genIPDx(DataTable ipdx)
        {
            String txt = "", dx = "", diagterm="";
            try
            {
                txt = "<IPDx Reccount=\"" + ipdx.Rows.Count + "\">" + Environment.NewLine;
                foreach(DataRow drow in ipdx.Rows)
                {
                    diagterm = (drow["diagterm"].ToString().Length == 0) ? "N/A" : drow["diagterm"].ToString();     //กรณีที่ รพ ไม่มีข้อมูล  ให้ใส่   [N/A] เข้ามาครับ
                    dx += drow["sequence1"].ToString() + split;
                    dx += drow["dxtype"].ToString() + split;
                    dx += drow["codesys"].ToString() + split;
                    dx += drow["code"].ToString() + split;
                    dx += diagterm + split;
                    dx += drow["dr"].ToString() + split;
                    dx += drow["datediag"].ToString() + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {

            }
            txt += dx + "</IPDx>" + Environment.NewLine;
            return txt;
        }
        public String genIPOp(DataTable ipop)
        {
            String txt = "", op = "", procterm="";
            try
            {
                txt = "<IPOp Reccount=\"" + ipop.Rows.Count + "\">" + Environment.NewLine;
                foreach(DataRow drow in ipop.Rows)
                {
                    if (drow["sequence1"].ToString().Length <= 0) continue;
                    procterm = (drow["procterm"].ToString().Length == 0) ? "N/A" : drow["procterm"].ToString();     //กรณีที่ รพ ไม่มีข้อมูล  ให้ใส่   [N/A] เข้ามาครับ
                    op += drow["sequence1"].ToString() + split;
                    op += drow["codesys"].ToString() + split;
                    op += drow["code"].ToString() + split;
                    op += procterm + split;
                    op += drow["dr"].ToString() + split;
                    op += drow["datein"].ToString() + split;
                    op += drow["dateout"].ToString() + split;
                    op += drow["location1"].ToString() + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {

            }
            txt += op + "</IPOp>" + Environment.NewLine;
            return txt;
        }
        public String genBillItems(String InvNumber, String InvDT, DataTable billItms)
        {
            String txt = "", inv = "", billinv = "";
            try
            {

            }
            catch (Exception ex)
            {

            }
            txt += inv + "</Invoices>" + Environment.NewLine;
            return txt;
        }
        public String genInvoice(String IDX, String DT, String InvAddDiscount, String DRGCharge, String XDRGClaim, DataTable billItms)
        {
            String txt = "", inv = "", billinv = "";
            StringBuilder billinv1 = new StringBuilder();
            try
            {
                txt = "<Invoices>" + Environment.NewLine;
                inv = "<InvNumber>" + IDX + "</InvNumber>" + Environment.NewLine;
                inv += "<InvDT>" + DT + "</InvDT>" + Environment.NewLine;
                inv += "<BillItems Reccount=\"" + billItms.Rows.Count + "\">" + Environment.NewLine;
                int i = 0;
                foreach (DataRow drow in billItms.Rows)
                {
                    String disc = "";
                    disc = drow["discount"].ToString().Length==0 ?"0": drow["discount"].ToString();
                    i++;
                    //billinv += i.ToString() + split;
                    //billinv += drow["serdate"].ToString() + split;
                    //billinv += drow["billgr"].ToString() + split;
                    //billinv += drow["lccode"].ToString() + split;
                    ////billinv += drow["ServDate"].ToString() + split;
                    //billinv += drow["descript"].ToString().Replace("&","").Replace("<", "").Replace(">", "") + split;
                    //billinv += drow["qty"].ToString() + split;
                    //billinv += drow["unitprice"].ToString() + split;
                    //billinv += drow["chargeamt"].ToString() + split;
                    //billinv += disc + split;
                    //billinv += drow["prodedureseq"].ToString() + split;
                    //billinv += drow["diagnosisseq"].ToString() + split;
                    //billinv += drow["claimsys"].ToString() + split;
                    //billinv += drow["billgrcs"].ToString() + split;
                    //billinv += drow["cscode"].ToString() + split;
                    //billinv += drow["codesys"].ToString() + split;
                    //billinv += drow["stdcode"].ToString() + split;
                    //billinv += drow["claimcat"].ToString() + split;
                    //billinv += drow["daterev"].ToString() + split;
                    //billinv += drow["claimup"].ToString() + split;
                    //billinv += drow["claimamt"].ToString() + Environment.NewLine;
                    billinv1.Append(i.ToString() + split);
                    billinv1.Append(drow["serdate"].ToString() + split);
                    billinv1.Append(drow["billgr"].ToString() + split);
                    billinv1.Append(drow["lccode"].ToString() + split);
                    billinv1.Append(drow["descript"].ToString().Replace("&", "").Replace("<", "").Replace(">", "") + split);
                    billinv1.Append(drow["qty"].ToString() + split);
                    billinv1.Append(drow["unitprice"].ToString() + split);
                    billinv1.Append(drow["chargeamt"].ToString() + split);
                    billinv1.Append(disc + split);
                    billinv1.Append(drow["prodedureseq"].ToString() + split);
                    billinv1.Append(drow["diagnosisseq"].ToString() + split);
                    billinv1.Append(drow["claimsys"].ToString() + split);
                    billinv1.Append(drow["billgrcs"].ToString() + split);
                    billinv1.Append(drow["cscode"].ToString() + split);
                    billinv1.Append(drow["codesys"].ToString() + split);
                    billinv1.Append(drow["stdcode"].ToString() + split);
                    billinv1.Append(drow["claimcat"].ToString() + split);
                    billinv1.Append(drow["daterev"].ToString() + split);
                    billinv1.Append(drow["claimup"].ToString() + split);
                    billinv1.Append(drow["claimamt"].ToString() + Environment.NewLine);
                    //billinv1.Append();
                }
                //inv += billinv;
                inv += billinv1.ToString();
                inv += "</BillItems>" + Environment.NewLine;
                inv += "<InvAddDiscount>" + InvAddDiscount + "</InvAddDiscount>" + Environment.NewLine;
                inv += "<DRGCharge>" + DRGCharge + "</DRGCharge>" + Environment.NewLine;
                inv += "<XDRGClaim>" + XDRGClaim + "</XDRGClaim>" + Environment.NewLine;
            }
            catch (Exception ex)
            {

            }
            txt += inv + "</Invoices>" + Environment.NewLine;
            return txt;
        }
        public String genCoinsurance(String InsTypeCode, String InsTotal, String InsRoomBoard, String InsProfFee, String InsOther)
        {
            String txt = "";
            txt = "<Coinsurance>" + Environment.NewLine;
            txt += "<Insurance>" + Environment.NewLine;
            txt += "<InsTypeCode>" + InsTypeCode + "</InsTypeCode>" + Environment.NewLine;
            txt += "<InsTotal>" + InsTotal + "</InsTotal>" + Environment.NewLine;
            txt += "<InsRoomBoard>" + InsRoomBoard + "</InsRoomBoard>" + Environment.NewLine;
            txt += "<InsProfFee>" + InsProfFee + "</InsProfFee>" + Environment.NewLine;
            txt += "<InsOther>" + InsOther + "</InsOther>" + Environment.NewLine;
            txt += "</Insurance>" + Environment.NewLine;
            txt += "</Coinsurance>" + Environment.NewLine;
            return txt;
        }
    }
}
