CREATE TABLE [dbo].[appointments]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [patientFID] INT NOT NULL, 
    [doctorFID] INT NOT NULL, 
    [date] VARCHAR(50) NOT NULL, 
    [desc] VARCHAR(MAX) NULL, 
    [isconfirmed] INT NOT NULL,
    CONSTRAINT FK_PatientFID FOREIGN KEY (patientFID) REFERENCES dbo.[user](Id),
    CONSTRAINT FK_DoctorFID FOREIGN KEY (doctorFID) REFERENCES dbo.[user](Id)
)
