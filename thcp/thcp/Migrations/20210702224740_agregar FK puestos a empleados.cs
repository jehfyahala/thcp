using Microsoft.EntityFrameworkCore.Migrations;

namespace thcp.Migrations
{
    public partial class agregarFKpuestosaempleados : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Position_PositionId",
                table: "Employee");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "Employee",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmetId",
                table: "Employee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmetId",
                table: "Employee",
                column: "DepartmetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Departments_DepartmetId",
                table: "Employee",
                column: "DepartmetId",
                principalTable: "Departments",
                principalColumn: "DepartmetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Position_PositionId",
                table: "Employee",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "PositionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Departments_DepartmetId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Position_PositionId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_DepartmetId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DepartmetId",
                table: "Employee");

            migrationBuilder.AlterColumn<int>(
                name: "PositionId",
                table: "Employee",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Position_PositionId",
                table: "Employee",
                column: "PositionId",
                principalTable: "Position",
                principalColumn: "PositionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
