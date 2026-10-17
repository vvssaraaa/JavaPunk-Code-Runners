using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Javapunk.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Completed_modules_Modules_ModulesId",
                table: "Completed_modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Modules_ModulesId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "ModulesId",
                table: "Questions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ModulesId",
                table: "Completed_modules",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Completed_modules_Modules_ModulesId",
                table: "Completed_modules",
                column: "ModulesId",
                principalTable: "Modules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Modules_ModulesId",
                table: "Questions",
                column: "ModulesId",
                principalTable: "Modules",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Completed_modules_Modules_ModulesId",
                table: "Completed_modules");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Modules_ModulesId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "ModulesId",
                table: "Questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModulesId",
                table: "Completed_modules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Completed_modules_Modules_ModulesId",
                table: "Completed_modules",
                column: "ModulesId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Modules_ModulesId",
                table: "Questions",
                column: "ModulesId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
