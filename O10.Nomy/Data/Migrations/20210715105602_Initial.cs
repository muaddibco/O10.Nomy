﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace O10.Nomy.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExpertiseAreas",
                columns: table => new
                {
                    ExpertiseAreaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertiseAreas", x => x.ExpertiseAreaId);
                });

            migrationBuilder.CreateTable(
                name: "NomyUsers",
                columns: table => new
                {
                    NomyUserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    O10Id = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomyUsers", x => x.NomyUserId);
                });

            migrationBuilder.CreateTable(
                name: "ExpertProfiles",
                columns: table => new
                {
                    ExpertProfileId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomyUserId = table.Column<long>(type: "bigint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertProfiles", x => x.ExpertProfileId);
                    table.ForeignKey(
                        name: "FK_ExpertProfiles_NomyUsers_NomyUserId",
                        column: x => x.NomyUserId,
                        principalTable: "NomyUsers",
                        principalColumn: "NomyUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceRecords",
                columns: table => new
                {
                    InvoiceRecordId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNomyUserId = table.Column<long>(type: "bigint", nullable: true),
                    Commitment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RangeProof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfProcessing = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRecords", x => x.InvoiceRecordId);
                    table.ForeignKey(
                        name: "FK_InvoiceRecords_NomyUsers_UserNomyUserId",
                        column: x => x.UserNomyUserId,
                        principalTable: "NomyUsers",
                        principalColumn: "NomyUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExpertiseSubAreas",
                columns: table => new
                {
                    ExpertiseSubAreaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpertiseAreaId = table.Column<long>(type: "bigint", nullable: false),
                    ExpertProfileId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpertiseSubAreas", x => x.ExpertiseSubAreaId);
                    table.ForeignKey(
                        name: "FK_ExpertiseSubAreas_ExpertiseAreas_ExpertiseAreaId",
                        column: x => x.ExpertiseAreaId,
                        principalTable: "ExpertiseAreas",
                        principalColumn: "ExpertiseAreaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpertiseSubAreas_ExpertProfiles_ExpertProfileId",
                        column: x => x.ExpertProfileId,
                        principalTable: "ExpertProfiles",
                        principalColumn: "ExpertProfileId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRecords",
                columns: table => new
                {
                    PaymentRecordId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNomyUserId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceRecordId = table.Column<long>(type: "bigint", nullable: true),
                    Commitment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RangeProof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfProcessing = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRecords", x => x.PaymentRecordId);
                    table.ForeignKey(
                        name: "FK_PaymentRecords_InvoiceRecords_InvoiceRecordId",
                        column: x => x.InvoiceRecordId,
                        principalTable: "InvoiceRecords",
                        principalColumn: "InvoiceRecordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentRecords_NomyUsers_UserNomyUserId",
                        column: x => x.UserNomyUserId,
                        principalTable: "NomyUsers",
                        principalColumn: "NomyUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecretInvoiceRecords",
                columns: table => new
                {
                    SecretInvoiceRecordId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNomyUserId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceRecordId = table.Column<long>(type: "bigint", nullable: true),
                    BlindingFactor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecretInvoiceRecords", x => x.SecretInvoiceRecordId);
                    table.ForeignKey(
                        name: "FK_SecretInvoiceRecords_InvoiceRecords_InvoiceRecordId",
                        column: x => x.InvoiceRecordId,
                        principalTable: "InvoiceRecords",
                        principalColumn: "InvoiceRecordId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecretInvoiceRecords_NomyUsers_UserNomyUserId",
                        column: x => x.UserNomyUserId,
                        principalTable: "NomyUsers",
                        principalColumn: "NomyUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecretPaymentRecords",
                columns: table => new
                {
                    SecretPaymentRecordId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserNomyUserId = table.Column<long>(type: "bigint", nullable: true),
                    PaymentRecordId = table.Column<long>(type: "bigint", nullable: true),
                    BlindingFactor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecretPaymentRecords", x => x.SecretPaymentRecordId);
                    table.ForeignKey(
                        name: "FK_SecretPaymentRecords_NomyUsers_UserNomyUserId",
                        column: x => x.UserNomyUserId,
                        principalTable: "NomyUsers",
                        principalColumn: "NomyUserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecretPaymentRecords_PaymentRecords_PaymentRecordId",
                        column: x => x.PaymentRecordId,
                        principalTable: "PaymentRecords",
                        principalColumn: "PaymentRecordId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpertiseSubAreas_ExpertiseAreaId",
                table: "ExpertiseSubAreas",
                column: "ExpertiseAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertiseSubAreas_ExpertProfileId",
                table: "ExpertiseSubAreas",
                column: "ExpertProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpertProfiles_NomyUserId",
                table: "ExpertProfiles",
                column: "NomyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRecords_UserNomyUserId",
                table: "InvoiceRecords",
                column: "UserNomyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecords_InvoiceRecordId",
                table: "PaymentRecords",
                column: "InvoiceRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRecords_UserNomyUserId",
                table: "PaymentRecords",
                column: "UserNomyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SecretInvoiceRecords_InvoiceRecordId",
                table: "SecretInvoiceRecords",
                column: "InvoiceRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_SecretInvoiceRecords_UserNomyUserId",
                table: "SecretInvoiceRecords",
                column: "UserNomyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SecretPaymentRecords_PaymentRecordId",
                table: "SecretPaymentRecords",
                column: "PaymentRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_SecretPaymentRecords_UserNomyUserId",
                table: "SecretPaymentRecords",
                column: "UserNomyUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpertiseSubAreas");

            migrationBuilder.DropTable(
                name: "SecretInvoiceRecords");

            migrationBuilder.DropTable(
                name: "SecretPaymentRecords");

            migrationBuilder.DropTable(
                name: "ExpertiseAreas");

            migrationBuilder.DropTable(
                name: "ExpertProfiles");

            migrationBuilder.DropTable(
                name: "PaymentRecords");

            migrationBuilder.DropTable(
                name: "InvoiceRecords");

            migrationBuilder.DropTable(
                name: "NomyUsers");
        }
    }
}
