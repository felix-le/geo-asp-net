using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Todos.Data.Migrations
{
  public partial class CreateFirstTables : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "TodoListModels",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_TodoListModels", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Todos",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
            DaysTime = table.Column<int>(type: "int", nullable: false),
            TodoName = table.Column<string>(type: "nvarchar(max)", nullable: false),
            TodoListModelId = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Todos", x => x.Id);
            table.ForeignKey(
                      name: "FK_Todos_TodoListModels_TodoListModelId",
                      column: x => x.TodoListModelId,
                      principalTable: "TodoListModels",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Todos_TodoListModelId",
          table: "Todos",
          column: "TodoListModelId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Todos");

      migrationBuilder.DropTable(
          name: "TodoListModels");
    }
  }
}
