using FluentMigrator;

namespace Migrations.Migrations;
[Migration(2025082202)]
public class Seed_Departments : Migration
{
    public override void Up()
    {
        Insert.IntoTable("Department")
            .Row(new { Name = "Engineering" })
            .Row(new { Name = "HR" })
            .Row(new { Name = "Finance" });
    }

    public override void Down()
    {
        Delete.FromTable("Department")
            .Row(new { Name = "Engineering" })
            .Row(new { Name = "HR" })
            .Row(new { Name = "Finance" });
    }
}
