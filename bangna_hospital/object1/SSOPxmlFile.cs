using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.object1
{
    public class SSOPxmlFile
    {
        public String DocClass = "", DocSysID = "", serviceEvent = "", authorID = "", authorName = "", effectiveTime = "";
        String split = "|";
        public SSOPxmlFile()
        {

        }
        
        public String genHeader(String hcode, String efftime, String authorname)
        {
            String txt = "";
            txt = "<Header>" + Environment.NewLine;
            txt += "<DocClass>IPClaim</DocClass>" + Environment.NewLine;
            txt += @"<DocSysID version=""2.1"">AIPN</DocSysID>" + Environment.NewLine;
            txt += "<serviceEvent>ADT</serviceEvent>" + Environment.NewLine;
            txt += "<authorID>" + hcode + "</authorID>" + Environment.NewLine;
            txt += "<authorName>" + authorname + "</authorName>" + Environment.NewLine;
            txt += "<effectiveTime>" + efftime + "</effectiveTime>" + Environment.NewLine;
            txt += "</Header>" + Environment.NewLine;
            return txt;
        }
        public String genOpService(String hcode, String hmain, String senddatetime, String session, List<SSOPOpservices> lopserv, List<SSOPOpdx> lopdx, String filename)
        {
            String txt = "", inv = "", billinv = "";
            String HeaderXML = "<?xml version=\"1.0\" encoding=\"windows-874\"?>";
            String ClaimRec = "<ClaimRec System=\"OP\" PayPlan=\"SS\" Version=\"0.93\" Prgs=\"PD\" >";
            StringBuilder billinv1 = new StringBuilder();
            try
            {
                txt = HeaderXML + Environment.NewLine;
                txt += ClaimRec + Environment.NewLine;
                txt += "<Header>" + Environment.NewLine;
                inv = "<HCODE>" + hcode + "</HCODE>" + Environment.NewLine;
                inv += "<HNAME>" + hmain + "</HNAME>" + Environment.NewLine;
                inv += "<DATETIME>" + senddatetime + "</DATETIME>" + Environment.NewLine;
                inv += "<SESSNO>" + session + "</SESSNO>" + Environment.NewLine;
                inv += "<RECCOUNT>" + lopserv.Count.ToString() + "</RECCOUNT></Header>" + Environment.NewLine;
                int i = 0;
                foreach (SSOPOpservices drow in lopserv)
                {
                    billinv1.Append(drow.invno + split);
                    billinv1.Append(drow.svid + split);
                    billinv1.Append(drow.class1 + split);
                    billinv1.Append(drow.hcode + split);
                    billinv1.Append(drow.hn + split);
                    billinv1.Append(drow.pid + split);
                    billinv1.Append(drow.careaccount + split);
                    billinv1.Append(drow.typeserv + split);
                    billinv1.Append(drow.typein + split);
                    billinv1.Append(drow.typeout + split);
                    billinv1.Append(drow.dtappoint + split);
                    billinv1.Append(drow.svpid + split);
                    billinv1.Append(drow.clinic + split);
                    billinv1.Append(drow.begdt + split);
                    billinv1.Append(drow.enddt + split);
                    billinv1.Append(drow.lccode + split);
                    billinv1.Append(drow.codeset + split);
                    billinv1.Append(drow.stdcode + split);
                    billinv1.Append(drow.svcharge + split);
                    billinv1.Append(drow.completion + split);
                    billinv1.Append(drow.svtxcode + split);
                    billinv1.Append(drow.claimcat + Environment.NewLine);
                }
                inv += "<OPServices>" + Environment.NewLine;
                inv += billinv1.ToString();
                inv += "</OPServices>" + Environment.NewLine;
                billinv1.Clear();
                foreach (SSOPOpdx item in lopdx)
                {
                    billinv1.Append(item.class1 + split);
                    billinv1.Append(item.svid + split);
                    billinv1.Append(item.sl + split);
                    billinv1.Append(item.codeset + split);
                    billinv1.Append(item.code + split);
                    billinv1.Append(item.desc1 + Environment.NewLine);
                }
                inv += "<OPDx>" + Environment.NewLine;
                inv += billinv1.ToString();
                inv += "</OPDx>" + Environment.NewLine;
                //inv += "</ClaimRec>" + Environment.NewLine;
                inv += "</ClaimRec>";      //ไม่เอา Enter
                billinv1.Clear();
            }
            catch (Exception ex)
            {
            }
            txt += inv;
            byte[] txtwin874Bytes = ToWindows874Bytes(txt);
            byte[] md5 = GetMD5(txtwin874Bytes);
            String md51 = ComputeMD5Bytes(txtwin874Bytes);
            byte[] md51Bytes = Encoding.GetEncoding(874).GetBytes(md51);
            byte[] footerEndNote1 = ToWindows874Bytes("<?EndNote Checksum=\"");
            byte[] footerEndNote2 = ToWindows874Bytes("\"?>");
            byte[] enterbytes = Encoding.GetEncoding(874).GetBytes(Environment.NewLine);
            // รวม
            byte[] result = txtwin874Bytes.Concat(enterbytes).Concat(footerEndNote1).Concat(md51Bytes).Concat(footerEndNote2).ToArray();
            if (File.Exists(filename))
                File.Delete(filename);
            File.WriteAllBytes(filename, result);
            return txt;
        }
        public String genDispensing(String hcode, String hmain, String senddatetime, String session, List<SSOPDispensing> ldisps, List<SSOPDispenseditem> ldispsi, String filename)
        {
            String txt = "", inv = "", billinv = "";
            String HeaderXML = "<?xml version=\"1.0\" encoding=\"windows-874\"?>";
            String ClaimRec = "<ClaimRec System=\"IP\" PayPlan=\"SS\" Version=\"0.93\" Prgs=\"PD\" >";
            StringBuilder billinv1 = new StringBuilder();
            try
            {
                txt = HeaderXML + Environment.NewLine;
                txt += ClaimRec + Environment.NewLine;
                txt += "<Header>" + Environment.NewLine;
                inv = "<HCODE>" + hcode + "</HCODE>" + Environment.NewLine;
                inv += "<HNAME>" + hmain + "</HNAME>" + Environment.NewLine;
                inv += "<DATETIME>" + senddatetime + "</DATETIME>" + Environment.NewLine;
                inv += "<SESSNO>" + session + "</SESSNO>" + Environment.NewLine;
                inv += "<RECCOUNT>" + ldisps.Count.ToString() + "</RECCOUNT></Header>" + Environment.NewLine;
                int i = 0;
                foreach (SSOPDispensing drow in ldisps)
                {
                    billinv1.Append(drow.providerid + split);
                    billinv1.Append(drow.dispid + split);
                    billinv1.Append(drow.invno + split);
                    billinv1.Append(drow.hn + split);
                    billinv1.Append(drow.pid + split);
                    billinv1.Append(drow.prescdt + split);
                    billinv1.Append(drow.dispdt + split);
                    billinv1.Append(drow.prescb + split);
                    billinv1.Append(drow.itemcnt + split);
                    billinv1.Append(drow.chargeamt + split);
                    billinv1.Append(drow.claimamt + split);
                    billinv1.Append(drow.paid + split);
                    billinv1.Append(drow.otherpay + split);
                    billinv1.Append(drow.reimburser + split);
                    billinv1.Append(drow.benefitplan + split);
                    billinv1.Append(drow.dispestat + split);
                    billinv1.Append(drow.svid + split);
                    billinv1.Append(drow.daycover + Environment.NewLine);
                    //billinv1.Append();
                }
                inv += "<Dispensing>" + Environment.NewLine;
                inv += billinv1.ToString();
                inv += "</Dispensing>" + Environment.NewLine;
                billinv1.Clear();
                foreach (SSOPDispenseditem item in ldispsi)
                {
                    billinv1.Append(item.dispid + split);
                    billinv1.Append(item.prdcat + split);
                    billinv1.Append(item.hospdrgid + split);
                    billinv1.Append(item.drgid + split);
                    billinv1.Append(item.dfscode + split);
                    billinv1.Append(item.dfstext + split);
                    billinv1.Append(item.packsize + split);
                    billinv1.Append(item.sigcode + split);
                    billinv1.Append(item.sigtext + split);
                    billinv1.Append(item.quantity + split);
                    billinv1.Append(item.unitprice + split);
                    billinv1.Append(item.chargeamt + split);
                    billinv1.Append(item.reimbprice + split);
                    billinv1.Append(item.reimbamt + split);
                    billinv1.Append(item.prdsecode + split);
                    billinv1.Append(item.claimcont + split);
                    billinv1.Append(item.claimcat + split);
                    billinv1.Append(item.multidisp + split);
                    billinv1.Append(item.supplyfor + Environment.NewLine);
                }
                inv += "<DispensedItems>" + Environment.NewLine;
                inv += billinv1.ToString();
                inv += "</DispensedItems>" + Environment.NewLine;
                //inv += "</ClaimRec>" + Environment.NewLine;
                inv += "</ClaimRec>";      //ไม่เอา Enter
            }
            catch (Exception ex)
            {
            }
            txt += inv;
            byte[] txtwin874Bytes = ToWindows874Bytes(txt);
            byte[] md5 = GetMD5(txtwin874Bytes);
            String md51 = ComputeMD5Bytes(txtwin874Bytes);
            byte[] md51Bytes = Encoding.GetEncoding(874).GetBytes(md51);
            byte[] footerEndNote1 = ToWindows874Bytes("<?EndNote Checksum=\"");
            byte[] footerEndNote2 = ToWindows874Bytes("\"?>");
            byte[] enterbytes = Encoding.GetEncoding(874).GetBytes(Environment.NewLine);
            // รวม
            byte[] result = txtwin874Bytes.Concat(enterbytes).Concat(footerEndNote1).Concat(md51Bytes).Concat(footerEndNote2).ToArray();
            if (File.Exists(filename))
                File.Delete(filename);
            File.WriteAllBytes(filename, result);
            return txt;
        }
        public String genBillTrans(String hcode, String hmain, String senddatetime, String session, List<SSOPBillTran> lbilltrans, List<SSOPBillItems> lbillitems, String filename)
        {
            String txt = "", inv = "", billinv = "";
            String HeaderXML = "<?xml version=\"1.0\" encoding=\"windows-874\"?>";
            String ClaimRec = "<ClaimRec System=\"OP\" PayPlan=\"SS\" Version=\"0.93\" Prgs=\"PD\" >";
            StringBuilder billinv1 = new StringBuilder();
            try
            {
                txt = HeaderXML + Environment.NewLine;
                txt += ClaimRec + Environment.NewLine;
                txt += "<Header>" + Environment.NewLine;
                inv = "<HCODE>" + hcode + "</HCODE>" + Environment.NewLine;
                inv += "<HNAME>" + hmain + "</HNAME>" + Environment.NewLine;
                inv += "<DATETIME>" + senddatetime + "</DATETIME>" + Environment.NewLine;
                inv += "<SESSNO>" + session + "</SESSNO>" + Environment.NewLine;
                inv += "<RECCOUNT>" + lbilltrans.Count.ToString() + "</RECCOUNT></Header>" + Environment.NewLine;
                int i = 0;
                foreach (SSOPBillTran drow in lbilltrans)
                {
                    billinv1.Append(drow.station + split);
                    billinv1.Append(drow.authcode + split);
                    billinv1.Append(drow.dtran + split);
                    billinv1.Append(drow.hcode + split);
                    billinv1.Append(drow.invno + split);
                    billinv1.Append(drow.billno + split);
                    billinv1.Append(drow.hn + split);
                    billinv1.Append(drow.memberno + split);
                    billinv1.Append(drow.amount + split);
                    billinv1.Append(drow.paid + split);
                    billinv1.Append(drow.vercode + split);
                    billinv1.Append(drow.tflag + split);
                    billinv1.Append(drow.pid + split);
                    billinv1.Append(drow.name + split);
                    billinv1.Append(drow.hmain + split);
                    billinv1.Append(drow.payplan + split);
                    billinv1.Append(drow.claimamt + split);
                    billinv1.Append(drow.otherpayplan + split);
                    billinv1.Append(drow.otherpay + Environment.NewLine);
                    //billinv1.Append();
                }
                inv += "<BILLTRAN>" + Environment.NewLine;
                inv += billinv1.ToString();
                inv += "</BILLTRAN>" + Environment.NewLine;
                billinv1.Clear();
                foreach (SSOPBillItems item in lbillitems)
                {
                    billinv1.Append(item.invno + split);
                    billinv1.Append(item.svdate + split);
                    billinv1.Append(item.billmuad + split);
                    billinv1.Append(item.lccode + split);
                    billinv1.Append(item.stdcode + split);
                    billinv1.Append(item.desc1 + split);
                    billinv1.Append(item.qty + split);
                    billinv1.Append(item.up1 + split);
                    billinv1.Append(item.chargeamt + split);
                    billinv1.Append(item.claimup + split);
                    billinv1.Append(item.claimamount + split);
                    billinv1.Append(item.svrefid + split);
                    billinv1.Append(item.claimcat + Environment.NewLine);
                }
                inv += "<BillItems>" + Environment.NewLine;
                inv += billinv1.ToString();
                inv += "</BillItem>" + Environment.NewLine;
                //inv += "</ClaimRec>" + Environment.NewLine;
                inv += "</ClaimRec>" ;      //ไม่เอา Enter
            }
            catch (Exception ex)
            {

            }
            txt += inv;
            byte[] txtwin874Bytes = ToWindows874Bytes(txt);
            byte[] md5 = GetMD5(txtwin874Bytes);
            String md51 = ComputeMD5Bytes(txtwin874Bytes);
            byte[] md51Bytes = Encoding.GetEncoding(874).GetBytes(md51);
            byte[] footerEndNote1 = ToWindows874Bytes("<?EndNote Checksum=\"");
            byte[] footerEndNote2 = ToWindows874Bytes("\"?>");
            byte[] enterbytes = Encoding.GetEncoding(874).GetBytes(Environment.NewLine);
            // รวม
            byte[] result = txtwin874Bytes.Concat(enterbytes).Concat(footerEndNote1).Concat(md51Bytes).Concat(footerEndNote2).ToArray();
            if(File.Exists(filename))
                File.Delete(filename);
            File.WriteAllBytes(filename, result);
            return txt;
        }
        public String ComputeMD5Bytes(byte[] input)
        {
            StringBuilder sb = new StringBuilder();
            // Initialize a MD5 hash object
            using (MD5 md5 = MD5.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = md5.ComputeHash(input);
                // Convert the byte array to string format
                foreach (byte b in hashValue)
                {
                    sb.Append($"{b:X2}");
                }
            }
            return sb.ToString();
        }
        public byte[] ToWindows874Bytes(string input)
        {
            if (input == null) return new byte[0];
            Encoding win874 = Encoding.GetEncoding("windows-874");
            return win874.GetBytes(input);
        }
        public byte[] GetMD5(byte[] input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(input);
                return hashBytes;
            }
        }
    }
}
