using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proyecto_url.Migrations
{
    public partial class MigracionNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "UserName" },
                values: new object[] { 2, "mabelucita@gmail.com", "Mabel Enrique", "lamismadesiempre", "mabeluci" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
