using Microsoft.EntityFrameworkCore.Migrations;

namespace O10.Nomy.Data.Migrations
{
    public partial class JointGroupsEx : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "O10GroupId",
                table: "JointGroups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "JointGroupMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "O10GroupId",
                table: "JointGroups");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "JointGroupMembers");
        }
    }
}
