using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareApp.Migrations
{
    /// <inheritdoc />
    public partial class RecreateManyToManyPrescriptionMedication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "MedicationPrescription",
                columns: table => new
                {
                    MedicationsId = table.Column<int>(type: "int", nullable: false),
                    PrescriptionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationPrescription", x => new { x.MedicationsId, x.PrescriptionsId });
                    table.ForeignKey(
                        name: "FK_MedicationPrescription_Medications_MedicationsId",
                        column: x => x.MedicationsId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicationPrescription_Prescriptions_PrescriptionsId",
                        column: x => x.PrescriptionsId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicationPrescription_PrescriptionsId",
                table: "MedicationPrescription",
                column: "PrescriptionsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicationPrescription");

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
    }
}
