drop table [dbo].[doc_group_scan];
CREATE TABLE [dbo].[doc_group_scan](
	[doc_group_id] [int] not NULL IDENTITY(1,1)  ,
	[doc_group_name] [varchar](255) NULL,
	[active] [varchar](255) NULL,
	[remark] [varchar](255) NULL,
	CONSTRAINT doc_group_id_pk PRIMARY KEY (doc_group_id)



	insert into PacsPlus.dbo.ResOrderTab(ResOrderKey, Orderclass, PatientID, AccessNumber, KPatientName, EPatientName
,DateOfBirth, PatientSex, PatientClass, Modality, StudyDate, SicknessName
, SicknessCode, ProcDesc, ResStatus, ResStatusPolling, InsertDate, OrderDate,
PhysicianID, PhysicianName, OrderDept, ModalityCode, ExamCode, ExamDescription,
ReadingPriority,  ReqPhysicianID, ReqPhysicianName, StationAE
)
values('29142917143','NEW','5033707','2006082914293','Mr. Test','Mr. Ekapop Ploentham'
,'19720513','M','O','CR','20190822105000','interface PACs'
,'100','Test Chest','U','N','20190822105000','20190822105000'
,'admin','administrator','IT','RAD','017DV','Neck'
,'0','H8','Hong Gil dong','XG-1'
);


insert into bn1_outlab.dbo.b_company
(comp_name_e, comp_name_t, active)
values('RIA','RIA','1');

insert into bn1_outlab.dbo.b_company
(comp_name_e, comp_name_t, active)
values('Medica','Medica','1');

insert into bn1_outlab.dbo.b_company
(comp_name_e, comp_name_t, active)
values('GM','GM','1');


select *
from mc_result
where hospital_code = 'CT-MD0076' and updatedate = '2020-06-26'

select *
from XRAY_T02
where MNC_REQ_YR = '2563' and MNC_REQ_DAT = '2020-07-02' and MNC_XR_CD in ('UAB008','XCH001') and MNC_REQ_NO = '10745';

update XRAY_T02
set status_pacs = '0'
where MNC_REQ_YR = '2563' and MNC_REQ_DAT = '2020-07-02' and MNC_XR_CD in ('UAB008','XCH001') and MNC_REQ_NO = '10745';

Update xray_t02 Set status_pacs = '1' Where mnc_req_no = '10745' and mnc_req_yr = '2563' and mnc_xr_cd = 'UAB008' and mnc_req_dat = '2020-07-02'