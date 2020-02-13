CREATE TABLE [dbo].[Packer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] VARCHAR(250) NULL, 
    [username] VARCHAR(250) NULL, 
    [password] VARCHAR(250) NULL, 
    [admin] INT NULL
)
