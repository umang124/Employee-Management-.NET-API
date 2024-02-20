using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee_Management_System_Net_Core_API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePerformanceEvaluationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "PerformanceEvaluations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "PerformanceEvaluations");
        }
    }
}
