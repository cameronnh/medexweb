CREATE TABLE [dbo].[doctorPatients]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [patientFID] INT NULL, 
    [doctorFID] INT NULL, 
    CONSTRAINT [FK_doctorPatients_ToTable] FOREIGN KEY ([patientFID]) REFERENCES [user]([Id]), 
    CONSTRAINT [FK_doctorPatients_ToTable_1] FOREIGN KEY ([doctorFID]) REFERENCES [user]([Id])
)
