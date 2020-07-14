using Microsoft.EntityFrameworkCore.Migrations;

namespace CS_Coffee.Migrations
{
    public partial class AddCol_Paid_OrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "OrderDetails",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "OrderDetails");
        }
    }
}
