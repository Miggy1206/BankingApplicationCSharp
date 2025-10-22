using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApplicationClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sortcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    OpenedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OpenedByStaffID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
