using Microsoft.EntityFrameworkCore.Migrations;

namespace O10.Nomy.Data.Migrations
{
    public partial class DemoValidations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemoValidation",
                columns: table => new
                {
                    DemoValidationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaticData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DynamicData = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemoValidation", x => x.DemoValidationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DemoValidation");
        }
    }
}
