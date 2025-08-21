using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Migrations.Migrations;
[Migration(20240821002)]
public class Upsert_Procedures : Migration
{
    public override void Up()
    {
        Execute.Sql(@"
        CREATE OR ALTER PROCEDURE dbo.spPerson_Insert
          @Firstname NVARCHAR(100),
          @Lastname  NVARCHAR(100),
          @Email NVARCHAR(100)
        AS
        BEGIN
          INSERT INTO dbo.Person (Firstname, Lastname, Email)
          VALUES (@Firstname, @Lastname, @Email);
        END
        ");
        // Add spPerson_Get, spPerson_GetAll, spPerson_Update, spPerson_Delete similarly
    }
    public override void Down()
    {
        Execute.Sql(@"IF OBJECT_ID('dbo.spPerson_Insert','P') IS NOT NULL DROP PROCEDURE dbo.spPerson_Insert;");
        // drop others similarly
    }
}
