using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZavrsniRad.Migrations
{
    /// <inheritdoc />
    public partial class PetTracker_KorisniciAplikacije : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KorisniciAplikacije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisniciAplikacije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjekti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OIB = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NazivTvrtke = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mobitel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjekti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KorisniciAplikacijeRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikAplikacijeId = table.Column<int>(type: "int", nullable: false),
                    RolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisniciAplikacijeRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisniciAplikacijeRole_KorisniciAplikacije_KorisnikAplikacijeId",
                        column: x => x.KorisnikAplikacijeId,
                        principalTable: "KorisniciAplikacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorisniciAplikacijeRole_Role_RolaId",
                        column: x => x.RolaId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KorisniciAplikacijeSubjekti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisnikAplikacijeId = table.Column<int>(type: "int", nullable: false),
                    SubjektId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorisniciAplikacijeSubjekti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KorisniciAplikacijeSubjekti_KorisniciAplikacije_KorisnikAplikacijeId",
                        column: x => x.KorisnikAplikacijeId,
                        principalTable: "KorisniciAplikacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KorisniciAplikacijeSubjekti_Subjekti_SubjektId",
                        column: x => x.SubjektId,
                        principalTable: "Subjekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciAplikacijeRole_KorisnikAplikacijeId",
                table: "KorisniciAplikacijeRole",
                column: "KorisnikAplikacijeId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciAplikacijeRole_RolaId",
                table: "KorisniciAplikacijeRole",
                column: "RolaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciAplikacijeSubjekti_KorisnikAplikacijeId",
                table: "KorisniciAplikacijeSubjekti",
                column: "KorisnikAplikacijeId");

            migrationBuilder.CreateIndex(
                name: "IX_KorisniciAplikacijeSubjekti_SubjektId",
                table: "KorisniciAplikacijeSubjekti",
                column: "SubjektId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorisniciAplikacijeRole");

            migrationBuilder.DropTable(
                name: "KorisniciAplikacijeSubjekti");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "KorisniciAplikacije");

            migrationBuilder.DropTable(
                name: "Subjekti");
        }
    }
}
