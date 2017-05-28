using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class modifySysModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysLanguagePacks");

            migrationBuilder.RenameColumn(
                name: "SysDefault",
                table: "AspNetRoles",
                newName: "System");

            migrationBuilder.AlterColumn<string>(
                name: "EnterpriseId",
                table: "AspNetRoles",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 450,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "System",
                table: "AspNetRoles",
                newName: "SysDefault");

            migrationBuilder.AlterColumn<string>(
                name: "EnterpriseId",
                table: "AspNetRoles",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SysLanguagePacks",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Language = table.Column<string>(maxLength: 200, nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Value = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysLanguagePacks", x => x.Id);
                });
        }
    }
}
