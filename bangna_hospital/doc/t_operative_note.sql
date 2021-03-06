USE [bn5_scan]
GO

/****** Object:  Table [dbo].[t_operative_note]    Script Date: 03/08/2020 11:17:07 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[t_operative_note](
	[operative_note_id] [int] IDENTITY(1080000000,1) NOT NULL,
	[dep_id] [nvarchar](255) NULL,
	[dep_name] [nvarchar](255) NULL,
	[ward_id] [nvarchar](255) NULL,
	[ward_name] [nvarchar](255) NULL,
	[attendind_stf_id] [nvarchar](255) NULL,
	[attendind_stf_name] [nvarchar](255) NULL,
	[patient_hn] [nvarchar](255) NULL,
	[patient_fullname] [nvarchar](255) NULL,
	[age] [nvarchar](255) NULL,
	[hn] [nvarchar](255) NULL,
	[an] [nvarchar](255) NULL,
	[mnc_date] [nvarchar](255) NULL,
	[pre_no] [nvarchar](255) NULL,
	[doc_scan_id] [int] NULL,
	[date_operation] [nvarchar](255) NULL,
	[time_start] [nvarchar](255) NULL,
	[time_finish] [nvarchar](255) NULL,
	[total_time] [nvarchar](255) NULL,
	[surgeon_id_1] [nvarchar](255) NULL,
	[surgeon_name_1] [nvarchar](255) NULL,
	[surgeon_id_2] [nvarchar](255) NULL,
	[surgeon_name_2] [nvarchar](255) NULL,
	[surgeon_id_3] [nvarchar](255) NULL,
	[surgeon_name_3] [nvarchar](255) NULL,
	[surgeon_id_4] [nvarchar](255) NULL,
	[surgeon_name_4] [nvarchar](255) NULL,
	[assistant_id_1] [nvarchar](255) NULL,
	[assistant_name_1] [nvarchar](255) NULL,
	[assistant_id_2] [nvarchar](255) NULL,
	[assistant_name_2] [nvarchar](255) NULL,
	[assistant_id_3] [nvarchar](255) NULL,
	[assistant_name_3] [nvarchar](255) NULL,
	[assistant_id_4] [nvarchar](255) NULL,
	[assistant_name_4] [nvarchar](255) NULL,
	[scrub_nurse_id_1] [nvarchar](255) NULL,
	[scrub_nurse_name_1] [nvarchar](255) NULL,
	[scrub_nurse_id_2] [nvarchar](255) NULL,
	[scrub_nurse_name_2] [nvarchar](255) NULL,
	[scrub_nurse_id_3] [nvarchar](255) NULL,
	[scrub_nurse_name_3] [nvarchar](255) NULL,
	[scrub_nurse_id_4] [nvarchar](255) NULL,
	[scrub_nurse_name_4] [nvarchar](255) NULL,
	[circulation_nurse_id_1] [nvarchar](255) NULL,
	[circulation_nurse_name_1] [nvarchar](255) NULL,
	[circulation_nurse_id_2] [nvarchar](255) NULL,
	[circulation_nurse_name_2] [nvarchar](255) NULL,
	[circulation_nurse_id_3] [nvarchar](255) NULL,
	[circulation_nurse_name_3] [nvarchar](255) NULL,
	[circulation_nurse_id_4] [nvarchar](255) NULL,
	[circulation_nurse_name_4] [nvarchar](255) NULL,
	[perfusionist_id_1] [nvarchar](255) NULL,
	[perfusionist_name_1] [nvarchar](255) NULL,
	[perfusionist_id_2] [nvarchar](255) NULL,
	[perfusionist_name_2] [nvarchar](255) NULL,
	[perfusionist_id_3] [nvarchar](255) NULL,
	[perfusionist_name_3] [nvarchar](255) NULL,
	[perfusionist_id_4] [nvarchar](255) NULL,
	[perfusionist_name_4] [nvarchar](255) NULL,
	[anesthetist_id_1] [nvarchar](255) NULL,
	[anesthetist_name_1] [nvarchar](255) NULL,
	[anesthetist_id_2] [nvarchar](255) NULL,
	[anesthetist_name_2] [nvarchar](255) NULL,
	[anesthetist_assistant_id_1] [nvarchar](255) NULL,
	[anesthetist_assistant_name_1] [nvarchar](255) NULL,
	[anesthetist_assistant_id_2] [nvarchar](255) NULL,
	[anesthetist_assistant_name_2] [nvarchar](255) NULL,
	[anesthesia_techique_id_1] [nvarchar](255) NULL,
	[anesthesia_techique_name_1] [nvarchar](255) NULL,
	[anesthesia_techique_id_2] [nvarchar](255) NULL,
	[anesthesia_techique_name_2] [nvarchar](255) NULL,
	[time_start_anesthesia_techique_1] [nvarchar](255) NULL,
	[time_finist_anesthesia_techique_1] [nvarchar](255) NULL,
	[time_start_anesthesia_techique_2] [nvarchar](255) NULL,
	[time_finish_anesthesia_techique_2] [nvarchar](255) NULL,
	[total_time_anesthesia_1] [nvarchar](255) NULL,
	[total_time_anesthesia_2] [nvarchar](255) NULL,
	[pre_operatation_diagnosis] [nvarchar](2000) NULL,
	[post_operation_diagnosis] [nvarchar](2000) NULL,
	[operation_1] [nvarchar](2000) NULL,
	[operation_2] [nvarchar](2000) NULL,
	[operation_3] [nvarchar](2000) NULL,
	[operation_4] [nvarchar](2000) NULL,
	[finding_1] [nvarchar](255) NULL,
	[finding_2] [nvarchar](255) NULL,
	[procidures_1] [nvarchar](255) NULL,
	[procidures_2] [nvarchar](255) NULL,
	[complication] [nvarchar](255) NULL,
	[estimated_blood_loss] [nvarchar](255) NULL,
	[tissue_biopsy] [nvarchar](255) NULL,
	[tissue_biopsy_unit] [nvarchar](255) NULL,
	[special_specimen] [nvarchar](255) NULL,
	[active] [nvarchar](255) NULL,
	[remark] [nvarchar](255) NULL,
	[date_create] [nvarchar](255) NULL,
	[date_modi] [nvarchar](255) NULL,
	[date_cancel] [nvarchar](255) NULL,
	[user_create] [nvarchar](255) NULL,
	[user_modi] [nvarchar](255) NULL,
	[user_cancel] [nvarchar](255) NULL,
 CONSTRAINT [PK_t_operative_note] PRIMARY KEY CLUSTERED 
(
	[operative_note_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

