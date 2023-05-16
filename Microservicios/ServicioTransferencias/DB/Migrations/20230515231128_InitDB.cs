using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DB.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Respuestas",
                columns: table => new
                {
                    respuestaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    resultado = table.Column<int>(type: "int", nullable: false),
                    cbuOrigen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cbuDestino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    importe = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Respuestas", x => x.respuestaId);
                });

            migrationBuilder.CreateTable(
                name: "Transferencias",
                columns: table => new
                {
                    transferenciaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cuilOriginante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cuilDestinatario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cbuOrigen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cbuDestino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    importe = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    concepto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    respuestaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transferencias", x => x.transferenciaId);
                    table.ForeignKey(
                        name: "FK_Transferencias_Respuestas_respuestaId",
                        column: x => x.respuestaId,
                        principalTable: "Respuestas",
                        principalColumn: "respuestaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transferencias_respuestaId",
                table: "Transferencias",
                column: "respuestaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transferencias");

            migrationBuilder.DropTable(
                name: "Respuestas");
        }
    }
}
