using bangna_hospital.object1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace bangna_hospital.objdb
{
    public class FinanceM01DB
    {
        public FinanceM01 finM01;
        ConnectDB conn;
        public FinanceM01DB(ConnectDB c)
        {
            conn = c;
            initConfig();
        }
        private void initConfig()
        {
            finM01 = new FinanceM01();
            finM01.MNC_FN_CD = "MNC_FN_CD";
            finM01.MNC_FN_CTL_CD = "MNC_FN_CTL_CD";
            finM01.MNC_FN_DSCT = "MNC_FN_DSCT";
            finM01.MNC_FN_DSCE = "MNC_FN_DSCE";
            finM01.MNC_FN_GRP = "MNC_FN_GRP";
            finM01.MNC_FN_STS = "MNC_FN_STS";
            finM01.MNC_FN_SP = "MNC_FN_SP";
            finM01.MNC_FN_DS = "MNC_FN_DS";
            finM01.MNC_FN_DS1 = "MNC_FN_DS1";
            finM01.MNC_FN_SS = "MNC_FN_SS";
            finM01.MNC_FN_SERP = "MNC_FN_SERP";
            finM01.MNC_FN_SERV = "MNC_FN_SERV";
            finM01.MNC_FN_SEP_FLG = "MNC_FN_SEP_FLG";
            finM01.MNC_FN_SEP_CD = "MNC_FN_SEP_CD";
            finM01.MNC_MULT_PR_FLG = "MNC_MULT_PR_FLG";
            finM01.MNC_FN_SERP_I = "MNC_FN_SERP_I";
            finM01.MNC_FN_SERV_I = "MNC_FN_SERV_I";
            finM01.MNC_AC_CD = "MNC_AC_CD";
            finM01.MNC_SUB_O = "MNC_SUB_O";
            finM01.MNC_SUB_I = "MNC_SUB_I";
            finM01.MNC_SUB_H = "MNC_SUB_H";
            finM01.MNC_SUB_P = "MNC_SUB_P";
            finM01.CHRGITEM = "CHRGITEM";
            finM01.REP_GRP = "REP_GRP";
            finM01.MNC_OVR_FLG = "MNC_OVR_FLG";
            finM01.MNC_GRP_SS = "MNC_GRP_SS";
            finM01.MNC_DEC_CD = "MNC_DEC_CD";
            finM01.MNC_DEC_NO = "MNC_DEC_NO";
            finM01.MNC_SIMB_CD = "MNC_SIMB_CD";
            finM01.MNC_STAMP_DAT = "MNC_STAMP_DAT";
            finM01.MNC_STAMP_TIM = "MNC_STAMP_TIM";
            finM01.MNC_USR_ADD = "MNC_USR_ADD";
            finM01.MNC_USR_UPD = "MNC_USR_UPD";
            finM01.MNC_FN_DF_STS = "MNC_FN_DF_STS";
            finM01.MNC_CHARGE_CD = "MNC_CHARGE_CD";
            finM01.MNC_SUB_CHARGE_CD = "MNC_SUB_CHARGE_CD";
            finM01.MNC_CAL_NURSE_FLG = "MNC_CAL_NURSE_FLG";
            finM01.MNC_NURSE_FN_CD = "MNC_NURSE_FN_CD";
            finM01.MNC_NURSE_PERCNT = "MNC_NURSE_PERCNT";
            finM01.MNC_NURSE_MIN_AMT = "MNC_NURSE_MIN_AMT";
            finM01.MNC_NURSE_MAX_AMT = "MNC_NURSE_MAX_AMT";
            finM01.MNC_IO_STS = "MNC_IO_STS";
            finM01.MNC_ENTRY_STS = "MNC_ENTRY_STS";
            finM01.MNC_NURSE_LOCK_STS = "MNC_NURSE_LOCK_STS";
            finM01.MNC_MAP_FN_STS = "MNC_MAP_FN_STS";
            finM01.MNC_INCOME_OTH_STS = "MNC_INCOME_OTH_STS";
            finM01.MNC_INCOME_OTH_AMT = "MNC_INCOME_OTH_AMT";
            finM01.MNC_INCOME_OTH_UPD_STS = "MNC_INCOME_OTH_UPD_STS";
            finM01.MNC_SUB_RO = "MNC_SUB_RO";
            finM01.MNC_SUB_RI = "MNC_SUB_RI";
            finM01.MNC_SUB_RH = "MNC_SUB_RH";
            finM01.MNC_UNLOCK_DSC = "MNC_UNLOCK_DSC";
            finM01.MNC_UNIT = "MNC_UNIT";
            finM01.MNC_UNIT_E = "MNC_UNIT_E";
            finM01.MNC_ACC_NO = "MNC_ACC_NO";
            finM01.mnc_grp_ss1 = "mnc_grp_ss1";
        }
        public DataTable SelectAll()
        {
            DataTable dt = new DataTable();

            String sql = "select * " +
                "From finance_m01  " +
                //"Where MNC_FN_TYP_STS = 'Y' " +
                "Order By MNC_FN_CD";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);MNC_DEF_DSC
            return dt;
        }
        public DataTable SelectAllOPBKK()
        {
            DataTable dt = new DataTable();

            String sql = "select fnm01.*, fnm06.MNC_FN_GRP_DSC, fnm11.MNC_DEF_DSC " +
                "From finance_m01 fnm01 " +
                "Left Join finance_m06 fnm06 on fnm06.mnc_fn_grp_cd = fnm01.MNC_FN_GRP " +
                "Left Join finance_m11 fnm11 on fnm11.mnc_def_cd = fnm01.mnc_simb_cd " +
                "Where MNC_FN_STS = 'Y' " +
                "Order By fnm01.MNC_FN_CD";
            dt = conn.selectData(sql);
            //new LogWriter("d", "SelectHnLabOut1 sql "+sql);
            return dt;
        }
    }
}
