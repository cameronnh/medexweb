﻿/*
Deployment script for medexDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "medexDB"
:setvar DefaultFilePrefix "medexDB"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"

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
PRINT N'Creating [dbo].[patientPrescription]...';


GO
CREATE TABLE [dbo].[patientPrescription] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [patientFID]       INT           NOT NULL,
    [prescriptionFID]  INT           NOT NULL,
    [fulfilled]        INT           NOT NULL,
    [rxName]           VARCHAR (250) NOT NULL,
    [rxDosage]         VARCHAR (250) NOT NULL,
    [rxPillCount]      INT           NOT NULL,
    [rxNumberofDays]   INT           NOT NULL,
    [dateShipped]      VARCHAR (250) NOT NULL,
    [prescriptionDesc] VARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [dbo].[prescription]...';


GO
CREATE TABLE [dbo].[prescription] (
    [prescriptionId]     INT           IDENTITY (1, 1) NOT NULL,
    [prescriptionName]   VARCHAR (250) NOT NULL,
    [prescriptionDosage] VARCHAR (250) NOT NULL,
    [classFID]           INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([prescriptionId] ASC)
);


GO
PRINT N'Creating [dbo].[prescriptionClasses]...';


GO
CREATE TABLE [dbo].[prescriptionClasses] (
    [classId]   INT           IDENTITY (1, 1) NOT NULL,
    [className] VARCHAR (250) NOT NULL,
    PRIMARY KEY NONCLUSTERED ([classId] ASC)
);


GO
PRINT N'Creating [dbo].[FK_patientPrescription_ToTable]...';


GO
ALTER TABLE [dbo].[patientPrescription] WITH NOCHECK
    ADD CONSTRAINT [FK_patientPrescription_ToTable] FOREIGN KEY ([patientFID]) REFERENCES [dbo].[Patient] ([Id]);


GO
PRINT N'Creating [dbo].[FK_patientPrescription_ToTable_1]...';


GO
ALTER TABLE [dbo].[patientPrescription] WITH NOCHECK
    ADD CONSTRAINT [FK_patientPrescription_ToTable_1] FOREIGN KEY ([prescriptionFID]) REFERENCES [dbo].[prescription] ([prescriptionId]);


GO
PRINT N'Creating [dbo].[FK_prescription_ToTable]...';


GO
ALTER TABLE [dbo].[prescription] WITH NOCHECK
    ADD CONSTRAINT [FK_prescription_ToTable] FOREIGN KEY ([classFID]) REFERENCES [dbo].[prescriptionClasses] ([classId]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[patientPrescription] WITH CHECK CHECK CONSTRAINT [FK_patientPrescription_ToTable];

ALTER TABLE [dbo].[patientPrescription] WITH CHECK CHECK CONSTRAINT [FK_patientPrescription_ToTable_1];

ALTER TABLE [dbo].[prescription] WITH CHECK CHECK CONSTRAINT [FK_prescription_ToTable];


GO
PRINT N'Update complete.';


GO