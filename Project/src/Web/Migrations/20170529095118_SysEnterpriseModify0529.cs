using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class SysEnterpriseModify0529 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SysEnterprises",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedDate",
                table: "SysEnterprises",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedTime",
                table: "SysEnterprises",
                maxLength: 16,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "SysEnterprises",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EnterpriseId",
                table: "SysEnterprises",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "SysEnterprises",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "SysEnterprises",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedDate",
                table: "SysEnterprises",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysEnterprises_CreatedBy",
                table: "SysEnterprises",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_SysEnterprises_UpdatedBy",
                table: "SysEnterprises",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_SysEnterprises_AspNetUsers_CreatedBy",
                table: "SysEnterprises",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SysEnterprises_AspNetUsers_UpdatedBy",
                table: "SysEnterprises",
                column: "UpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SysEnterprises_AspNetUsers_CreatedBy",
                table: "SysEnterprises");

            migrationBuilder.DropForeignKey(
                name: "FK_SysEnterprises_AspNetUsers_UpdatedBy",
                table: "SysEnterprises");

            migrationBuilder.DropIndex(
                name: "IX_SysEnterprises_CreatedBy",
                table: "SysEnterprises");

            migrationBuilder.DropIndex(
                name: "IX_SysEnterprises_UpdatedBy",
                table: "SysEnterprises");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SysEnterprises");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "SysEnterprises");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "SysEnterprises");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "SysEnterprises");

            migrationBuilder.DropColumn(
                name: "EnterpriseId",
                table: "SysEnterprises");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "SysEnterprises");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "SysEnterprises");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "SysEnterprises");
        }
    }
}
