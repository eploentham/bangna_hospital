-- ===================================================================
-- E-CLAIM DATABASE TABLES FOR MS SQL SERVER
-- ===================================================================

-- 1. ตาราง EClaim_Detail - เก็บข้อมูลรายละเอียดการเบิกจ่าย
-- ===================================================================
CREATE TABLE EClaim_Detail (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY,
    REP NVARCHAR(50),
    NO_ INT,
    TRAN_ID NVARCHAR(50),
    HN NVARCHAR(50),
    PID NVARCHAR(13),
    PatientName NVARCHAR(255),
    ServiceDate DATETIME2,
    ReferralNumber NVARCHAR(50),
    ServiceUnit NVARCHAR(10),
    
    -- ข้อมูลหน่วยบริการ
    ServiceType1 NVARCHAR(20),
    ServiceProv1 NVARCHAR(10),
    ServiceHcode NVARCHAR(50),
    ServiceType2 NVARCHAR(20),
    ServiceProv2 NVARCHAR(10),
    MainService NVARCHAR(100),
    ReferService NVARCHAR(100),
    
    -- ข้อมูลการวินิจฉัย
    DX_Code NVARCHAR(20),
    Proc_Code NVARCHAR(50),
    DMIS NVARCHAR(50),
    HMAIN3 NVARCHAR(50),
    DAR NVARCHAR(50),
    CA_TYPE NVARCHAR(20),
    
    -- ข้อมูลการชำระเงิน
    ClaimCase NVARCHAR(10),
    ClaimAmount DECIMAL(12,2) DEFAULT 0,
    TotalExpense DECIMAL(12,2) DEFAULT 0,
    CentralReimburse DECIMAL(12,2) DEFAULT 0,
    OPRefAmount DECIMAL(12,2) DEFAULT 0,
    OtherTreatment DECIMAL(12,2) DEFAULT 0,
    BeforeReduction DECIMAL(12,2) DEFAULT 0,
    AfterReduction DECIMAL(12,2) DEFAULT 0,
    
    -- ข้อมูลการจัดสรรงบประมาณ
    CUP_Province NVARCHAR(50),
    NHSO_Budget NVARCHAR(50),
    NetCompensation DECIMAL(12,2) DEFAULT 0,
    PaymentBy NVARCHAR(100),
    
    -- รายละเอียดค่าใช้จ่ายแต่ละประเภท
    HC01_Emergency DECIMAL(10,2) DEFAULT 0,
    HC02_CARAE DECIMAL(10,2) DEFAULT 0,
    HC03_OPINST DECIMAL(10,2) DEFAULT 0,
    HC04_DMISRC DECIMAL(10,2) DEFAULT 0,
    HC05_WorkLoad DECIMAL(10,2) DEFAULT 0,
    HC06_RCUHOSC DECIMAL(10,2) DEFAULT 0,
    HC07_RCUHOSR DECIMAL(10,2) DEFAULT 0,
    HC08_LLOP DECIMAL(10,2) DEFAULT 0,
    HC09_LP DECIMAL(10,2) DEFAULT 0,
    
    -- ค่าใช้จ่ายตามประเภทบริการ (เบิกได้/เบิกไม่ได้)
    RoomFood_Claimable DECIMAL(10,2) DEFAULT 0,
    RoomFood_NonClaimable DECIMAL(10,2) DEFAULT 0,
    MedicalDevice_Claimable DECIMAL(10,2) DEFAULT 0,
    MedicalDevice_NonClaimable DECIMAL(10,2) DEFAULT 0,
    HospitalDrug_Claimable DECIMAL(10,2) DEFAULT 0,
    HospitalDrug_NonClaimable DECIMAL(10,2) DEFAULT 0,
    HomeDrug_Claimable DECIMAL(10,2) DEFAULT 0,
    HomeDrug_NonClaimable DECIMAL(10,2) DEFAULT 0,
    NonDrugSupply_Claimable DECIMAL(10,2) DEFAULT 0,
    NonDrugSupply_NonClaimable DECIMAL(10,2) DEFAULT 0,
    BloodService_Claimable DECIMAL(10,2) DEFAULT 0,
    BloodService_NonClaimable DECIMAL(10,2) DEFAULT 0,
    LabTest_Claimable DECIMAL(10,2) DEFAULT 0,
    LabTest_NonClaimable DECIMAL(10,2) DEFAULT 0,
    Radiology_Claimable DECIMAL(10,2) DEFAULT 0,
    Radiology_NonClaimable DECIMAL(10,2) DEFAULT 0,
    SpecialDiag_Claimable DECIMAL(10,2) DEFAULT 0,
    SpecialDiag_NonClaimable DECIMAL(10,2) DEFAULT 0,
    MedicalEquip_Claimable DECIMAL(10,2) DEFAULT 0,
    MedicalEquip_NonClaimable DECIMAL(10,2) DEFAULT 0,
    Procedure_Claimable DECIMAL(10,2) DEFAULT 0,
    Procedure_NonClaimable DECIMAL(10,2) DEFAULT 0,
    NursingService_Claimable DECIMAL(10,2) DEFAULT 0,
    NursingService_NonClaimable DECIMAL(10,2) DEFAULT 0,
    DentalService_Claimable DECIMAL(10,2) DEFAULT 0,
    DentalService_NonClaimable DECIMAL(10,2) DEFAULT 0,
    PhysicalTherapy_Claimable DECIMAL(10,2) DEFAULT 0,
    PhysicalTherapy_NonClaimable DECIMAL(10,2) DEFAULT 0,
    Acupuncture_Claimable DECIMAL(10,2) DEFAULT 0,
    Acupuncture_NonClaimable DECIMAL(10,2) DEFAULT 0,
    OperatingRoom_Claimable DECIMAL(10,2) DEFAULT 0,
    OperatingRoom_NonClaimable DECIMAL(10,2) DEFAULT 0,
    MedicalFee_Claimable DECIMAL(10,2) DEFAULT 0,
    MedicalFee_NonClaimable DECIMAL(10,2) DEFAULT 0,
    OtherService_Claimable DECIMAL(10,2) DEFAULT 0,
    OtherService_NonClaimable DECIMAL(10,2) DEFAULT 0,
    Uncategorized_Claimable DECIMAL(10,2) DEFAULT 0,
    Uncategorized_NonClaimable DECIMAL(10,2) DEFAULT 0,
    
    -- ข้อมูลอื่นๆ
    ErrorCode NVARCHAR(20),
    DenyReason NVARCHAR(500),
    VA_Code NVARCHAR(50),
    Remark NVARCHAR(1000),
    AuditResult NVARCHAR(200),
    PaymentType NVARCHAR(100),
    
    -- ข้อมูล Audit
    CreatedDate DATETIME2 DEFAULT GETDATE(),
    ModifiedDate DATETIME2 DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- สร้าง Index สำหรับการค้นหา
CREATE INDEX IX_EClaim_Detail_PID ON EClaim_Detail(PID);
CREATE INDEX IX_EClaim_Detail_HN ON EClaim_Detail(HN);
CREATE INDEX IX_EClaim_Detail_TRAN_ID ON EClaim_Detail(TRAN_ID);
CREATE INDEX IX_EClaim_Detail_ServiceDate ON EClaim_Detail(ServiceDate);
CREATE INDEX IX_EClaim_Detail_REP ON EClaim_Detail(REP);

-- ===================================================================
-- 2. ตาราง EClaim_Summary - เก็บข้อมูลสรุปการเบิกจ่าย
-- ===================================================================
CREATE TABLE EClaim_Summary (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY,
    Period NVARCHAR(20),
    HCODE NVARCHAR(20),
    REP_NO NVARCHAR(50),
    
    -- ข้อมูลปกติ
    NormalData_Amount DECIMAL(15,2) DEFAULT 0,
    TotalCompensation DECIMAL(15,2) DEFAULT 0,
    
    -- ข้อมูลอุทธรณ์
    Appeal_Amount DECIMAL(15,2) DEFAULT 0,
    Appeal_Count INT DEFAULT 0,
    
    -- ข้อมูล OP REFER
    OPRefer_Normal_Amount DECIMAL(15,2) DEFAULT 0,
    OPRefer_Appeal_Amount DECIMAL(15,2) DEFAULT 0,
    OPRefer_Net_Amount DECIMAL(15,2) DEFAULT 0,
    
    NetCompensation DECIMAL(15,2) DEFAULT 0,
    
    -- ข้อมูล Audit
    CreatedDate DATETIME2 DEFAULT GETDATE(),
    ModifiedDate DATETIME2 DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- สร้าง Index
CREATE INDEX IX_EClaim_Summary_HCODE ON EClaim_Summary(HCODE);
CREATE INDEX IX_EClaim_Summary_Period ON EClaim_Summary(Period);
CREATE INDEX IX_EClaim_Summary_REP_NO ON EClaim_Summary(REP_NO);

-- ===================================================================
-- 3. ตาราง EClaim_DrugData - เก็บข้อมูลการเบิกยา
-- ===================================================================
CREATE TABLE EClaim_DrugData (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY,
    TRAN_ID NVARCHAR(50),
    HN NVARCHAR(50),
    PID NVARCHAR(13),
    
    -- ข้อมูลยา
    DrugCode NVARCHAR(50),
    DrugName NVARCHAR(500),
    DrugGenericName NVARCHAR(500),
    DrugStrength NVARCHAR(100),
    DrugUnit NVARCHAR(50),
    Quantity DECIMAL(10,2),
    UnitPrice DECIMAL(10,2),
    TotalPrice DECIMAL(12,2),
    
    -- ข้อมูลการจ่าย
    ClaimableAmount DECIMAL(12,2) DEFAULT 0,
    NonClaimableAmount DECIMAL(12,2) DEFAULT 0,
    
    -- ประเภทยา
    DrugType NVARCHAR(50), -- เช่น ยาในรพ., ยานำกลับบ้าน
    DrugCategory NVARCHAR(100),
    
    -- ข้อมูลการอนุมัติ
    ApprovalStatus NVARCHAR(20),
    DenyReason NVARCHAR(500),
    
    -- ข้อมูล Audit
    CreatedDate DATETIME2 DEFAULT GETDATE(),
    ModifiedDate DATETIME2 DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- สร้าง Index
CREATE INDEX IX_EClaim_DrugData_TRAN_ID ON EClaim_DrugData(TRAN_ID);
CREATE INDEX IX_EClaim_DrugData_PID ON EClaim_DrugData(PID);
CREATE INDEX IX_EClaim_DrugData_HN ON EClaim_DrugData(HN);
CREATE INDEX IX_EClaim_DrugData_DrugCode ON EClaim_DrugData(DrugCode);

-- ===================================================================
-- 4. ตาราง EClaim_BatchImport - เก็บข้อมูลการ Import
-- ===================================================================
CREATE TABLE EClaim_BatchImport (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY,
    FileName NVARCHAR(255),
    FilePath NVARCHAR(500),
    ImportDate DATETIME2 DEFAULT GETDATE(),
    ImportBy NVARCHAR(100),
    TotalRecords INT DEFAULT 0,
    SuccessRecords INT DEFAULT 0,
    ErrorRecords INT DEFAULT 0,
    Status NVARCHAR(20) DEFAULT 'Completed', -- Pending, Processing, Completed, Error
    ErrorLog NVARCHAR(MAX),
    
    -- ข้อมูล Summary
    TotalAmount DECIMAL(15,2) DEFAULT 0,
    
    CreatedDate DATETIME2 DEFAULT GETDATE(),
    IsActive BIT DEFAULT 1
);

-- สร้าง Index
CREATE INDEX IX_EClaim_BatchImport_ImportDate ON EClaim_BatchImport(ImportDate);
CREATE INDEX IX_EClaim_BatchImport_Status ON EClaim_BatchImport(Status);

-- ===================================================================
-- 5. เพิ่ม Foreign Key Relationships (Optional)
-- ===================================================================

-- เพิ่มคอลัมน์ BatchImportID ใน EClaim_Detail
ALTER TABLE EClaim_Detail ADD BatchImportID BIGINT;
ALTER TABLE EClaim_Summary ADD BatchImportID BIGINT;
ALTER TABLE EClaim_DrugData ADD BatchImportID BIGINT;

-- สร้าง Foreign Key
ALTER TABLE EClaim_Detail 
ADD CONSTRAINT FK_EClaim_Detail_BatchImport 
FOREIGN KEY (BatchImportID) REFERENCES EClaim_BatchImport(ID);

ALTER TABLE EClaim_Summary 
ADD CONSTRAINT FK_EClaim_Summary_BatchImport 
FOREIGN KEY (BatchImportID) REFERENCES EClaim_BatchImport(ID);

ALTER TABLE EClaim_DrugData 
ADD CONSTRAINT FK_EClaim_DrugData_BatchImport 
FOREIGN KEY (BatchImportID) REFERENCES EClaim_BatchImport(ID);

-- ===================================================================
-- 6. Stored Procedures สำหรับการจัดการข้อมูล
-- ===================================================================

-- Stored Procedure สำหรับดึงข้อมูลสรุปตาม PID
CREATE PROCEDURE sp_GetEClaimByPID
    @PID NVARCHAR(13)
AS
BEGIN
    SELECT 
        d.*,
        b.FileName,
        b.ImportDate
    FROM EClaim_Detail d
    LEFT JOIN EClaim_BatchImport b ON d.BatchImportID = b.ID
    WHERE d.PID = @PID
    AND d.IsActive = 1
    ORDER BY d.ServiceDate DESC;
END;

-- Stored Procedure สำหรับดึงข้อมูลสรุปตามช่วงวันที่
CREATE PROCEDURE sp_GetEClaimSummaryByDateRange
    @StartDate DATETIME2,
    @EndDate DATETIME2
AS
BEGIN
    SELECT 
        COUNT(*) as TotalCases,
        SUM(NetCompensation) as TotalCompensation,
        AVG(NetCompensation) as AvgCompensation,
        MAX(NetCompensation) as MaxCompensation,
        MIN(NetCompensation) as MinCompensation
    FROM EClaim_Detail
    WHERE ServiceDate BETWEEN @StartDate AND @EndDate
    AND IsActive = 1;
END;

-- ===================================================================
-- 7. Views สำหรับการรายงาน
-- ===================================================================

-- View สำหรับดูข้อมูลสรุปรายเดือน
CREATE VIEW vw_MonthlyEClaimSummary AS
SELECT 
    YEAR(ServiceDate) as ClaimYear,
    MONTH(ServiceDate) as ClaimMonth,
    COUNT(*) as TotalCases,
    SUM(NetCompensation) as TotalCompensation,
    AVG(NetCompensation) as AvgCompensation,
    SUM(CASE WHEN NetCompensation > 0 THEN 1 ELSE 0 END) as ApprovedCases,
    SUM(CASE WHEN NetCompensation = 0 THEN 1 ELSE 0 END) as RejectedCases
FROM EClaim_Detail
WHERE IsActive = 1
GROUP BY YEAR(ServiceDate), MONTH(ServiceDate);

-- View สำหรับดูข้อมูลตามหน่วยบริการ
CREATE VIEW vw_ServiceUnitSummary AS
SELECT 
    ServiceUnit,
    COUNT(*) as TotalCases,
    SUM(NetCompensation) as TotalCompensation,
    AVG(NetCompensation) as AvgCompensation
FROM EClaim_Detail
WHERE IsActive = 1
GROUP BY ServiceUnit;

-- ===================================================================
-- 8. Comments และ Documentation
-- ===================================================================

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'ตารางเก็บข้อมูลรายละเอียดการเบิกจ่าย E-Claim', 
    @level0type = N'Schema', @level0name = 'dbo', 
    @level1type = N'Table', @level1name = 'EClaim_Detail';

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'เลขที่บัตรประชาชน 13 หลัก', 
    @level0type = N'Schema', @level0name = 'dbo', 
    @level1type = N'Table', @level1name = 'EClaim_Detail',
    @level2type = N'Column', @level2name = 'PID';

EXEC sp_addextendedproperty 
    @name = N'MS_Description', 
    @value = N'จำนวนเงินชดเชยสุทธิ', 
    @level0type = N'Schema', @level0name = 'dbo', 
    @level1type = N'Table', @level1name = 'EClaim_Detail',
    @level2type = N'Column', @level2name = 'NetCompensation';

-- ===================================================================
-- 9. Sample Data Insert (ตัวอย่าง)
-- ===================================================================

/*
-- ตัวอย่างการ Insert ข้อมูล
INSERT INTO EClaim_BatchImport (FileName, ImportBy, TotalRecords)
VALUES ('eclaim_24036_ORF_25680612_155501995.xls', 'System', 1);

DECLARE @BatchID BIGINT = SCOPE_IDENTITY();

INSERT INTO EClaim_Detail (
    REP, NO_, TRAN_ID, HN, PID, PatientName, ServiceDate, 
    NetCompensation, BatchImportID
)
VALUES (
    '680600017', 1, '654327093', '5379694', '3700200432760', 
    'นาย สำเร็จ เวียนวัฒนะ', '2025-05-25 12:22:00', 
    150.00, @BatchID
);
*/

PRINT 'E-Claim database tables created successfully!';
PRINT 'Tables created:';
PRINT '1. EClaim_Detail - รายละเอียดการเบิกจ่าย';
PRINT '2. EClaim_Summary - ข้อมูลสรุป';
PRINT '3. EClaim_DrugData - ข้อมูลยา';
PRINT '4. EClaim_BatchImport - ข้อมูล Import';
PRINT '5. Views and Stored Procedures - สำหรับรายงาน';