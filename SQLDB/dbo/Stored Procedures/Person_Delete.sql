CREATE PROCEDURE [dbo].[Person_Delete]
	@Id int
AS
	DELETE FROM People WHERE [Id] = @Id
