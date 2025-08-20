using FluentMigrator;

namespace Migrations.Migrations;
[Migration(2025081401)]
public class AddEmailToPerson : Migration
{
    public override void Up()
    {
        Alter.Table("Person")
            .AddColumn("Email").AsString(100).Nullable();
    }

    public override void Down()
    {
        Delete.Column("Email").FromTable("Person");
    }
}

