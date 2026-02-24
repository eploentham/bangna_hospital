using System;

namespace bangna_hospital.object1
{
    /// <summary>
    /// ข้อมูลการจองเตียง (Bed Reservation)
    /// </summary>
    public class PatientT10
    {
        // Primary Keys
        public string MNC_HN_YR { get; set; }
        public string MNC_HN_NO { get; set; }
        public int MNC_RSV_NO { get; set; }
        public DateTime MNC_RSV_DATE { get; set; }

        // Reservation Info
        public int? MNC_RSV_TIME { get; set; }
        public string MNC_REQ_STS { get; set; }
        public string MNC_HN_NAM { get; set; }
        public int? MNC_AD_DT_AMT { get; set; }
        public int? MNC_PRE_NO { get; set; }

        // Arrival Info
        public DateTime? MNC_ARV_DATE { get; set; }
        public int? MNC_ARV_TIME { get; set; }
        public string MNC_DEP_NO { get; set; }

        // Status
        public string MNC_RSV_STS { get; set; }
        public string MNC_AD_STS { get; set; }

        // Old Location
        public int? MNC_WD_NO_O { get; set; }
        public string MNC_RM_NAM_O { get; set; }
        public int? MNC_BD_NO_O { get; set; }

        // Current Location
        public int? MNC_WD_NO { get; set; }
        public string MNC_RM_NAM { get; set; }
        public int? MNC_BD_NO { get; set; }

        // Diagnosis & Doctor
        public string MNC_DIA_CD { get; set; }
        public string MNC_DIA_MEMO { get; set; }
        public string MNC_DOT_CD { get; set; }

        // Employee & Finance
        public string MNC_EMP_CD { get; set; }
        public DateTime? MNC_DATE { get; set; }
        public int? MNC_TIME { get; set; }
        public string MNC_FN_TYP_CD { get; set; }
        public string MNC_EMPR_CD { get; set; }
        public string MNC_EMPC_CD { get; set; }

        // Stamp
        public DateTime? MNC_STAMP_DAT { get; set; }
        public int? MNC_STAMP_TIM { get; set; }
        public string MNC_WAD_CD { get; set; }

        public PatientT10()
        {
            MNC_HN_YR = string.Empty;
            MNC_HN_NO = string.Empty;
            MNC_RSV_NO = 0;
            MNC_RSV_DATE = DateTime.Now;
            MNC_RSV_TIME = null;
            MNC_REQ_STS = "0";
            MNC_HN_NAM = string.Empty;
            MNC_AD_DT_AMT = null;
            MNC_PRE_NO = null;
            MNC_ARV_DATE = null;
            MNC_ARV_TIME = null;
            MNC_DEP_NO = string.Empty;
            MNC_RSV_STS = "0";
            MNC_AD_STS = "0";
            MNC_WD_NO_O = null;
            MNC_RM_NAM_O = string.Empty;
            MNC_BD_NO_O = null;
            MNC_WD_NO = null;
            MNC_RM_NAM = string.Empty;
            MNC_BD_NO = null;
            MNC_DIA_CD = string.Empty;
            MNC_DIA_MEMO = string.Empty;
            MNC_DOT_CD = string.Empty;
            MNC_EMP_CD = string.Empty;
            MNC_DATE = null;
            MNC_TIME = null;
            MNC_FN_TYP_CD = string.Empty;
            MNC_EMPR_CD = string.Empty;
            MNC_EMPC_CD = string.Empty;
            MNC_STAMP_DAT = null;
            MNC_STAMP_TIM = null;
            MNC_WAD_CD = string.Empty;
        }

        // Helper property สำหรับ HN เต็ม
        public string FullHN
        {
            get { return MNC_HN_YR + "-" + MNC_HN_NO; }
        }
    }
}
