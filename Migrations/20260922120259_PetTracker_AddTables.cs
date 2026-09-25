using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZavrsniRad.Migrations
{
    /// <inheritdoc />
    public partial class PetTracker_AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Djelatnosti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oznaka = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Djelatnosti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drzave",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OznakaAlpha2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OznakaAlpha3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumerickaOznaka = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzave", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IdentifikacijskeOznakeVrste",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentifikacijskeOznakeVrste", x => x.Id);
                });

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
                name: "Pasmine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pasmine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PravniOblici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PravniOblici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spol",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spol", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjektiOsobe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjektiOsobe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjektiVrste",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjektiVrste", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipPrstena",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipPrstena", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VrsteAdresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteAdresa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZdravstveniZapisiVrste",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZdravstveniZapisiVrste", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZivotinjeStatusi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZivotinjeStatusi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ZivotinjeVrste",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZivotinjeVrste", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zupanije",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zupanije", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjekti",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NazivTvrtke = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telefon = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    VrstaOsobeId = table.Column<int>(type: "int", nullable: false),
                    VrstaSubjektaId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjekti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjekti_SubjektiOsobe_VrstaOsobeId",
                        column: x => x.VrstaOsobeId,
                        principalTable: "SubjektiOsobe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subjekti_SubjektiVrste_VrstaSubjektaId",
                        column: x => x.VrstaSubjektaId,
                        principalTable: "SubjektiVrste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZivotinjeVrstePasmina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VrstaZivotinjeId = table.Column<int>(type: "int", nullable: false),
                    PasminaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZivotinjeVrstePasmina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZivotinjeVrstePasmina_Pasmine_PasminaId",
                        column: x => x.PasminaId,
                        principalTable: "Pasmine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZivotinjeVrstePasmina_ZivotinjeVrste_VrstaZivotinjeId",
                        column: x => x.VrstaZivotinjeId,
                        principalTable: "ZivotinjeVrste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Naselja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZupanijaId = table.Column<int>(type: "int", nullable: true),
                    PostanskiBroj = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    DrzavaId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Naselja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Naselja_Drzave_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzave",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Naselja_Zupanije_ZupanijaId",
                        column: x => x.ZupanijaId,
                        principalTable: "Zupanije",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SubjektiDetalji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjektId = table.Column<int>(type: "int", nullable: false),
                    DatumOsnivanja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OIB = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    MBO = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    MB = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    MBS = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    PravniOblikId = table.Column<int>(type: "int", nullable: false),
                    DjelatnostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjektiDetalji", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjektiDetalji_Djelatnosti_DjelatnostId",
                        column: x => x.DjelatnostId,
                        principalTable: "Djelatnosti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjektiDetalji_PravniOblici_PravniOblikId",
                        column: x => x.PravniOblikId,
                        principalTable: "PravniOblici",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjektiDetalji_Subjekti_SubjektId",
                        column: x => x.SubjektId,
                        principalTable: "Subjekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjektiRasporedi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KorisniciAplikacijeId = table.Column<int>(type: "int", nullable: false),
                    SubjektId = table.Column<int>(type: "int", nullable: false),
                    AktivanRaspored = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjektiRasporedi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjektiRasporedi_KorisniciAplikacije_KorisniciAplikacijeId",
                        column: x => x.KorisniciAplikacijeId,
                        principalTable: "KorisniciAplikacije",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjektiRasporedi_Subjekti_SubjektId",
                        column: x => x.SubjektId,
                        principalTable: "Subjekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zivotinje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VrstaZivotinjePasminaId = table.Column<int>(type: "int", nullable: false),
                    SpolId = table.Column<int>(type: "int", nullable: false),
                    Boja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DatumRodenja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zivotinje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zivotinje_Spol_SpolId",
                        column: x => x.SpolId,
                        principalTable: "Spol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zivotinje_ZivotinjeStatusi_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ZivotinjeStatusi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zivotinje_ZivotinjeVrstePasmina_VrstaZivotinjePasminaId",
                        column: x => x.VrstaZivotinjePasminaId,
                        principalTable: "ZivotinjeVrstePasmina",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjektiAdrese",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjektId = table.Column<int>(type: "int", nullable: false),
                    Ulica = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KucniBroj = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    VrstaAdreseId = table.Column<int>(type: "int", nullable: false),
                    NaseljeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjektiAdrese", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjektiAdrese_Naselja_NaseljeId",
                        column: x => x.NaseljeId,
                        principalTable: "Naselja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjektiAdrese_Subjekti_SubjektId",
                        column: x => x.SubjektId,
                        principalTable: "Subjekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjektiAdrese_VrsteAdresa_VrstaAdreseId",
                        column: x => x.VrstaAdreseId,
                        principalTable: "VrsteAdresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjektiRasporediRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RasporedSubjektaId = table.Column<int>(type: "int", nullable: false),
                    RolaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjektiRasporediRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjektiRasporediRole_Role_RolaId",
                        column: x => x.RolaId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjektiRasporediRole_SubjektiRasporedi_RasporedSubjektaId",
                        column: x => x.RasporedSubjektaId,
                        principalTable: "SubjektiRasporedi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IdentifikacijskeOznakeZivotinje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZivotinjaId = table.Column<int>(type: "int", nullable: false),
                    VrstaIdentifikacijskeOznakeId = table.Column<int>(type: "int", nullable: false),
                    Oznaka = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DatumIzdavanja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DrzavaId = table.Column<int>(type: "int", nullable: true),
                    Oznacavatelj = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Napomena = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentifikacijskeOznakeZivotinje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IdentifikacijskeOznakeZivotinje_Drzave_DrzavaId",
                        column: x => x.DrzavaId,
                        principalTable: "Drzave",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_IdentifikacijskeOznakeZivotinje_IdentifikacijskeOznakeVrste_VrstaIdentifikacijskeOznakeId",
                        column: x => x.VrstaIdentifikacijskeOznakeId,
                        principalTable: "IdentifikacijskeOznakeVrste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IdentifikacijskeOznakeZivotinje_Zivotinje_ZivotinjaId",
                        column: x => x.ZivotinjaId,
                        principalTable: "Zivotinje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prehrana",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZivotinjaId = table.Column<int>(type: "int", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VrstaHrane = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NazivHrane = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Proizvodac = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    KolicinaHrane = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BrojObrokaDnevno = table.Column<int>(type: "int", nullable: true),
                    NamirniceZaIzbjegavanje = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Napomena = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prehrana", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prehrana_Zivotinje_ZivotinjaId",
                        column: x => x.ZivotinjaId,
                        principalTable: "Zivotinje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjektiZivotinje",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjektId = table.Column<int>(type: "int", nullable: false),
                    ZivotinjaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjektiZivotinje", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubjektiZivotinje_Subjekti_SubjektId",
                        column: x => x.SubjektId,
                        principalTable: "Subjekti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjektiZivotinje_Zivotinje_ZivotinjaId",
                        column: x => x.ZivotinjaId,
                        principalTable: "Zivotinje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZdravstveniZapisi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZivotinjaId = table.Column<int>(type: "int", nullable: false),
                    VrstaZdravstvenogZapisaId = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZdravstveniZapisi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZdravstveniZapisi_ZdravstveniZapisiVrste_VrstaZdravstvenogZapisaId",
                        column: x => x.VrstaZdravstvenogZapisaId,
                        principalTable: "ZdravstveniZapisiVrste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZdravstveniZapisi_Zivotinje_ZivotinjaId",
                        column: x => x.ZivotinjaId,
                        principalTable: "Zivotinje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetaljiMikrocipaLjubimaca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentifikacijskaOznakaZivotinjeId = table.Column<int>(type: "int", nullable: false),
                    MjestoImplantacije = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetaljiMikrocipaLjubimaca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetaljiMikrocipaLjubimaca_IdentifikacijskeOznakeZivotinje_IdentifikacijskaOznakaZivotinjeId",
                        column: x => x.IdentifikacijskaOznakaZivotinjeId,
                        principalTable: "IdentifikacijskeOznakeZivotinje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetaljiPrstenaLjubimaca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentifikacijskaOznakaZivotinjeId = table.Column<int>(type: "int", nullable: false),
                    TipPrstenaId = table.Column<int>(type: "int", nullable: false),
                    Noga = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Boja = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Materijal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UnutarnjiPromjer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Godina = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetaljiPrstenaLjubimaca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetaljiPrstenaLjubimaca_IdentifikacijskeOznakeZivotinje_IdentifikacijskaOznakaZivotinjeId",
                        column: x => x.IdentifikacijskaOznakaZivotinjeId,
                        principalTable: "IdentifikacijskeOznakeZivotinje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetaljiPrstenaLjubimaca_TipPrstena_TipPrstenaId",
                        column: x => x.TipPrstenaId,
                        principalTable: "TipPrstena",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetaljiTetovazeLjubimaca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentifikacijskaOznakaZivotinjeId = table.Column<int>(type: "int", nullable: false),
                    PolozajTetovaze = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Boja = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetaljiTetovazeLjubimaca", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetaljiTetovazeLjubimaca_IdentifikacijskeOznakeZivotinje_IdentifikacijskaOznakaZivotinjeId",
                        column: x => x.IdentifikacijskaOznakaZivotinjeId,
                        principalTable: "IdentifikacijskeOznakeZivotinje",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetaljiMikrocipaLjubimaca_IdentifikacijskaOznakaZivotinjeId",
                table: "DetaljiMikrocipaLjubimaca",
                column: "IdentifikacijskaOznakaZivotinjeId");

            migrationBuilder.CreateIndex(
                name: "IX_DetaljiPrstenaLjubimaca_IdentifikacijskaOznakaZivotinjeId",
                table: "DetaljiPrstenaLjubimaca",
                column: "IdentifikacijskaOznakaZivotinjeId");

            migrationBuilder.CreateIndex(
                name: "IX_DetaljiPrstenaLjubimaca_TipPrstenaId",
                table: "DetaljiPrstenaLjubimaca",
                column: "TipPrstenaId");

            migrationBuilder.CreateIndex(
                name: "IX_DetaljiTetovazeLjubimaca_IdentifikacijskaOznakaZivotinjeId",
                table: "DetaljiTetovazeLjubimaca",
                column: "IdentifikacijskaOznakaZivotinjeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentifikacijskeOznakeZivotinje_DrzavaId",
                table: "IdentifikacijskeOznakeZivotinje",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentifikacijskeOznakeZivotinje_VrstaIdentifikacijskeOznakeId",
                table: "IdentifikacijskeOznakeZivotinje",
                column: "VrstaIdentifikacijskeOznakeId");

            migrationBuilder.CreateIndex(
                name: "IX_IdentifikacijskeOznakeZivotinje_ZivotinjaId",
                table: "IdentifikacijskeOznakeZivotinje",
                column: "ZivotinjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Naselja_DrzavaId",
                table: "Naselja",
                column: "DrzavaId");

            migrationBuilder.CreateIndex(
                name: "IX_Naselja_ZupanijaId",
                table: "Naselja",
                column: "ZupanijaId");

            migrationBuilder.CreateIndex(
                name: "IX_Prehrana_ZivotinjaId",
                table: "Prehrana",
                column: "ZivotinjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjekti_VrstaOsobeId",
                table: "Subjekti",
                column: "VrstaOsobeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjekti_VrstaSubjektaId",
                table: "Subjekti",
                column: "VrstaSubjektaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiAdrese_NaseljeId",
                table: "SubjektiAdrese",
                column: "NaseljeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiAdrese_SubjektId",
                table: "SubjektiAdrese",
                column: "SubjektId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiAdrese_VrstaAdreseId",
                table: "SubjektiAdrese",
                column: "VrstaAdreseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiDetalji_DjelatnostId",
                table: "SubjektiDetalji",
                column: "DjelatnostId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiDetalji_PravniOblikId",
                table: "SubjektiDetalji",
                column: "PravniOblikId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiDetalji_SubjektId",
                table: "SubjektiDetalji",
                column: "SubjektId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiRasporedi_KorisniciAplikacijeId",
                table: "SubjektiRasporedi",
                column: "KorisniciAplikacijeId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiRasporedi_SubjektId",
                table: "SubjektiRasporedi",
                column: "SubjektId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiRasporediRole_RasporedSubjektaId",
                table: "SubjektiRasporediRole",
                column: "RasporedSubjektaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiRasporediRole_RolaId",
                table: "SubjektiRasporediRole",
                column: "RolaId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiZivotinje_SubjektId",
                table: "SubjektiZivotinje",
                column: "SubjektId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjektiZivotinje_ZivotinjaId",
                table: "SubjektiZivotinje",
                column: "ZivotinjaId");

            migrationBuilder.CreateIndex(
                name: "IX_ZdravstveniZapisi_VrstaZdravstvenogZapisaId",
                table: "ZdravstveniZapisi",
                column: "VrstaZdravstvenogZapisaId");

            migrationBuilder.CreateIndex(
                name: "IX_ZdravstveniZapisi_ZivotinjaId",
                table: "ZdravstveniZapisi",
                column: "ZivotinjaId");

            migrationBuilder.CreateIndex(
                name: "IX_Zivotinje_SpolId",
                table: "Zivotinje",
                column: "SpolId");

            migrationBuilder.CreateIndex(
                name: "IX_Zivotinje_StatusId",
                table: "Zivotinje",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Zivotinje_VrstaZivotinjePasminaId",
                table: "Zivotinje",
                column: "VrstaZivotinjePasminaId");

            migrationBuilder.CreateIndex(
                name: "IX_ZivotinjeVrstePasmina_PasminaId",
                table: "ZivotinjeVrstePasmina",
                column: "PasminaId");

            migrationBuilder.CreateIndex(
                name: "IX_ZivotinjeVrstePasmina_VrstaZivotinjeId",
                table: "ZivotinjeVrstePasmina",
                column: "VrstaZivotinjeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetaljiMikrocipaLjubimaca");

            migrationBuilder.DropTable(
                name: "DetaljiPrstenaLjubimaca");

            migrationBuilder.DropTable(
                name: "DetaljiTetovazeLjubimaca");

            migrationBuilder.DropTable(
                name: "Prehrana");

            migrationBuilder.DropTable(
                name: "SubjektiAdrese");

            migrationBuilder.DropTable(
                name: "SubjektiDetalji");

            migrationBuilder.DropTable(
                name: "SubjektiRasporediRole");

            migrationBuilder.DropTable(
                name: "SubjektiZivotinje");

            migrationBuilder.DropTable(
                name: "ZdravstveniZapisi");

            migrationBuilder.DropTable(
                name: "TipPrstena");

            migrationBuilder.DropTable(
                name: "IdentifikacijskeOznakeZivotinje");

            migrationBuilder.DropTable(
                name: "Naselja");

            migrationBuilder.DropTable(
                name: "VrsteAdresa");

            migrationBuilder.DropTable(
                name: "Djelatnosti");

            migrationBuilder.DropTable(
                name: "PravniOblici");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "SubjektiRasporedi");

            migrationBuilder.DropTable(
                name: "ZdravstveniZapisiVrste");

            migrationBuilder.DropTable(
                name: "IdentifikacijskeOznakeVrste");

            migrationBuilder.DropTable(
                name: "Zivotinje");

            migrationBuilder.DropTable(
                name: "Drzave");

            migrationBuilder.DropTable(
                name: "Zupanije");

            migrationBuilder.DropTable(
                name: "KorisniciAplikacije");

            migrationBuilder.DropTable(
                name: "Subjekti");

            migrationBuilder.DropTable(
                name: "Spol");

            migrationBuilder.DropTable(
                name: "ZivotinjeStatusi");

            migrationBuilder.DropTable(
                name: "ZivotinjeVrstePasmina");

            migrationBuilder.DropTable(
                name: "SubjektiOsobe");

            migrationBuilder.DropTable(
                name: "SubjektiVrste");

            migrationBuilder.DropTable(
                name: "Pasmine");

            migrationBuilder.DropTable(
                name: "ZivotinjeVrste");
        }
    }
}
