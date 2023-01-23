CREATE PROCEDURE [dbo].[Person_Insert]
	@Id int,
	@OIB varchar(50),
	@Name varchar(50),
	@Surname varchar(50),
	@Place varchar(50),
	@Address varchar(50),
	@Phone varchar(50),
	@Mail varchar(50)
AS
	INSERT INTO People ([OIB], [Name], [Surname], [Place], [Address], [Phone], [Mail])
	VALUES (@OIB, @Name, @Surname, @Place, @Address, @Phone, @Mail)
	
	SELECT [Id], [OIB], [Name], [Surname], [Place], [Address], [Phone], [Mail]
	FROM People
	WHERE Id = SCOPE_IDENTITY()
