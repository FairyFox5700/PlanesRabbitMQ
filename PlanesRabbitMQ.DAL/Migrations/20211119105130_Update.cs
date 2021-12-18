using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanesRabbitMQ.DAL.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParametersId1",
                table: "Planes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "Chars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ScoutCharacteristicsId",
                table: "Chars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planes_ParametersId1",
                table: "Planes",
                column: "ParametersId1");

            migrationBuilder.CreateIndex(
                name: "IX_Chars_ScoutCharacteristicsId",
                table: "Chars",
                column: "ScoutCharacteristicsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chars_ScoutCharacteristics_ScoutCharacteristicsId",
                table: "Chars",
                column: "ScoutCharacteristicsId",
                principalTable: "ScoutCharacteristics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Planes_Parameters_ParametersId1",
                table: "Planes",
                column: "ParametersId1",
                principalTable: "Parameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chars_ScoutCharacteristics_ScoutCharacteristicsId",
                table: "Chars");

            migrationBuilder.DropForeignKey(
                name: "FK_Planes_Parameters_ParametersId1",
                table: "Planes");

            migrationBuilder.DropIndex(
                name: "IX_Planes_ParametersId1",
                table: "Planes");

            migrationBuilder.DropIndex(
                name: "IX_Chars_ScoutCharacteristicsId",
                table: "Chars");

            migrationBuilder.DropColumn(
                name: "ParametersId1",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "ScoutCharacteristicsId",
                table: "Chars");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Chars",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
