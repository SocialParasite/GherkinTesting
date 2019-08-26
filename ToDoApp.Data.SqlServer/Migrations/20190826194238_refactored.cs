using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp.Data.SqlServer.Migrations
{
    public partial class refactored : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_ToDoLists_ProjectId",
                table: "ToDoItem");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "ToDoItem",
                newName: "ToDoListId");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItem_ProjectId",
                table: "ToDoItem",
                newName: "IX_ToDoItem_ToDoListId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ToDoLists",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ToDoItem",
                maxLength: 64,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_ToDoLists_ToDoListId",
                table: "ToDoItem",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_ToDoLists_ToDoListId",
                table: "ToDoItem");

            migrationBuilder.RenameColumn(
                name: "ToDoListId",
                table: "ToDoItem",
                newName: "ProjectId");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItem_ToDoListId",
                table: "ToDoItem",
                newName: "IX_ToDoItem_ProjectId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ToDoLists",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ToDoItem",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 64);

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_ToDoLists_ProjectId",
                table: "ToDoItem",
                column: "ProjectId",
                principalTable: "ToDoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
