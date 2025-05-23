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
                name: "DADOS_SEQ");

            migrationBuilder.CreateSequence<int>(
                name: "MOTORISTA_SEQ");

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

            migrationBuilder.CreateIndex(
                name: "IX_MOTORISTA_ID_DADOS",
                table: "MOTORISTA",
                column: "ID_DADOS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MOTORISTA");

            migrationBuilder.DropTable(
                name: "DADOS");

            migrationBuilder.DropSequence(
                name: "DADOS_SEQ");

            migrationBuilder.DropSequence(
                name: "MOTORISTA_SEQ");
        }
    }
}
