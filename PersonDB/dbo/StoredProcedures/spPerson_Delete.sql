CREATE PROCEDURE [dbo].[spPerson_Delete]
	@Id uniqueidentifier
AS
BEGIN
	DELETE
	FROM dbo.[Person] 
	WHERE Id=@Id;
END

