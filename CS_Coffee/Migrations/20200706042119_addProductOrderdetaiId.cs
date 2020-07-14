using Microsoft.EntityFrameworkCore.Migrations;

namespace CS_Coffee.Migrations
{
    public partial class addProductOrderdetaiId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductOrderDetailId",
                table: "ProductOrderDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductOrderDetailId",
                table: "ProductOrderDetails");
        }
    }
}
