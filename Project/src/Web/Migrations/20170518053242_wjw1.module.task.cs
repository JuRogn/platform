using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class wjw1moduletask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Task_TaskCenter",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 450, nullable: false),
                    ActualEndTime = table.Column<string>(maxLength: 64, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    CreatedDate = table.Column<string>(maxLength: 20, nullable: false),
                    CreatedTime = table.Column<string>(maxLength: 16, nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Duration = table.Column<decimal>(nullable: false),
                    EnterpriseId = table.Column<string>(maxLength: 450, nullable: true),
                    Files = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(maxLength: 200, nullable: true),
                    ScheduleEndTime = table.Column<string>(maxLength: 64, nullable: true),
                    TaskExecutorId = table.Column<string>(nullable: false),
                    TaskType = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    UpdatedDate = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Task_TaskCenter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_TaskCenter_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Task_TaskCenter_AspNetUsers_TaskExecutorId",
                        column: x => x.TaskExecutorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Task_TaskCenter_AspNetUsers_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Task_TaskCenter_CreatedBy",
                table: "Task_TaskCenter",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TaskCenter_TaskExecutorId",
                table: "Task_TaskCenter",
                column: "TaskExecutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Task_TaskCenter_UpdatedBy",
                table: "Task_TaskCenter",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Task_TaskCenter");
        }
    }
}
