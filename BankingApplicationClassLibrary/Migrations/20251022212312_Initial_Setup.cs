using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingApplicationClassLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Setup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreditLimit = table.Column<double>(type: "float", nullable: false),
                    CreditInterestRate = table.Column<double>(type: "float", nullable: false),
                    WithdrawalFee = table.Column<double>(type: "float", nullable: false),
                    AccountNumber16 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_CreditCards_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentAccounts",
                columns: table => new
                {
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OverdraftLimit = table.Column<double>(type: "float", nullable: false),
                    OverdraftInterestRate = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentAccounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_CurrentAccounts_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mortgages",
                columns: table => new
                {
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalMortgageAmount = table.Column<double>(type: "float", nullable: false),
                    MortgageInterestRate = table.Column<double>(type: "float", nullable: false),
                    RepaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MortgageType = table.Column<int>(type: "int", nullable: false),
                    FurtherAdvanceCharge = table.Column<double>(type: "float", nullable: false),
                    FixedOverpaymentLimit = table.Column<double>(type: "float", nullable: false),
                    AccountNumber16 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mortgages", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_Mortgages_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SavingsAccounts",
                columns: table => new
                {
                    AccountID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    InterestRate = table.Column<double>(type: "float", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    MaturityDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingsAccounts", x => x.AccountID);
                    table.ForeignKey(
                        name: "FK_SavingsAccounts_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "AccountID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCards");

            migrationBuilder.DropTable(
                name: "CurrentAccounts");

            migrationBuilder.DropTable(
                name: "Mortgages");

            migrationBuilder.DropTable(
                name: "SavingsAccounts");
        }
    }
}
