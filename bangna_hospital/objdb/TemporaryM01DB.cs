using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bangna_hospital.objdb
{
    public class TemporaryM01DB
    {
        public ConnectDB conn;
        public TemporaryM01 tem01;

        public TemporaryM01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            tem01 = new TemporaryM01();
            tem01.MNC_DOC_CD = "MNC_DOC_CD";
            tem01.MNC_DOC_YR = "MNC_DOC_YR";
            tem01.MNC_DOC_NO = "MNC_DOC_NO";
            tem01.MNC_DOC_DAT = "MNC_DOC_DAT";
            tem01.MNC_HN_YR = "MNC_HN_YR";
            tem01.MNC_HN_NO = "MNC_HN_NO";
            tem01.MNC_AN_YR = "MNC_AN_YR";
            tem01.MNC_AN_NO = "MNC_AN_NO";
            tem01.MNC_HN_NAME = "MNC_HN_NAME";
            tem01.MNC_DIA_TIM = "MNC_DIA_TIM";
            tem01.MNC_ADM_TIM = "MNC_ADM_TIM";
            tem01.MNC_DIS_TIM = "MNC_DIS_TIM";
            tem01.MNC_COM_CD = "MNC_COM_CD";
            tem01.MNC_COM_NAME = "MNC_COM_NAME";
            tem01.MNC_COM_ADDR = "MNC_COM_ADDR";
            tem01.MNC_AGE = "MNC_AGE";
            tem01.MNC_SEX = "MNC_SEX";
            tem01.MNC_DIAG = "MNC_DIAG";
            tem01.MNC_RM_NAM = "MNC_RM_NAM";
            tem01.MNC_BED_NO = "MNC_BED_NO";
            tem01.MNC_VN_NO = "MNC_VN_NO";
            tem01.MNC_DOC_NO2 = "MNC_DOC_NO2";
            tem01.MNC_DAY = "MNC_DAY";
            tem01.MNC_DIS_DAT = "MNC_DIS_DAT";
            tem01.MNC_DIA_DAT = "MNC_DIA_DAT";
            tem01.MNC_ADM_DAT = "MNC_ADM_DAT";
            tem01.MNC_REM = "MNC_REM";
            tem01.MNC_SS_ID = "MNC_SS_ID";
            tem01.MNC_ACCI_CD = "MNC_ACCI_CD";
            tem01.MNC_DIA_DSC = "MNC_DIA_DSC";
            tem01.MNC_OR_DSC = "MNC_OR_DSC";
            tem01.MNC_DOT_NAME = "MNC_DOT_NAME";
            tem01.MNC_AR_BILL_NO = "MNC_AR_BILL_NO";
            tem01.AGE = "AGE";
            tem01.MALE = "MALE";
            tem01.MNC_FN_TYP_CD = "MNC_FN_TYP_CD";
            tem01.MNC_FN_TYP_DSC = "MNC_FN_TYP_DSC";
            tem01.CARD_NO = "CARD_NO";
            tem01.DIA_DSC_E1 = "DIA_DSC_E1";
            tem01.DIA_DSC_E2 = "DIA_DSC_E2";
            tem01.DIA_DSC_E3 = "DIA_DSC_E3";
            tem01.DIA_DSC_E4 = "DIA_DSC_E4";
            tem01.DIA_DSC_E5 = "DIA_DSC_E5";
            tem01.DIA_DSC_E6 = "DIA_DSC_E6";
            tem01.DIAOR_DSC_1 = "DIAOR_DSC_1";
            tem01.DIAOR_DSC_2 = "DIAOR_DSC_2";
            tem01.DIAOR_DSC_3 = "DIAOR_DSC_3";
            tem01.DIAOR_DSC_4 = "DIAOR_DSC_4";
            tem01.DIAOR_DSC_5 = "DIAOR_DSC_5";
            tem01.DIAOR_DSC_6 = "DIAOR_DSC_6";
            tem01.MNC_DIS_TOT = "MNC_DIS_TOT";
            tem01.MNC_CAR_DSC = "MNC_CAR_DSC";
            tem01.MNC_WARD_DSC = "MNC_WARD_DSC";
            tem01.MNC_TEXT_HOS = "MNC_TEXT_HOS";
            tem01.MNC_DOT_NAME1 = "MNC_DOT_NAME1";
            tem01.MNC_DOT_NAME2 = "MNC_DOT_NAME2";
            tem01.MNC_DOT_NAME3 = "MNC_DOT_NAME3";
            tem01.MNC_DOT_NAME4 = "MNC_DOT_NAME4";
            tem01.MNC_APPRV_CD = "MNC_APPRV_CD";
            tem01.MNC_IP_ADD1 = "MNC_IP_ADD1";
            tem01.MNC_IP_ADD2 = "MNC_IP_ADD2";
            tem01.MNC_IP_ADD3 = "MNC_IP_ADD3";
            tem01.MNC_IP_ADD4 = "MNC_IP_ADD4";
            tem01.MNC_SUM_AMT = "MNC_SUM_AMT";
            tem01.MNC_FLG_STS = "MNC_FLG_STS";
            tem01.MNC_AR_DOC_CD = "MNC_AR_DOC_CD";
            tem01.MNC_AR_DOC_YR = "MNC_AR_DOC_YR";
            tem01.MNC_AR_DOC_NO = "MNC_AR_DOC_NO";
            tem01.MNC_AR_DOC_DAT = "MNC_AR_DOC_DAT";
            tem01.MNC_PAY_TYP = "MNC_PAY_TYP";
            tem01.MNC_RE_PRN = "MNC_RE_PRN";
            tem01.MNC_FN_STS = "MNC_FN_STS";
            tem01.MNC_SPLIT_DOC = "MNC_SPLIT_DOC";

            tem01.table = "TEMPORARY_M01";
        }
        public DataTable selectSummaryOPD(String hn, String hnyr, String vn, String vsdate)
        {
            DataTable dt = new DataTable();
            String sql = "", whereAn = "";

            sql = "Select tem02.*, tem01.mnc_hn_name, tem01.mnc_com_name, tem01.mnc_com_addr, tem01.mnc_age, tem01.mnc_sex,convert(VARCHAR(20), tem01.mnc_doc_dat ,23) as doc_dat, tem02.MNC_def_lev " +
                ", tem01.MNC_SUM_AMT, tem01.MNC_FN_TYP_CD, tem01.MNC_FN_TYP_DSC,tem01.MNC_COM_NAME, tem01.MNC_DIA_DSC, convert(VARCHAR(20),tem01.MNC_DIS_DAT) as MNC_DIS_DAT, convert(VARCHAR(20),tem01.MNC_ADM_DAT) as MNC_ADM_DAT, convert(VARCHAR(20),tem01.MNC_DIA_DAT) as MNC_DIA_DAT " +
            "From temporary_m01 tem01 " +
            "inner join temporary_m02 tem02 on tem01.MNC_DOC_CD = tem02.MNC_DOC_CD and tem01.MNC_DOC_YR = tem02.MNC_DOC_YR and tem01.MNC_DOC_NO = tem02.MNC_DOC_NO and tem01.MNC_DOC_DAT = tem02.MNC_DOC_DAT " +
            "where tem01.mnc_hn_yr  = '"+ hnyr + "' and tem01.mnc_hn_no = '"+ hn + "' and tem01.mnc_vn_no = '"+vn+"' and tem01.mnc_doc_dat = '"+vsdate+"'" +
            "Order By tem02.mnc_ord_by ";
            //}
            dt = conn.selectData(sql);
            return dt;
        }
        public DataTable selectSummaryIPD(String hn, String hnyr, String an, String anyr, String doccd, String docno, String docyr)
        {
            DataTable dt = new DataTable();
            String sql = "", whereAn = "";

            sql = "Select tem02.*, tem01.mnc_hn_name, tem01.mnc_com_name, tem01.mnc_com_addr, tem01.mnc_age, tem01.mnc_sex,convert(VARCHAR(20), tem01.mnc_doc_dat ,23) as doc_dat, tem02.MNC_def_lev" +
                ", tem01.MNC_SUM_AMT, tem01.MNC_FN_TYP_CD, tem01.MNC_FN_TYP_DSC,tem01.MNC_COM_NAME, tem01.MNC_DIA_DSC, convert(VARCHAR(20),tem01.MNC_DIS_DAT) as MNC_DIS_DAT, convert(VARCHAR(20),tem01.MNC_ADM_DAT) as MNC_ADM_DAT, convert(VARCHAR(20),tem01.MNC_DIA_DAT) as MNC_DIA_DAT " +
            "From temporary_m01 tem01 " +
            "inner join temporary_m02 tem02 on tem01.MNC_DOC_CD = tem02.MNC_DOC_CD and tem01.MNC_DOC_YR = tem02.MNC_DOC_YR and tem01.MNC_DOC_NO = tem02.MNC_DOC_NO and tem01.MNC_DOC_DAT = tem02.MNC_DOC_DAT " +
            "where tem01.mnc_hn_yr  = '" + hnyr + "' and tem01.mnc_hn_no = '" + hn + "' and tem01.mnc_an_no = '" + an + "' and tem01.mnc_an_yr = '" + anyr + "' and tem01.mnc_doc_cd = '"+ doccd + "' and tem01.mnc_doc_no = '"+docno +"' and tem01.mnc_doc_yr = '"+docyr + "' "+
            "Order By tem02.mnc_ord_by ";
            //}
            dt = conn.selectData(sql);
            return dt;
        }
    }
}
