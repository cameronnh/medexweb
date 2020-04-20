CREATE TABLE [dbo].[prescriptionColors] (
    [Id]              INT          IDENTITY (1, 1) NOT NULL,
    [patientFID]      INT          NOT NULL,
    [prescriptionFID] INT          NOT NULL,
    [color]           VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_prescriptionColors_pId] FOREIGN KEY ([patientFID]) REFERENCES [dbo].[user] ([Id]),
    CONSTRAINT [FK_prescriptionColors_rxId] FOREIGN KEY ([prescriptionFID]) REFERENCES [dbo].[patientPrescriptions] ([Id])
);