CREATE TABLE [dbo].[messages]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [userId] INT NOT NULL, 
    [text] VARCHAR(MAX) NOT NULL, 
    [user] VARCHAR(50) NOT NULL, 
    [time] VARCHAR(50) NOT NULL, 
    [date] VARCHAR(50) NOT NULL, 
    [chatID] INT NOT NULL,
    CONSTRAINT [FK_Messages_ChatId] FOREIGN KEY ([chatID]) REFERENCES [dbo].[chats] ([Id]),
    CONSTRAINT [FK_Messages_UserId] FOREIGN KEY ([userId]) REFERENCES [dbo].[user] ([Id])
)
