CREATE TABLE [dbo].[patientPrescription] (
    [Id]               INT           NOT NULL IDENTITY,
    [patientFID]       INT           NOT NULL,
    [prescriptionFID]  INT           NOT NULL,
    [fulfilled]        INT           NOT NULL,
    [rxName]           VARCHAR (250) NOT NULL,
    [rxDosage]         VARCHAR (250) NOT NULL,
    [rxPillCount]      INT           NOT NULL,
    [rxNumberofDays]   INT           NOT NULL,
    [dateShipped]      VARCHAR (250) NOT NULL,
    [prescriptionDesc] VARCHAR (250) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_patientPrescription_ToTable] FOREIGN KEY ([patientFID]) REFERENCES [dbo].[user] ([Id]),
    CONSTRAINT [FK_patientPrescription_ToTable_1] FOREIGN KEY ([prescriptionFID]) REFERENCES [dbo].[prescription] ([prescriptionId])
);

