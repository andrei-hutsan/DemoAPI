CREATE PROCEDURE [dbo].[spPerson_Update]
    @Id UNIQUEIDENTIFIER,
    @Firstname NVARCHAR(100),
    @Lastname NVARCHAR(100)
AS
BEGIN
    UPDATE dbo.[Person]
    SET 
        Firstname = @Firstname,
        Lastname = @Lastname
    WHERE Id = @Id;
END;
