using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp.Data.SqlServer.Migrations
{
    public partial class TaskRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskItem");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentTaskId",
                table: "ToDoItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItem_ParentTaskId",
                table: "ToDoItem",
                column: "ParentTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_ToDoItem_ParentTaskId",
                table: "ToDoItem",
                column: "ParentTaskId",
                principalTable: "ToDoItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_ToDoItem_ParentTaskId",
                table: "ToDoItem");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItem_ParentTaskId",
                table: "ToDoItem");

            migrationBuilder.DropColumn(
                name: "ParentTaskId",
                table: "ToDoItem");

            migrationBuilder.CreateTable(
                name: "TaskItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ToDoItemId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItem_ToDoItem_ToDoItemId",
                        column: x => x.ToDoItemId,
                        principalTable: "ToDoItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_ToDoItemId",
                table: "TaskItem",
                column: "ToDoItemId");
        }
    }
}
