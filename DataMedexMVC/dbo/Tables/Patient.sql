CREATE TABLE [dbo].[Patient]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [fName] NVARCHAR(50) NOT NULL, 
    [lName] NVARCHAR(50) NOT NULL, 
    [email] NVARCHAR(100) NOT NULL, 
    [password] NVARCHAR(100) NOT NULL, 
    [phoneNumber] NVARCHAR(50) NOT NULL, 
    [streetAddress] NVARCHAR(100) NOT NULL, 
    [city] NVARCHAR(100) NOT NULL, 
    [state] NVARCHAR(100) NOT NULL, 
    [zipcode] NVARCHAR(50) NOT NULL
)
