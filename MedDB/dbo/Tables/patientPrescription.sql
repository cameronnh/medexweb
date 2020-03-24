CREATE TABLE [dbo].[patientPrescriptions] (
    [Id]               INT  IDENTITY (1, 1)   PRIMARY KEY ,
    [patientFID]       INT           NOT NULL,
    [prescriptionFID]  INT           NOT NULL,
    [name]           VARCHAR (100) NOT NULL,
    [dosage]         VARCHAR (50) NOT NULL,
    [pillCount]      INT           NOT NULL,
    [numberofRefills]   INT           NOT NULL,
    [useBefore]      VARCHAR (50) NOT NULL,
    [description] VARCHAR (250) NOT NULL,
    [datePrescribed] VARCHAR(50) NOT NULL, 
    [doctorFID] INT NOT NULL,
    CONSTRAINT [FK_patientPrescription_ToTable] FOREIGN KEY ([patientFID]) REFERENCES [dbo].[user] ([Id]),
    CONSTRAINT [FK_patientPrescription_ToTable_1] FOREIGN KEY ([prescriptionFID]) REFERENCES [dbo].[prescription] ([prescriptionId]), 
    CONSTRAINT [FK_patientPrescription_ToTable_2] FOREIGN KEY ([doctorFID]) REFERENCES [dbo].[user] ([Id])
);
