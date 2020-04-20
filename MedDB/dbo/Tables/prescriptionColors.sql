CREATE TABLE [dbo].[prescriptionColors]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [patientFID] INT NOT NULL, 
    [prescriptionFID] INT NOT NULL, 
    [color] VARCHAR(20) NOT NULL,
	CONSTRAINT [FK_prescriptionColors_pId] FOREIGN KEY ([patientFID]) REFERENCES [dbo].[user] ([Id]),
    CONSTRAINT [FK_prescriptionColors_rxId] FOREIGN KEY ([prescriptionFID]) REFERENCES [dbo].[patientPrescriptions] ([Id])
)
