using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessApp.API.Migrations
{
    public partial class ExtendedBusinessEntityClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublishable",
                table: "Businesses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Businesses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Landmark",
                table: "Businesses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublishable",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "Landmark",
                table: "Businesses");
        }
    }
}
