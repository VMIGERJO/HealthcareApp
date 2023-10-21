using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareApp.Migrations
{
    /// <inheritdoc />
    public partial class DeleteManyToManyPrescriptionMedication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrescriptionMedication");

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionId",
                table: "Medications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medications_PrescriptionId",
                table: "Medications",
                column: "PrescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Prescriptions_PrescriptionId",
                table: "Medications",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Prescriptions_PrescriptionId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_PrescriptionId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "PrescriptionId",
                table: "Medications");

            migrationBuilder.CreateTable(
                name: "PrescriptionMedication",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false),
                    MedicationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMedication", x => new { x.PrescriptionID, x.MedicationID });
                    table.ForeignKey(
                        name: "FK_PrescriptionMedication_Medications_MedicationID",
                        column: x => x.MedicationID,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrescriptionMedication_Prescriptions_PrescriptionID",
                        column: x => x.PrescriptionID,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedication_MedicationID",
                table: "PrescriptionMedication",
                column: "MedicationID");
        }
    }
}
