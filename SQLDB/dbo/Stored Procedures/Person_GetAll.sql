CREATE PROCEDURE [dbo].[Person_GetAll]
	
AS
	SELECT [Id], [OIB], [Name], [Surname], [Place], [Address], [Phone], [Mail]
	FROM People

