using FluentMigrator;

namespace Migrations.Migrations;
[Migration(2025082200)]
public class Create_Department : Migration
{
    public override void Down()
    {
        Create.Table("Department")
            .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithDefault(SystemMethods.NewSequentialId)
            .WithColumn("Name").AsString(100).NotNullable();

        Create.Index("UX_Department_Name")
            .OnTable("Department").OnColumn("Name").Ascending()
            .WithOptions().Unique();
    }

    public override void Up()
    {
        Delete.Index("UX_Department_Name").OnTable("Department");
        Delete.Table("Department");
    }
}
