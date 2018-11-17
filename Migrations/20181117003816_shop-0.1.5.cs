using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Migrations
{
    public partial class shop015 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
