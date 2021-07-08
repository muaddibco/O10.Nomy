using Microsoft.EntityFrameworkCore.Migrations;

namespace O10.Nomy.Data.Migrations
{
    public partial class AccountsProfiles : Migration
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpertiseSubAreas");

            migrationBuilder.DropTable(
                name: "ExpertiseAreas");

            migrationBuilder.DropTable(
                name: "ExpertProfiles");

            migrationBuilder.DropTable(
                name: "NomyUsers");
        }
    }
}
