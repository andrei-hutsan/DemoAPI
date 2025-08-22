using FluentMigrator;

namespace Migrations.Migrations;
[Migration(2025082202)]
public class Seed_Departments : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
        IF NOT EXISTS (SELECT 1 FROM dbo.Department)
        BEGIN
            INSERT dbo.Department (Id, Name)
            VALUES (NEWSEQUENTIALID(), N'Engineering'),
                   (NEWSEQUENTIALID(), N'HR'),
                   (NEWSEQUENTIALID(), N'Finance');
        END
        ");
    }

    public override void Down()
    {
    }
}
