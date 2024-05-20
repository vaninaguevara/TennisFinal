using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tennis.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jugador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dni = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Habilidad = table.Column<int>(type: "int", nullable: false),
                    Suerte = table.Column<int>(type: "int", nullable: false),
                    Fuerza = table.Column<int>(type: "int", nullable: false),
                    Velocidad = table.Column<int>(type: "int", nullable: false),
                    Reaccion = table.Column<int>(type: "int", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jugador", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiration = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Torneo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FechaTermino = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdJugadorW = table.Column<int>(type: "int", nullable: true),
                    JugadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Torneo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Torneo_Jugador_IdJugadorW",
                        column: x => x.IdJugadorW,
                        principalTable: "Jugador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Torneo_Jugador_JugadorId",
                        column: x => x.JugadorId,
                        principalTable: "Jugador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Partido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTorneo = table.Column<int>(type: "int", nullable: false),
                    IdJugadorL = table.Column<int>(type: "int", nullable: false),
                    IdJugadorW = table.Column<int>(type: "int", nullable: false),
                    TorneoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partido_Jugador_IdJugadorL",
                        column: x => x.IdJugadorL,
                        principalTable: "Jugador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partido_Jugador_IdJugadorW",
                        column: x => x.IdJugadorW,
                        principalTable: "Jugador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partido_Torneo_IdTorneo",
                        column: x => x.IdTorneo,
                        principalTable: "Torneo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Partido_Torneo_TorneoId",
                        column: x => x.TorneoId,
                        principalTable: "Torneo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TorneoJugador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JugadorId = table.Column<int>(type: "int", nullable: false),
                    JugadorId1 = table.Column<int>(type: "int", nullable: true),
                    TorneoId = table.Column<int>(type: "int", nullable: false),
                    TorneoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TorneoJugador", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jugador",
                        column: x => x.JugadorId,
                        principalTable: "Jugador",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Torneo",
                        column: x => x.TorneoId,
                        principalTable: "Torneo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_TorneoJugador_Jugador_JugadorId1",
                        column: x => x.JugadorId1,
                        principalTable: "Jugador",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TorneoJugador_Torneo_TorneoId1",
                        column: x => x.TorneoId1,
                        principalTable: "Torneo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Partido_IdJugadorL",
                table: "Partido",
                column: "IdJugadorL");

            migrationBuilder.CreateIndex(
                name: "IX_Partido_IdJugadorW",
                table: "Partido",
                column: "IdJugadorW");

            migrationBuilder.CreateIndex(
                name: "IX_Partido_IdTorneo",
                table: "Partido",
                column: "IdTorneo");

            migrationBuilder.CreateIndex(
                name: "IX_Partido_TorneoId",
                table: "Partido",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_Torneo_IdJugadorW",
                table: "Torneo",
                column: "IdJugadorW");

            migrationBuilder.CreateIndex(
                name: "IX_Torneo_JugadorId",
                table: "Torneo",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_TorneoJugador_JugadorId",
                table: "TorneoJugador",
                column: "JugadorId");

            migrationBuilder.CreateIndex(
                name: "IX_TorneoJugador_JugadorId1",
                table: "TorneoJugador",
                column: "JugadorId1");

            migrationBuilder.CreateIndex(
                name: "IX_TorneoJugador_TorneoId",
                table: "TorneoJugador",
                column: "TorneoId");

            migrationBuilder.CreateIndex(
                name: "IX_TorneoJugador_TorneoId1",
                table: "TorneoJugador",
                column: "TorneoId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partido");

            migrationBuilder.DropTable(
                name: "TorneoJugador");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Torneo");

            migrationBuilder.DropTable(
                name: "Jugador");
        }
    }
}
