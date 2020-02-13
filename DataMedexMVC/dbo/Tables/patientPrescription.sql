CREATE TABLE [dbo].[patientPrescription] (
    [Id]               INT           NOT NULL IDENTITY,
    [patientFID]       INT           NULL,
    [prescriptionFID]  INT           NULL,
    [fulfilled]        INT           NULL,
    [rxName]           VARCHAR (250) NULL,
    [rxDosage]         VARCHAR (250) NULL,
    [rxPillCount]      INT           NULL,
    [rxNumberofDays]   INT           NULL,
    [dateShipped]      VARCHAR (250) NULL,
    [prescriptionDesc] VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_patientPrescription_ToTable] FOREIGN KEY ([patientFID]) REFERENCES [dbo].[Patient] ([Id]),
    CONSTRAINT [FK_patientPrescription_ToTable_1] FOREIGN KEY ([prescriptionFID]) REFERENCES [dbo].[prescription] ([prescriptionId])
);

