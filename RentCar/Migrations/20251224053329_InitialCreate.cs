using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCar.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MsCars",
                columns: table => new
                {
                    CarId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumberOfCarSeats = table.Column<int>(type: "int", nullable: false),
                    Transmission = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "MsCustomers",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DriverLicenseNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCustomers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "MsEmployees",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsEmployees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "MsCarImages",
                columns: table => new
                {
                    ImageCarId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MsCarImages", x => x.ImageCarId);
                    table.ForeignKey(
                        name: "FK_MsCarImages_MsCars_CarId",
                        column: x => x.CarId,
                        principalTable: "MsCars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrRentals",
                columns: table => new
                {
                    RentalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RentalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CarId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrRentals", x => x.RentalId);
                    table.ForeignKey(
                        name: "FK_TrRentals_MsCars_CarId",
                        column: x => x.CarId,
                        principalTable: "MsCars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrRentals_MsCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "MsCustomers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrMaintenances",
                columns: table => new
                {
                    MaintenanceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrMaintenances", x => x.MaintenanceId);
                    table.ForeignKey(
                        name: "FK_TrMaintenances_MsCars_CarId",
                        column: x => x.CarId,
                        principalTable: "MsCars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrMaintenances_MsEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "MsEmployees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LtPayments",
                columns: table => new
                {
                    PaymentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RentalId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LtPayments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_LtPayments_TrRentals_RentalId",
                        column: x => x.RentalId,
                        principalTable: "TrRentals",
                        principalColumn: "RentalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LtPayments_RentalId",
                table: "LtPayments",
                column: "RentalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MsCarImages_CarId",
                table: "MsCarImages",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_MsCars_LicensePlate",
                table: "MsCars",
                column: "LicensePlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MsCustomers_Email",
                table: "MsCustomers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MsEmployees_Email",
                table: "MsEmployees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrMaintenances_CarId",
                table: "TrMaintenances",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TrMaintenances_EmployeeId",
                table: "TrMaintenances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrRentals_CarId",
                table: "TrRentals",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_TrRentals_CustomerId",
                table: "TrRentals",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LtPayments");

            migrationBuilder.DropTable(
                name: "MsCarImages");

            migrationBuilder.DropTable(
                name: "TrMaintenances");

            migrationBuilder.DropTable(
                name: "TrRentals");

            migrationBuilder.DropTable(
                name: "MsEmployees");

            migrationBuilder.DropTable(
                name: "MsCars");

            migrationBuilder.DropTable(
                name: "MsCustomers");
        }
    }
}
