CREATE TABLE [dbo].[appointments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [date] VARCHAR(50) NULL, 
    [time] VARCHAR(50) NULL, 
    [description] VARCHAR(50) NULL, 
    [location] VARCHAR(50) NULL
)
