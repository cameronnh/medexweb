﻿/*
Deployment script for MedDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "MedDB"
:setvar DefaultFilePrefix "MedDB"
:setvar DefaultDataPath "C:\Users\Connor\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"
:setvar DefaultLogPath "C:\Users\Connor\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[patientPrescriptions].[deliveryFID] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[patientPrescriptions])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Rename refactoring operation with key 79f4bb9c-2314-4c44-956b-8d5f82b6dc2b is skipped, element [dbo].[Chats].[Name] (SqlSimpleColumn) will not be renamed to name';


GO
PRINT N'Rename refactoring operation with key 022ffb66-7f3e-4460-99b7-fc109b2fd237 is skipped, element [dbo].[Chats].[name] (SqlSimpleColumn) will not be renamed to topic';


GO
PRINT N'Dropping [dbo].[FK_doctorPatients_ToTable]...';


GO
ALTER TABLE [dbo].[bridgeDoctorPatient] DROP CONSTRAINT [FK_doctorPatients_ToTable];


GO
PRINT N'Dropping [dbo].[FK_doctorPatients_ToTable_1]...';


GO
ALTER TABLE [dbo].[bridgeDoctorPatient] DROP CONSTRAINT [FK_doctorPatients_ToTable_1];


GO
PRINT N'Dropping [dbo].[FK_patientPrescription_ToTable]...';


GO
ALTER TABLE [dbo].[patientPrescriptions] DROP CONSTRAINT [FK_patientPrescription_ToTable];


GO
PRINT N'Dropping [dbo].[FK_patientPrescription_ToTable_2]...';


GO
ALTER TABLE [dbo].[patientPrescriptions] DROP CONSTRAINT [FK_patientPrescription_ToTable_2];


GO
PRINT N'Dropping [dbo].[FK_deliveries_ToTable]...';


GO
ALTER TABLE [dbo].[deliveries] DROP CONSTRAINT [FK_deliveries_ToTable];


GO
PRINT N'Dropping [dbo].[FK_deliveries_ToTable_2]...';


GO
ALTER TABLE [dbo].[deliveries] DROP CONSTRAINT [FK_deliveries_ToTable_2];


GO
PRINT N'Dropping [dbo].[FK_PatientFID]...';


GO
ALTER TABLE [dbo].[appointments] DROP CONSTRAINT [FK_PatientFID];


GO
PRINT N'Dropping [dbo].[FK_DoctorFID]...';


GO
ALTER TABLE [dbo].[appointments] DROP CONSTRAINT [FK_DoctorFID];


GO
PRINT N'Dropping unnamed constraint on [dbo].[deliveries]...';


GO
ALTER TABLE [dbo].[deliveries] DROP CONSTRAINT [PK__deliveri__3214EC07F4C1AC94];


GO
PRINT N'Altering [dbo].[patientPrescriptions]...';


GO
ALTER TABLE [dbo].[patientPrescriptions] DROP COLUMN [deliveryFID];


GO
PRINT N'Starting rebuilding table [dbo].[user]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_user] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [fName]         NVARCHAR (50)  NULL,
    [lName]         NVARCHAR (50)  NULL,
    [email]         NVARCHAR (100) NOT NULL,
    [password]      NVARCHAR (100) NOT NULL,
    [phoneNumber]   NVARCHAR (50)  NULL,
    [streetAddress] NVARCHAR (100) NULL,
    [city]          NVARCHAR (100) NULL,
    [state]         NVARCHAR (100) NULL,
    [zipcode]       NVARCHAR (50)  NULL,
    [accountType]   INT            NOT NULL,
    [officeHours]   NVARCHAR (500) NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_user1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[user])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_user] ON;
        INSERT INTO [dbo].[tmp_ms_xx_user] ([Id], [fName], [lName], [email], [password], [phoneNumber], [streetAddress], [city], [state], [zipcode], [accountType], [officeHours])
        SELECT   [Id],
                 [fName],
                 [lName],
                 [email],
                 [password],
                 [phoneNumber],
                 [streetAddress],
                 [city],
                 [state],
                 [zipcode],
                 [accountType],
                 [officeHours]
        FROM     [dbo].[user]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_user] OFF;
    END

DROP TABLE [dbo].[user];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_user]', N'user';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_user1]', N'PK_user', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[Chats]...';


GO
CREATE TABLE [dbo].[Chats] (
    [Id]        INT          IDENTITY (1, 1) NOT NULL,
    [topic]     VARCHAR (50) NOT NULL,
    [patientID] INT          NULL,
    [doctorID]  INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[Messages]...';


GO
CREATE TABLE [dbo].[Messages] (
    [Id]     INT           IDENTITY (1, 1) NOT NULL,
    [userId] INT           NOT NULL,
    [text]   VARCHAR (MAX) NOT NULL,
    [user]   VARCHAR (50)  NOT NULL,
    [time]   VARCHAR (50)  NOT NULL,
    [date]   VARCHAR (50)  NOT NULL,
    [chatID] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[FK_doctorPatients_ToTable]...';


GO
ALTER TABLE [dbo].[bridgeDoctorPatient] WITH NOCHECK
    ADD CONSTRAINT [FK_doctorPatients_ToTable] FOREIGN KEY ([patientFID]) REFERENCES [dbo].[user] ([Id]);


GO
PRINT N'Creating [dbo].[FK_doctorPatients_ToTable_1]...';


GO
ALTER TABLE [dbo].[bridgeDoctorPatient] WITH NOCHECK
    ADD CONSTRAINT [FK_doctorPatients_ToTable_1] FOREIGN KEY ([doctorFID]) REFERENCES [dbo].[user] ([Id]);


GO
PRINT N'Creating [dbo].[FK_patientPrescription_ToTable]...';


GO
ALTER TABLE [dbo].[patientPrescriptions] WITH NOCHECK
    ADD CONSTRAINT [FK_patientPrescription_ToTable] FOREIGN KEY ([patientFID]) REFERENCES [dbo].[user] ([Id]);


GO
PRINT N'Creating [dbo].[FK_patientPrescription_ToTable_2]...';


GO
ALTER TABLE [dbo].[patientPrescriptions] WITH NOCHECK
    ADD CONSTRAINT [FK_patientPrescription_ToTable_2] FOREIGN KEY ([doctorFID]) REFERENCES [dbo].[user] ([Id]);


GO
PRINT N'Creating [dbo].[FK_deliveries_ToTable]...';


GO
ALTER TABLE [dbo].[deliveries] WITH NOCHECK
    ADD CONSTRAINT [FK_deliveries_ToTable] FOREIGN KEY ([patientFID]) REFERENCES [dbo].[user] ([Id]);


GO
PRINT N'Creating [dbo].[FK_deliveries_ToTable_2]...';


GO
ALTER TABLE [dbo].[deliveries] WITH NOCHECK
    ADD CONSTRAINT [FK_deliveries_ToTable_2] FOREIGN KEY ([doctorFID]) REFERENCES [dbo].[user] ([Id]);


GO
PRINT N'Creating [dbo].[FK_PatientFID]...';


GO
ALTER TABLE [dbo].[appointments] WITH NOCHECK
    ADD CONSTRAINT [FK_PatientFID] FOREIGN KEY ([patientFID]) REFERENCES [dbo].[user] ([Id]);


GO
PRINT N'Creating [dbo].[FK_DoctorFID]...';


GO
ALTER TABLE [dbo].[appointments] WITH NOCHECK
    ADD CONSTRAINT [FK_DoctorFID] FOREIGN KEY ([doctorFID]) REFERENCES [dbo].[user] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Chats_PatientID]...';


GO
ALTER TABLE [dbo].[Chats] WITH NOCHECK
    ADD CONSTRAINT [FK_Chats_PatientID] FOREIGN KEY ([patientID]) REFERENCES [dbo].[user] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Chats_DoctorID]...';


GO
ALTER TABLE [dbo].[Chats] WITH NOCHECK
    ADD CONSTRAINT [FK_Chats_DoctorID] FOREIGN KEY ([doctorID]) REFERENCES [dbo].[user] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Messages_ChatId]...';


GO
ALTER TABLE [dbo].[Messages] WITH NOCHECK
    ADD CONSTRAINT [FK_Messages_ChatId] FOREIGN KEY ([chatID]) REFERENCES [dbo].[Chats] ([Id]);


GO
PRINT N'Creating [dbo].[FK_Messages_UserId]...';


GO
ALTER TABLE [dbo].[Messages] WITH NOCHECK
    ADD CONSTRAINT [FK_Messages_UserId] FOREIGN KEY ([userId]) REFERENCES [dbo].[user] ([Id]);


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '79f4bb9c-2314-4c44-956b-8d5f82b6dc2b')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('79f4bb9c-2314-4c44-956b-8d5f82b6dc2b')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '022ffb66-7f3e-4460-99b7-fc109b2fd237')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('022ffb66-7f3e-4460-99b7-fc109b2fd237')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[bridgeDoctorPatient] WITH CHECK CHECK CONSTRAINT [FK_doctorPatients_ToTable];

ALTER TABLE [dbo].[bridgeDoctorPatient] WITH CHECK CHECK CONSTRAINT [FK_doctorPatients_ToTable_1];

ALTER TABLE [dbo].[patientPrescriptions] WITH CHECK CHECK CONSTRAINT [FK_patientPrescription_ToTable];

ALTER TABLE [dbo].[patientPrescriptions] WITH CHECK CHECK CONSTRAINT [FK_patientPrescription_ToTable_2];

ALTER TABLE [dbo].[deliveries] WITH CHECK CHECK CONSTRAINT [FK_deliveries_ToTable];

ALTER TABLE [dbo].[deliveries] WITH CHECK CHECK CONSTRAINT [FK_deliveries_ToTable_2];

ALTER TABLE [dbo].[appointments] WITH CHECK CHECK CONSTRAINT [FK_PatientFID];

ALTER TABLE [dbo].[appointments] WITH CHECK CHECK CONSTRAINT [FK_DoctorFID];

ALTER TABLE [dbo].[Chats] WITH CHECK CHECK CONSTRAINT [FK_Chats_PatientID];

ALTER TABLE [dbo].[Chats] WITH CHECK CHECK CONSTRAINT [FK_Chats_DoctorID];

ALTER TABLE [dbo].[Messages] WITH CHECK CHECK CONSTRAINT [FK_Messages_ChatId];

ALTER TABLE [dbo].[Messages] WITH CHECK CHECK CONSTRAINT [FK_Messages_UserId];


GO
PRINT N'Update complete.';


GO
