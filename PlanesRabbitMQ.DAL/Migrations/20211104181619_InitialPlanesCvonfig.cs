using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanesRabbitMQ.DAL.Migrations
{
    public partial class InitialPlanesCvonfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParametersCharacteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParametersCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScoutCharacteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RocketsCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoutCharacteristics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "int", nullable: false),
                    HasAmmunition = table.Column<bool>(type: "bit", nullable: false),
                    HasRadar = table.Column<bool>(type: "bit", nullable: false),
                    CharacteristicsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chars_ScoutCharacteristics_CharacteristicsId",
                        column: x => x.CharacteristicsId,
                        principalTable: "ScoutCharacteristics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CharsId = table.Column<int>(type: "int", nullable: true),
                    ParametersId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planes_Chars_CharsId",
                        column: x => x.CharsId,
                        principalTable: "Chars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Planes_Parameters_ParametersId",
                        column: x => x.ParametersId,
                        principalTable: "Parameters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chars_CharacteristicsId",
                table: "Chars",
                column: "CharacteristicsId");

            migrationBuilder.CreateIndex(
                name: "IX_Planes_CharsId",
                table: "Planes",
                column: "CharsId");

            migrationBuilder.CreateIndex(
                name: "IX_Planes_ParametersId",
                table: "Planes",
                column: "ParametersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParametersCharacteristics");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Chars");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "ScoutCharacteristics");
        }
    }
}
