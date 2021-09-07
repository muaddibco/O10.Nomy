using Microsoft.EntityFrameworkCore.Migrations;

namespace O10.Nomy.Data.Migrations
{
    public partial class DemoCompromization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AdversaryFrom",
                table: "NomyUsers",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdversaryFrom",
                table: "NomyUsers");
        }
    }
}
