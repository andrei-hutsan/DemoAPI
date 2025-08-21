using FluentMigrator;

namespace Migrations.Migrations;
[Migration(2025081400)]
public class CreatePersonTable : Migration
{
    public override void Up()
    {
        Create.Table("Person")
            .WithColumn("Id").AsGuid().PrimaryKey()
                .WithDefault(SystemMethods.NewSequentialId)
            .WithColumn("Firstname").AsString(100).NotNullable()
            .WithColumn("Lastname").AsString(100).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("Person");
    }
}
