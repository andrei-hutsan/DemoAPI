CREATE PROCEDURE [dbo].[spPerson_Get]
	@Id uniqueidentifier
AS
BEGIN
	SELECT * 
	FROM dbo.[Person] 
	WHERE Id=@Id;
END
