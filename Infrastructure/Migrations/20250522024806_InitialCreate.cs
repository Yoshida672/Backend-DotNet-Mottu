using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CP2_BackEndMottu_DotNet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Condicao",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Cor = table.Column<string>(type: "NVARCHAR2(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condicao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Moto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    Placa = table.Column<string>(type: "NVARCHAR2(10)", maxLength: 10, nullable: false),
                    Modelo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CondicaoId = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Moto_Condicao_CondicaoId",
                        column: x => x.CondicaoId,
                        principalTable: "Condicao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Localizacoes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    LATITUDE = table.Column<decimal>(type: "NUMBER(10,6)", nullable: false),
                    LONGITUDE = table.Column<decimal>(type: "NUMBER(10,6)", nullable: false),
                    TIMESTAMP = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    MOTO_ID = table.Column<Guid>(type: "RAW(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localizacoes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Localizacoes_Moto_MOTO_ID",
                        column: x => x.MOTO_ID,
                        principalTable: "Moto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Localizacoes_MOTO_ID",
                table: "Localizacoes",
                column: "MOTO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Moto_CondicaoId",
                table: "Moto",
                column: "CondicaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Localizacoes");

            migrationBuilder.DropTable(
                name: "Moto");

            migrationBuilder.DropTable(
                name: "Condicao");
        }
    }
}
