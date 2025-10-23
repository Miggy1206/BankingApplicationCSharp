using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApplicationClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class User_Table_Bug_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Users",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Users",
                newName: "FullName");
        }
    }
}
