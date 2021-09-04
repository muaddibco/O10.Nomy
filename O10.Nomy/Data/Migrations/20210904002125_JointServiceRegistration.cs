using Microsoft.EntityFrameworkCore.Migrations;

namespace O10.Nomy.Data.Migrations
{
    public partial class JointServiceRegistration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RegistrationJointServiceRegistrationId",
                table: "JointGroups",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JointServiceRegistrations",
                columns: table => new
                {
                    JointServiceRegistrationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationCommitment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    O10RegistrationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JointServiceRegistrations", x => x.JointServiceRegistrationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JointGroups_RegistrationJointServiceRegistrationId",
                table: "JointGroups",
                column: "RegistrationJointServiceRegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_JointServiceRegistrations_O10RegistrationId",
                table: "JointServiceRegistrations",
                column: "O10RegistrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_JointGroups_JointServiceRegistrations_RegistrationJointServiceRegistrationId",
                table: "JointGroups",
                column: "RegistrationJointServiceRegistrationId",
                principalTable: "JointServiceRegistrations",
                principalColumn: "JointServiceRegistrationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JointGroups_JointServiceRegistrations_RegistrationJointServiceRegistrationId",
                table: "JointGroups");

            migrationBuilder.DropTable(
                name: "JointServiceRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_JointGroups_RegistrationJointServiceRegistrationId",
                table: "JointGroups");

            migrationBuilder.DropColumn(
                name: "RegistrationJointServiceRegistrationId",
                table: "JointGroups");
        }
    }
}
