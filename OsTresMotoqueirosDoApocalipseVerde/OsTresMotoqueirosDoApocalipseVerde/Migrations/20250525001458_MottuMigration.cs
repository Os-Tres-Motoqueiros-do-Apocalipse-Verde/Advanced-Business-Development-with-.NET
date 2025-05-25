using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsTresMotoqueirosDoApocalipseVerde.Migrations
{
    /// <inheritdoc />
    public partial class MottuMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "DADOS_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence<int>(
                name: "MODELO_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence<int>(
                name: "MOTO_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence<int>(
                name: "MOTORISTA_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "DADOS",
                columns: table => new
                {
                    ID_DADOS = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "DADOS_SEQ.NEXTVAL"),
                    CPF = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    TELEFONE = table.Column<string>(type: "NVARCHAR2(13)", maxLength: 13, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DADOS", x => x.ID_DADOS);
                });

            migrationBuilder.CreateTable(
                name: "MODELO",
                columns: table => new
                {
                    ID_MODELO = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "MODELO_SEQ.NEXTVAL"),
                    NOME_MODELO = table.Column<string>(type: "NVARCHAR2(25)", maxLength: 25, nullable: false),
                    FRENAGEM = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    SIS_PARTIDA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    TANQUE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIPO_COMBUSTIVEL = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CONSUMO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MODELO", x => x.ID_MODELO);
                });

            migrationBuilder.CreateTable(
                name: "MOTORISTA",
                columns: table => new
                {
                    ID_MOTORISTA = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "MOTORISTA_SEQ.NEXTVAL"),
                    PLANO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ID_DADOS = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOTORISTA", x => x.ID_MOTORISTA);
                    table.ForeignKey(
                        name: "FK_MOTORISTA_DADOS_ID_DADOS",
                        column: x => x.ID_DADOS,
                        principalTable: "DADOS",
                        principalColumn: "ID_DADOS",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MOTO",
                columns: table => new
                {
                    ID_MOTO = table.Column<int>(type: "NUMBER(10)", nullable: false, defaultValueSql: "MOTO_SEQ.NEXTVAL"),
                    PLACA = table.Column<string>(type: "NVARCHAR2(7)", maxLength: 7, nullable: false),
                    CHASSI = table.Column<string>(type: "NVARCHAR2(17)", maxLength: 17, nullable: false),
                    CONDICAO = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    LATITUDE = table.Column<string>(type: "NVARCHAR2(5)", maxLength: 5, nullable: false),
                    LONGITUDE = table.Column<string>(type: "NVARCHAR2(5)", maxLength: 5, nullable: false),
                    ID_MODELO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOTO", x => x.ID_MOTO);
                    table.ForeignKey(
                        name: "FK_MOTO_MODELO_ID_MODELO",
                        column: x => x.ID_MODELO,
                        principalTable: "MODELO",
                        principalColumn: "ID_MODELO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_ID_MODELO",
                table: "MOTO",
                column: "ID_MODELO");

            migrationBuilder.CreateIndex(
                name: "IX_MOTORISTA_ID_DADOS",
                table: "MOTORISTA",
                column: "ID_DADOS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MOTO");

            migrationBuilder.DropTable(
                name: "MOTORISTA");

            migrationBuilder.DropTable(
                name: "MODELO");

            migrationBuilder.DropTable(
                name: "DADOS");

            migrationBuilder.DropSequence(
                name: "DADOS_SEQ");

            migrationBuilder.DropSequence(
                name: "MODELO_SEQ");

            migrationBuilder.DropSequence(
                name: "MOTO_SEQ");

            migrationBuilder.DropSequence(
                name: "MOTORISTA_SEQ");
        }
    }
}
