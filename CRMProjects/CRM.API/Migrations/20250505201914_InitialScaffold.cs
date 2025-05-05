using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CRM.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialScaffold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "Password", "Phone", "UserName" },
                values: new object[,]
                {
                    { 1, "admin@example.com", "Administrator", "5ab598c2af4a0d05a5cd66885fb02605151f4d18b90e557d526e36583eb5944a", "", "Admin" },
                    { 2, "manager@example.com", "Manager", "5ab598c2af4a0d05a5cd66885fb02605151f4d18b90e557d526e36583eb5944a", "", "Manager" },
                    { 3, "reader@example.com", "Reader", "5ab598c2af4a0d05a5cd66885fb02605151f4d18b90e557d526e36583eb5944a", "", "Reader" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
