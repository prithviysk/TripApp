using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TripApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRouteInfoToTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Trips_TripId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_TripId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "Trips");

            migrationBuilder.AddColumn<string>(
                name: "Distance",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Polyline",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Polyline",
                table: "Trips");

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TripId",
                table: "Trips",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Trips_TripId",
                table: "Trips",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");
        }
    }
}
