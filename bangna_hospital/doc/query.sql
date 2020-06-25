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
