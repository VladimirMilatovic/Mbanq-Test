CREATE PROCEDURE [dbo].[Person_Update]
	
	@Id int,
	@OIB varchar(50),
	@Name varchar(50),
	@Surname varchar(50),
	@Place varchar(50),
	@Address varchar(50),
	@Phone varchar(50),
	@Mail varchar(50)
AS
	UPDATE People
	SET [OIB] = @OIB, [Name] = @Name, [Surname] = @Surname, [Place] = @Place, [Address] = @Address, [Phone] = @Phone, [Mail] = @Mail
	WHERE [Id] = @Id
