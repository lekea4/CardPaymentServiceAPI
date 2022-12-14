using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CardPaymentServiceAPI.DatabaseConnection.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardsDetails",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncryptedCardPan = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    EncryptedCVV = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CardExpiry = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ReoccuringPaymentEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccountLinkedToCard = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardsDetails", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "Fintechs",
                columns: table => new
                {
                    FintechID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FintechName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FintechEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreationReference = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fintechs", x => x.FintechID);
                });

            migrationBuilder.CreateTable(
                name: "ReoccuringPaymentFrquency",
                columns: table => new
                {
                    ReoccuringPaymentFrequencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReoccuringPaymentFrequencyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReoccuringPaymentFrequencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReoccuringPaymentFrquency", x => x.ReoccuringPaymentFrequencyId);
                });

            migrationBuilder.CreateTable(
                name: "ReoccuringPayment",
                columns: table => new
                {
                    ReoccuringPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReoccuringPaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    ReoccuringPaymentFrquencyIdReoccuringPaymentFrequencyId = table.Column<int>(type: "int", nullable: true),
                    ReoccuringPaymentFrquencyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentPaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    CurrentPaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentPaymentResponseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPayementResponseMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextPayemntDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardId = table.Column<int>(type: "int", nullable: true),
                    EncryptedCardPan = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FintechsFintechID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReoccuringPayment", x => x.ReoccuringPaymentId);
                    table.ForeignKey(
                        name: "FK_ReoccuringPayment_CardsDetails_CardId",
                        column: x => x.CardId,
                        principalTable: "CardsDetails",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReoccuringPayment_Fintechs_FintechsFintechID",
                        column: x => x.FintechsFintechID,
                        principalTable: "Fintechs",
                        principalColumn: "FintechID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReoccuringPayment_ReoccuringPaymentFrquency_ReoccuringPaymentFrquencyIdReoccuringPaymentFrequencyId",
                        column: x => x.ReoccuringPaymentFrquencyIdReoccuringPaymentFrequencyId,
                        principalTable: "ReoccuringPaymentFrquency",
                        principalColumn: "ReoccuringPaymentFrequencyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncryptedCardPan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentResponseCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentResponseMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaymentSuccessful = table.Column<bool>(type: "bit", nullable: false),
                    IsReocurringPayment = table.Column<bool>(type: "bit", nullable: false),
                    ReoccuringPaymentId = table.Column<int>(type: "int", nullable: true),
                    FintechID = table.Column<int>(type: "int", nullable: true),
                    FintechName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payment_Fintechs_FintechID",
                        column: x => x.FintechID,
                        principalTable: "Fintechs",
                        principalColumn: "FintechID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_ReoccuringPayment_ReoccuringPaymentId",
                        column: x => x.ReoccuringPaymentId,
                        principalTable: "ReoccuringPayment",
                        principalColumn: "ReoccuringPaymentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_FintechID",
                table: "Payment",
                column: "FintechID");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_ReoccuringPaymentId",
                table: "Payment",
                column: "ReoccuringPaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReoccuringPayment_CardId",
                table: "ReoccuringPayment",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_ReoccuringPayment_FintechsFintechID",
                table: "ReoccuringPayment",
                column: "FintechsFintechID");

            migrationBuilder.CreateIndex(
                name: "IX_ReoccuringPayment_ReoccuringPaymentFrquencyIdReoccuringPaymentFrequencyId",
                table: "ReoccuringPayment",
                column: "ReoccuringPaymentFrquencyIdReoccuringPaymentFrequencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "ReoccuringPayment");

            migrationBuilder.DropTable(
                name: "CardsDetails");

            migrationBuilder.DropTable(
                name: "Fintechs");

            migrationBuilder.DropTable(
                name: "ReoccuringPaymentFrquency");
        }
    }
}
