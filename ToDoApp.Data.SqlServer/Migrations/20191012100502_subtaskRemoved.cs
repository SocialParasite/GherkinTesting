using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp.Data.SqlServer.Migrations
{
    public partial class subtaskRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subtask");

            migrationBuilder.AddColumn<Guid>(
                name: "TaskItemId",
                table: "ToDoItems",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    TaskItemId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_ToDoItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "ToDoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItems_TaskItemId",
                table: "ToDoItems",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_TaskItemId",
                table: "Category",
                column: "TaskItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoItems_TaskItemId",
                table: "ToDoItems",
                column: "TaskItemId",
                principalTable: "ToDoItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoItems_TaskItemId",
                table: "ToDoItems");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItems_TaskItemId",
                table: "ToDoItems");

            migrationBuilder.DropColumn(
                name: "TaskItemId",
                table: "ToDoItems");

            migrationBuilder.CreateTable(
                name: "Subtask",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 64, nullable: false),
                    TaskItemId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subtask_ToDoItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "ToDoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subtask_TaskItemId",
                table: "Subtask",
                column: "TaskItemId");
        }
    }
}
