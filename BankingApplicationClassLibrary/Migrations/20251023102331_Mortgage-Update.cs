using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApplicationClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class MortgageUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MortgageTermInYears",
                table: "Mortgages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MortgageTermInYears",
                table: "Mortgages");
        }
    }
}
