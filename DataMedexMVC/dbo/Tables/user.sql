﻿CREATE TABLE [dbo].[user]
(
	[Id] INT NOT NULL IDENTITY, 
    [fName] NVARCHAR(50) NULL, 
    [lName] NVARCHAR(50) NULL, 
    [email] NVARCHAR(100) NOT NULL, 
    [password] NVARCHAR(100) NOT NULL, 
    [phoneNumber] NVARCHAR(50) NULL, 
    [streetAddress] NVARCHAR(100) NULL, 
    [city] NVARCHAR(100) NULL, 
    [state] NVARCHAR(100) NULL, 
    [zipcode] NVARCHAR(50) NULL, 
    [accountType] INT NOT NULL, 
    CONSTRAINT [PK_user] PRIMARY KEY ([Id])
)

