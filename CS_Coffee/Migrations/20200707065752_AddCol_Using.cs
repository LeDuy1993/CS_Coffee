using Microsoft.EntityFrameworkCore.Migrations;

namespace CS_Coffee.Migrations
{
    public partial class AddCol_Using : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Using",
                table: "Orders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Using",
                table: "Orders");
        }
    }
}
