using FluentMigrator;

namespace Migrations.Migrations;
[Migration(2025082201)]
public class Add_DepartmentId_To_Person : Migration
{
    public override void Down()
    {
        Alter.Table("Person")
            .AddColumn("DepartmentId").AsGuid().Nullable();

        Create.ForeignKey("FK_Person_Department")
            .FromTable("Person").ForeignColumn("DepartmentId")
            .ToTable("Department").PrimaryColumn("Id");

        Create.Index("IX_Person_DepartmentId")
            .OnTable("Person").OnColumn("DepartmentId").Ascending();
    }

    public override void Up()
    {
        Delete.Index("IX_Person_DepartmentId").OnTable("Person");
        Delete.ForeignKey("FK_Person_Department").OnTable("Person");
        Delete.Column("DepartmentId").FromTable("Person");
    }
}
