using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayerBase_3.Data.Migrations
{
    public partial class addedForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Players",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Players");
        }
    }
}
