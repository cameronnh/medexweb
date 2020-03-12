CREATE TABLE [dbo].[deliveries] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [patientFID]       INT           NOT NULL,
    [patientRXFID]  INT           NOT NULL,
    [doctorFID] INT NOT NULL, 
    [shippedDate] VARCHAR(50) NULL, 
    [arrivalDate] VARCHAR(50) NULL,
    CONSTRAINT [FK_deliveries_ToTable] FOREIGN KEY ([patientFID]) REFERENCES [dbo].[user] ([Id]),
    CONSTRAINT [FK_deliveries_ToTable_1] FOREIGN KEY ([patientRXFID]) REFERENCES [dbo].[patientPrescriptions] ([Id]), 
    CONSTRAINT [FK_deliveries_ToTable_2] FOREIGN KEY ([doctorFID]) REFERENCES [dbo].[user] ([Id])
);