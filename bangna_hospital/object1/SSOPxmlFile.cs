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
        public String genOpService(String hcode, String hname, String senddatetime, String session, List<SSOPOpservices> lopserv, List<SSOPOpdx> lopdx, String filename)
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
                inv += "<HNAME>" + hname + "</HNAME>" + Environment.NewLine;
                inv += "<DATETIME>" + senddatetime + "</DATETIME>" + Environment.NewLine;
                inv += "<SESSNO>" + session + "</SESSNO>" + Environment.NewLine;
                inv += "<RECCOUNT>" + lopserv.Count.ToString() + "</RECCOUNT></Header>" + Environment.NewLine;
                int i = 0;
                foreach (SSOPOpservices drow in lopserv)
                {
                    float svcharge = 0;
                    drow.svcharge = (float.TryParse(drow.svcharge, out svcharge)) ? svcharge.ToString("0.00") : "0.00";
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
            StringBuilder dispe = new StringBuilder();
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
                    float chargeamt = 0, claimamt = 0, paid = 0, otherpay = 0;
                    drow.chargeamt = (float.TryParse(drow.chargeamt, out chargeamt)) ? chargeamt.ToString("0.00") : "0.00";
                    drow.claimamt = (float.TryParse(drow.claimamt, out claimamt)) ? claimamt.ToString("0.00") : "0.00";
                    drow.paid = (float.TryParse(drow.paid, out paid)) ? paid.ToString("0.00") : "0.00";
                    drow.otherpay = (float.TryParse(drow.otherpay, out otherpay)) ? otherpay.ToString("0.00") : "0.00";
                    dispe.Append(drow.providerid + split);
                    dispe.Append(drow.dispid + split);
                    dispe.Append(drow.invno + split);
                    dispe.Append(drow.hn + split);
                    dispe.Append(drow.pid + split);
                    dispe.Append(drow.prescdt + split);
                    dispe.Append(drow.dispdt + split);
                    dispe.Append(drow.prescb + split);
                    dispe.Append(drow.itemcnt + split);
                    dispe.Append(drow.chargeamt + split);
                    dispe.Append(drow.claimamt + split);
                    dispe.Append(drow.paid + split);
                    dispe.Append(drow.otherpay + split);
                    dispe.Append(drow.reimburser + split);
                    dispe.Append(drow.benefitplan + split);
                    dispe.Append(drow.dispestat + split);
                    dispe.Append(drow.svid + split);
                    dispe.Append(drow.daycover + Environment.NewLine);
                    //billinv1.Append();
                }
                inv += "<Dispensing>" + Environment.NewLine;
                inv += dispe.ToString();
                inv += "</Dispensing>" + Environment.NewLine;
                dispe.Clear();
                foreach (SSOPDispenseditem item in ldispsi)
                {
                    float unitprice = 0, chargeamt = 0, reimbprice = 0, reimbamt = 0;
                    item.unitprice = (float.TryParse(item.unitprice, out unitprice)) ? unitprice.ToString("0.00") : "0.00";
                    item.chargeamt = (float.TryParse(item.chargeamt, out chargeamt)) ? chargeamt.ToString("0.00") : "0.00";
                    item.reimbprice = (float.TryParse(item.reimbprice, out reimbprice)) ? reimbprice.ToString("0.00") : "0.00";
                    item.reimbamt = (float.TryParse(item.reimbamt, out reimbamt)) ? reimbamt.ToString("0.00") : "0.00";
                    dispe.Append(item.dispid + split);
                    dispe.Append(item.prdcat + split);
                    dispe.Append(item.hospdrgid + split);
                    dispe.Append(item.drgid + split);
                    dispe.Append(item.dfscode + split);
                    dispe.Append(item.dfstext + split);
                    dispe.Append(item.packsize + split);
                    dispe.Append(item.sigcode + split);
                    dispe.Append(item.sigtext + split);
                    dispe.Append(item.quantity + split);
                    dispe.Append(item.unitprice + split);
                    dispe.Append(item.chargeamt + split);
                    dispe.Append(item.reimbprice + split);
                    dispe.Append(item.reimbamt + split);
                    dispe.Append(item.prdsecode + split);
                    dispe.Append(item.claimcont + split);
                    dispe.Append(item.claimcat + split);
                    dispe.Append(item.multidisp + split);
                    dispe.Append(item.supplyfor + Environment.NewLine);
                }
                inv += "<DispensedItems>" + Environment.NewLine;
                inv += dispe.ToString();
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
                    float amt = 0, paid=0, claimamt=0, otherpay=0;
                    drow.amount = (float.TryParse(drow.amount, out amt)) ? amt.ToString("0.00"):"0.00";
                    drow.paid = (float.TryParse(drow.paid, out paid)) ? paid.ToString("0.00") : "0.00";
                    drow.claimamt = (float.TryParse(drow.claimamt, out claimamt)) ? claimamt.ToString("0.00") : "0.00";
                    drow.otherpay = (float.TryParse(drow.otherpay, out otherpay)) ? otherpay.ToString("0.00") : "0.00";

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
                    float up1=0,chargeamt = 0, claimup = 0, claimamount = 0;
                    item.up1 = (float.TryParse(item.up1, out up1)) ? up1.ToString("0.00") : "0.00";
                    item.chargeamt = (float.TryParse(item.chargeamt, out chargeamt)) ? chargeamt.ToString("0.00") : "0.00";
                    item.claimup = (float.TryParse(item.claimup, out claimup)) ? claimup.ToString("0.00") : "0.00";
                    item.claimamount = (float.TryParse(item.claimamount, out claimamount)) ? claimamount.ToString("0.00") : "0.00";
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
