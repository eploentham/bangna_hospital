drop table [dbo].[doc_group_scan];
CREATE TABLE [dbo].[doc_group_scan](
	[doc_group_id] [int] not NULL IDENTITY(1,1)  ,
	[doc_group_name] [varchar](255) NULL,
	[active] [varchar](255) NULL,
	[remark] [varchar](255) NULL,
	CONSTRAINT doc_group_id_pk PRIMARY KEY (doc_group_id)