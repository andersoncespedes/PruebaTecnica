using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Journies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<double>(type: "float(15)", precision: 15, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightCarrier = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Origin = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Price = table.Column<double>(type: "float(15)", precision: 15, scale: 2, nullable: false),
                    IdTransportFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Transports_IdTransportFK",
                        column: x => x.IdTransportFK,
                        principalTable: "Transports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JourneyFlight",
                columns: table => new
                {
                    IdJourneyFK = table.Column<int>(type: "int", nullable: false),
                    IdFlightFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JourneyFlight", x => new { x.IdJourneyFK, x.IdFlightFK });
                    table.ForeignKey(
                        name: "FK_JourneyFlight_Flights_IdFlightFK",
                        column: x => x.IdFlightFK,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JourneyFlight_Journies_IdJourneyFK",
                        column: x => x.IdJourneyFK,
                        principalTable: "Journies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_IdTransportFK",
                table: "Flights",
                column: "IdTransportFK");

            migrationBuilder.CreateIndex(
                name: "IX_JourneyFlight_IdFlightFK",
                table: "JourneyFlight",
                column: "IdFlightFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JourneyFlight");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Journies");

            migrationBuilder.DropTable(
                name: "Transports");
        }
    }
}
