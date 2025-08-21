CREATE PROCEDURE [dbo].[spPerson_Insert]
    @Firstname NVARCHAR(50),
    @Lastname NVARCHAR(50),
    @Email  NVARCHAR(256) = NULL
AS
BEGIN
    INSERT INTO dbo.Person (Firstname, Lastname, Email)
    VALUES (@Firstname, @Lastname, @Email);
END;
