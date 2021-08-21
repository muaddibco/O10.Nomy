using Microsoft.EntityFrameworkCore.Migrations;

namespace O10.Nomy.Data.Migrations
{
    public partial class JointGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JointGroups",
                columns: table => new
                {
                    JointGroupId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    O10RegistrationId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JointGroups", x => x.JointGroupId);
                });

            migrationBuilder.CreateTable(
                name: "JointGroupMembers",
                columns: table => new
                {
                    JointGroupMemberId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupJointGroupId = table.Column<long>(type: "bigint", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRegistered = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JointGroupMembers", x => x.JointGroupMemberId);
                    table.ForeignKey(
                        name: "FK_JointGroupMembers_JointGroups_GroupJointGroupId",
                        column: x => x.GroupJointGroupId,
                        principalTable: "JointGroups",
                        principalColumn: "JointGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JointGroupMembers_GroupJointGroupId",
                table: "JointGroupMembers",
                column: "GroupJointGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_JointGroups_O10RegistrationId",
                table: "JointGroups",
                column: "O10RegistrationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JointGroupMembers");

            migrationBuilder.DropTable(
                name: "JointGroups");
        }
    }
}
