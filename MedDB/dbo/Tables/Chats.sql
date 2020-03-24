CREATE TABLE [dbo].[chats]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[topic] varchar(50) NOT NULL, 
    [patientID] INT NULL, 
    [doctorID] INT NULL,
	CONSTRAINT [FK_Chats_PatientID] FOREIGN KEY ([patientID]) REFERENCES [dbo].[user] ([Id]),
	CONSTRAINT [FK_Chats_DoctorID] FOREIGN KEY ([doctorID]) REFERENCES [dbo].[user] ([Id]),

)
