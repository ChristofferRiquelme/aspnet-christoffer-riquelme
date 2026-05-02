using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymPortal.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMembershipTypeAndPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Memberships",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Memberships",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Memberships");
        }
    }
}
