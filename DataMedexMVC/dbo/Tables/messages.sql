CREATE TABLE [dbo].[messages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ownerID] INT NULL, 
    [recieverID] INT NULL, 
    [message] VARCHAR(MAX) NOT NULL, 
    [ownerPic] IMAGE NOT NULL, 
    [recieverPic] IMAGE NOT NULL
)
