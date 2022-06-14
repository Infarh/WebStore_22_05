using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebStore.DAL.Migrations
{
    public partial class Employees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_LastName_FirstName_Patronymic_Age",
                table: "Employees",
                columns: new[] { "LastName", "FirstName", "Patronymic", "Age" },
                unique: true,
                filter: "[Patronymic] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
