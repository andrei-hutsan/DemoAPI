CREATE PROCEDURE [dbo].[spPerson_Insert]
    @Id UNIQUEIDENTIFIER,
    @Firstname NVARCHAR(50),
    @Lastname NVARCHAR(50)
AS
BEGIN
    INSERT INTO dbo.[Person] (Id, Firstname, Lastname)
    VALUES (@Id, @Firstname, @Lastname);
END;
