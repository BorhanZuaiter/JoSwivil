using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RouteId",
                table: "Driver",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Driver_RouteId",
                table: "Driver",
                column: "RouteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Driver_Route_RouteId",
                table: "Driver",
                column: "RouteId",
                principalTable: "Route",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Driver_Route_RouteId",
                table: "Driver");

            migrationBuilder.DropIndex(
                name: "IX_Driver_RouteId",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Driver");
        }
    }
}
