﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsTresMotoqueirosDoApocalipseVerde.Migrations
{
    /// <inheritdoc />
    public partial class MottuMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "DADOS_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "ENDERECO_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "FILIAL_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "FUNCIONARIO_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "MODELO_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "MOTO_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "MOTORISTA_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "PATIO_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "SETOR_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateSequence(
                name: "SITUACAO_SEQ",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "DADOS",
                columns: table => new
                {
                    ID_DADOS = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "DADOS_SEQ.NEXTVAL"),
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
                name: "ENDERECO",
                columns: table => new
                {
                    ID_ENDERECO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "ENDERECO_SEQ.NEXTVAL"),
                    NUMERO = table.Column<int>(type: "NUMBER(10)", maxLength: 4, nullable: false),
                    ESTADO = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    CODIGO_PAIS = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CODIGO_POSTAL = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    COMPLEMENTO = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    RUA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENDERECO", x => x.ID_ENDERECO);
                });

            migrationBuilder.CreateTable(
                name: "MODELO",
                columns: table => new
                {
                    ID_MODELO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "MODELO_SEQ.NEXTVAL"),
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
                name: "SITUACAO",
                columns: table => new
                {
                    ID_SITUACAO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SITUACAO_SEQ.NEXTVAL"),
                    NOME = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    STATUS = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SITUACAO", x => x.ID_SITUACAO);
                });

            migrationBuilder.CreateTable(
                name: "MOTORISTA",
                columns: table => new
                {
                    ID_MOTORISTA = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "MOTORISTA_SEQ.NEXTVAL"),
                    PLANO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ID_DADOS = table.Column<long>(type: "NUMBER(19)", nullable: false)
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
                name: "FILIAL",
                columns: table => new
                {
                    ID_FILIAL = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "FILIAL_SEQ.NEXTVAL"),
                    NOME_FILIAL = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    ID_ENDERECO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FILIAL", x => x.ID_FILIAL);
                    table.ForeignKey(
                        name: "FK_FILIAL_ENDERECO_ID_ENDERECO",
                        column: x => x.ID_ENDERECO,
                        principalTable: "ENDERECO",
                        principalColumn: "ID_ENDERECO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FUNCIONARIO",
                columns: table => new
                {
                    ID_FUNC = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "FUNCIONARIO_SEQ.NEXTVAL"),
                    CARGO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    ID_DADOS = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    FILIAL_ID = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    FilialId1 = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FUNCIONARIO", x => x.ID_FUNC);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIO_DADOS_ID_DADOS",
                        column: x => x.ID_DADOS,
                        principalTable: "DADOS",
                        principalColumn: "ID_DADOS",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIO_FILIAL_FILIAL_ID",
                        column: x => x.FILIAL_ID,
                        principalTable: "FILIAL",
                        principalColumn: "ID_FILIAL",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FUNCIONARIO_FILIAL_FilialId1",
                        column: x => x.FilialId1,
                        principalTable: "FILIAL",
                        principalColumn: "ID_FILIAL");
                });

            migrationBuilder.CreateTable(
                name: "PATIO",
                columns: table => new
                {
                    ID_PATIO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "PATIO_SEQ.NEXTVAL"),
                    TOTAL_MOTOS = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CAPACIDADE_MOTO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    LOCALIZACAO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ID_FILIAL = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIO", x => x.ID_PATIO);
                    table.ForeignKey(
                        name: "FK_PATIO_FILIAL_ID_FILIAL",
                        column: x => x.ID_FILIAL,
                        principalTable: "FILIAL",
                        principalColumn: "ID_FILIAL",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SETOR",
                columns: table => new
                {
                    ID_SETOR = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "SETOR_SEQ.NEXTVAL"),
                    NOME_SETOR = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    QTD_MOTO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CAPACIDADE_MOTO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    Cor = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LOCALIZACAO = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    PATIO_ID = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    PatioId1 = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SETOR", x => x.ID_SETOR);
                    table.ForeignKey(
                        name: "FK_SETOR_PATIO_PATIO_ID",
                        column: x => x.PATIO_ID,
                        principalTable: "PATIO",
                        principalColumn: "ID_PATIO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SETOR_PATIO_PatioId1",
                        column: x => x.PatioId1,
                        principalTable: "PATIO",
                        principalColumn: "ID_PATIO");
                });

            migrationBuilder.CreateTable(
                name: "MOTO",
                columns: table => new
                {
                    ID_MOTO = table.Column<long>(type: "NUMBER(19)", nullable: false, defaultValueSql: "MOTO_SEQ.NEXTVAL"),
                    PLACA = table.Column<string>(type: "NVARCHAR2(7)", maxLength: 7, nullable: false),
                    CHASSI = table.Column<string>(type: "NVARCHAR2(17)", maxLength: 17, nullable: false),
                    CONDICAO = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    LOCALIZACAO_MOTO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ID_MOTORISTA = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_MODELO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_SETOR = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_SITUACAO = table.Column<long>(type: "NUMBER(19)", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_MOTO_MOTORISTA_ID_MOTORISTA",
                        column: x => x.ID_MOTORISTA,
                        principalTable: "MOTORISTA",
                        principalColumn: "ID_MOTORISTA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOTO_SETOR_ID_SETOR",
                        column: x => x.ID_SETOR,
                        principalTable: "SETOR",
                        principalColumn: "ID_SETOR",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MOTO_SITUACAO_ID_SITUACAO",
                        column: x => x.ID_SITUACAO,
                        principalTable: "SITUACAO",
                        principalColumn: "ID_SITUACAO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FILIAL_ID_ENDERECO",
                table: "FILIAL",
                column: "ID_ENDERECO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_FILIAL_ID",
                table: "FUNCIONARIO",
                column: "FILIAL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_FilialId1",
                table: "FUNCIONARIO",
                column: "FilialId1");

            migrationBuilder.CreateIndex(
                name: "IX_FUNCIONARIO_ID_DADOS",
                table: "FUNCIONARIO",
                column: "ID_DADOS",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_ID_MODELO",
                table: "MOTO",
                column: "ID_MODELO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_ID_MOTORISTA",
                table: "MOTO",
                column: "ID_MOTORISTA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_ID_SETOR",
                table: "MOTO",
                column: "ID_SETOR",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MOTO_ID_SITUACAO",
                table: "MOTO",
                column: "ID_SITUACAO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MOTORISTA_ID_DADOS",
                table: "MOTORISTA",
                column: "ID_DADOS",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PATIO_ID_FILIAL",
                table: "PATIO",
                column: "ID_FILIAL");

            migrationBuilder.CreateIndex(
                name: "IX_SETOR_PATIO_ID",
                table: "SETOR",
                column: "PATIO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SETOR_PatioId1",
                table: "SETOR",
                column: "PatioId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FUNCIONARIO");

            migrationBuilder.DropTable(
                name: "MOTO");

            migrationBuilder.DropTable(
                name: "MODELO");

            migrationBuilder.DropTable(
                name: "MOTORISTA");

            migrationBuilder.DropTable(
                name: "SETOR");

            migrationBuilder.DropTable(
                name: "SITUACAO");

            migrationBuilder.DropTable(
                name: "DADOS");

            migrationBuilder.DropTable(
                name: "PATIO");

            migrationBuilder.DropTable(
                name: "FILIAL");

            migrationBuilder.DropTable(
                name: "ENDERECO");

            migrationBuilder.DropSequence(
                name: "DADOS_SEQ");

            migrationBuilder.DropSequence(
                name: "ENDERECO_SEQ");

            migrationBuilder.DropSequence(
                name: "FILIAL_SEQ");

            migrationBuilder.DropSequence(
                name: "FUNCIONARIO_SEQ");

            migrationBuilder.DropSequence(
                name: "MODELO_SEQ");

            migrationBuilder.DropSequence(
                name: "MOTO_SEQ");

            migrationBuilder.DropSequence(
                name: "MOTORISTA_SEQ");

            migrationBuilder.DropSequence(
                name: "PATIO_SEQ");

            migrationBuilder.DropSequence(
                name: "SETOR_SEQ");

            migrationBuilder.DropSequence(
                name: "SITUACAO_SEQ");
        }
    }
}
