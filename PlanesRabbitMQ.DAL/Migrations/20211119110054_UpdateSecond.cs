using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanesRabbitMQ.DAL.Migrations
{
    public partial class UpdateSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planes_Parameters_ParametersId1",
                table: "Planes");

            migrationBuilder.DropIndex(
                name: "IX_Planes_ParametersId1",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "ParametersId1",
                table: "Planes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParametersId1",
                table: "Planes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Planes_ParametersId1",
                table: "Planes",
                column: "ParametersId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Planes_Parameters_ParametersId1",
                table: "Planes",
                column: "ParametersId1",
                principalTable: "Parameters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
