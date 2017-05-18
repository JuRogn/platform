using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class wjw1modulelocalization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Localization_Culture",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localization_Culture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Localization_Resource",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    CultureId = table.Column<string>(nullable: false),
                    Key = table.Column<string>(maxLength: 450, nullable: false),
                    Value = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localization_Resource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Localization_Resource_Localization_Culture_CultureId",
                        column: x => x.CultureId,
                        principalTable: "Localization_Culture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Localization_Resource_CultureId",
                table: "Localization_Resource",
                column: "CultureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localization_Resource");

            migrationBuilder.DropTable(
                name: "Localization_Culture");
        }
    }
}
