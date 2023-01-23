CREATE TABLE [dbo].[People]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OIB] VARCHAR(50) NOT NULL, 
    [Name] VARCHAR(50) NULL, 
    [Surname] VARCHAR(50) NULL, 
    [Place] VARCHAR(50) NULL, 
    [Address] VARCHAR(50) NULL, 
    [Phone] VARCHAR(50) NULL, 
    [Mail] VARCHAR(50) NULL
)

GO

CREATE UNIQUE INDEX [IX_People_OIB] ON [dbo].[People] ([OIB])
