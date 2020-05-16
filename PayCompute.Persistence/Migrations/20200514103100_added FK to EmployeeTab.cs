using Microsoft.EntityFrameworkCore.Migrations;

namespace PayCompute.Persistence.Migrations
{
    public partial class addedFKtoEmployeeTab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OverTimeHours",
                table: "PaymentRecords",
                newName: "OvertimeHours");

            migrationBuilder.AddColumn<decimal>(
                name: "OvertimeEarnings",
                table: "PaymentRecords",
                type: "Money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OvertimeEarnings",
                table: "PaymentRecords");

            migrationBuilder.RenameColumn(
                name: "OvertimeHours",
                table: "PaymentRecords",
                newName: "OverTimeHours");
        }
    }
}
