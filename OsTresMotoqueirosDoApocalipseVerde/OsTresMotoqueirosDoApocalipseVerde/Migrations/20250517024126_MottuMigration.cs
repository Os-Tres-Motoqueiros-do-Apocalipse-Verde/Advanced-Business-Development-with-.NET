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
            migrationBuilder.CreateTable(
                name: "modelo",
                columns: table => new
                {
                    IdModelo = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeModelo = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    Frenagem = table.Column<int>(type: "NUMBER(10)", maxLength: 50, nullable: false),
                    SistemaPartida = table.Column<int>(type: "NUMBER(10)", maxLength: 100, nullable: false),
                    Tanque = table.Column<float>(type: "BINARY_FLOAT", maxLength: 3, nullable: false),
                    TipoCombustivel = table.Column<int>(type: "NUMBER(10)", maxLength: 50, nullable: false),
                    Consumo = table.Column<float>(type: "BINARY_FLOAT", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_modelo", x => x.IdModelo);
                });

            migrationBuilder.CreateTable(
                name: "situacao",
                columns: table => new
                {
                    IdSituacao = table.Column<long>(type: "NUMBER(19)", maxLength: 14, nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    Status = table.Column<int>(type: "NUMBER(10)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_situacao", x => x.IdSituacao);
                });

            migrationBuilder.CreateTable(
                name: "dados",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", maxLength: 15, nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CPF = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    Telefone = table.Column<string>(type: "NVARCHAR2(13)", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Senha = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    Nome = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    FuncionarioId = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    FuncionarioIdFuncionario = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    MotoristaId = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    MotoristaIdMotorista = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "motorista",
                columns: table => new
                {
                    IdMotorista = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Plano = table.Column<int>(type: "NUMBER(10)", maxLength: 20, nullable: false),
                    DadosId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motorista", x => x.IdMotorista);
                    table.ForeignKey(
                        name: "FK_motorista_dados_DadosId",
                        column: x => x.DadosId,
                        principalTable: "dados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "endereco",
                columns: table => new
                {
                    IdEndereco = table.Column<long>(type: "NUMBER(19)", maxLength: 20, nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Numero = table.Column<int>(type: "NUMBER(10)", maxLength: 4, nullable: false),
                    Estado = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    CodigoPais = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    CodigoPostal = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Complemento = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    Rua = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    FilialId = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    FilialIdFilial = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_endereco", x => x.IdEndereco);
                });

            migrationBuilder.CreateTable(
                name: "filial",
                columns: table => new
                {
                    IdFilial = table.Column<long>(type: "NUMBER(19)", maxLength: 17, nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeFilial = table.Column<string>(type: "NVARCHAR2(150)", maxLength: 150, nullable: false),
                    ResponsavelId = table.Column<long>(type: "NUMBER(19)", maxLength: 7, nullable: false),
                    EnderecoId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_filial", x => x.IdFilial);
                    table.ForeignKey(
                        name: "FK_filial_endereco_EnderecoId",
                        column: x => x.EnderecoId,
                        principalTable: "endereco",
                        principalColumn: "IdEndereco",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "funcionario",
                columns: table => new
                {
                    IdFuncionario = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Cargo = table.Column<int>(type: "NUMBER(10)", maxLength: 100, nullable: false),
                    FilialId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    DadosId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_funcionario", x => x.IdFuncionario);
                    table.ForeignKey(
                        name: "FK_funcionario_dados_DadosId",
                        column: x => x.DadosId,
                        principalTable: "dados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_funcionario_filial_FilialId",
                        column: x => x.FilialId,
                        principalTable: "filial",
                        principalColumn: "IdFilial",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "patio",
                columns: table => new
                {
                    IdPatio = table.Column<long>(type: "NUMBER(19)", maxLength: 30, nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    TotalMotos = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CapacidadeMoto = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AreaPatio = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FilialId = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patio", x => x.IdPatio);
                    table.ForeignKey(
                        name: "FK_patio_filial_FilialId",
                        column: x => x.FilialId,
                        principalTable: "filial",
                        principalColumn: "IdFilial",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "setor",
                columns: table => new
                {
                    IdSetor = table.Column<long>(type: "NUMBER(19)", maxLength: 17, nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    QuantidadeMoto = table.Column<int>(type: "NUMBER(10)", maxLength: 3, nullable: false),
                    Capacidade = table.Column<int>(type: "NUMBER(10)", maxLength: 3, nullable: false),
                    AreaSetor = table.Column<long>(type: "NUMBER(19)", maxLength: 5, nullable: false),
                    NomeSetor = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    Descricao = table.Column<string>(type: "NVARCHAR2(250)", maxLength: 250, nullable: false),
                    PatioId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    PatioIdPatio = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_setor", x => x.IdSetor);
                    table.ForeignKey(
                        name: "FK_setor_patio_PatioId",
                        column: x => x.PatioId,
                        principalTable: "patio",
                        principalColumn: "IdPatio",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_setor_patio_PatioIdPatio",
                        column: x => x.PatioIdPatio,
                        principalTable: "patio",
                        principalColumn: "IdPatio");
                });

            migrationBuilder.CreateTable(
                name: "moto",
                columns: table => new
                {
                    IdMoto = table.Column<long>(type: "NUMBER(19)", maxLength: 16, nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Placa = table.Column<string>(type: "NVARCHAR2(7)", maxLength: 7, nullable: false),
                    Chassi = table.Column<string>(type: "NVARCHAR2(17)", maxLength: 17, nullable: false),
                    Condicao = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    Latitude = table.Column<float>(type: "BINARY_FLOAT", maxLength: 5, nullable: false),
                    Longitude = table.Column<float>(type: "BINARY_FLOAT", maxLength: 5, nullable: false),
                    ModeloId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    SetorId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    MotoristaId = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ModeloIdModelo = table.Column<long>(type: "NUMBER(19)", nullable: true),
                    SetorIdSetor = table.Column<long>(type: "NUMBER(19)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moto", x => x.IdMoto);
                    table.ForeignKey(
                        name: "FK_moto_modelo_ModeloId",
                        column: x => x.ModeloId,
                        principalTable: "modelo",
                        principalColumn: "IdModelo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_moto_modelo_ModeloIdModelo",
                        column: x => x.ModeloIdModelo,
                        principalTable: "modelo",
                        principalColumn: "IdModelo");
                    table.ForeignKey(
                        name: "FK_moto_motorista_MotoristaId",
                        column: x => x.MotoristaId,
                        principalTable: "motorista",
                        principalColumn: "IdMotorista",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_moto_setor_SetorId",
                        column: x => x.SetorId,
                        principalTable: "setor",
                        principalColumn: "IdSetor",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_moto_setor_SetorIdSetor",
                        column: x => x.SetorIdSetor,
                        principalTable: "setor",
                        principalColumn: "IdSetor");
                });

            migrationBuilder.CreateTable(
                name: "MotoSituacao",
                columns: table => new
                {
                    MotosIdMoto = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    SituacoesIdSituacao = table.Column<long>(type: "NUMBER(19)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotoSituacao", x => new { x.MotosIdMoto, x.SituacoesIdSituacao });
                    table.ForeignKey(
                        name: "FK_MotoSituacao_moto_MotosIdMoto",
                        column: x => x.MotosIdMoto,
                        principalTable: "moto",
                        principalColumn: "IdMoto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotoSituacao_situacao_SituacoesIdSituacao",
                        column: x => x.SituacoesIdSituacao,
                        principalTable: "situacao",
                        principalColumn: "IdSituacao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dados_FuncionarioIdFuncionario",
                table: "dados",
                column: "FuncionarioIdFuncionario");

            migrationBuilder.CreateIndex(
                name: "IX_dados_MotoristaIdMotorista",
                table: "dados",
                column: "MotoristaIdMotorista");

            migrationBuilder.CreateIndex(
                name: "IX_endereco_FilialIdFilial",
                table: "endereco",
                column: "FilialIdFilial");

            migrationBuilder.CreateIndex(
                name: "IX_filial_EnderecoId",
                table: "filial",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_funcionario_DadosId",
                table: "funcionario",
                column: "DadosId");

            migrationBuilder.CreateIndex(
                name: "IX_funcionario_FilialId",
                table: "funcionario",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_moto_ModeloId",
                table: "moto",
                column: "ModeloId");

            migrationBuilder.CreateIndex(
                name: "IX_moto_ModeloIdModelo",
                table: "moto",
                column: "ModeloIdModelo");

            migrationBuilder.CreateIndex(
                name: "IX_moto_MotoristaId",
                table: "moto",
                column: "MotoristaId");

            migrationBuilder.CreateIndex(
                name: "IX_moto_SetorId",
                table: "moto",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_moto_SetorIdSetor",
                table: "moto",
                column: "SetorIdSetor");

            migrationBuilder.CreateIndex(
                name: "IX_motorista_DadosId",
                table: "motorista",
                column: "DadosId");

            migrationBuilder.CreateIndex(
                name: "IX_MotoSituacao_SituacoesIdSituacao",
                table: "MotoSituacao",
                column: "SituacoesIdSituacao");

            migrationBuilder.CreateIndex(
                name: "IX_patio_FilialId",
                table: "patio",
                column: "FilialId");

            migrationBuilder.CreateIndex(
                name: "IX_setor_PatioId",
                table: "setor",
                column: "PatioId");

            migrationBuilder.CreateIndex(
                name: "IX_setor_PatioIdPatio",
                table: "setor",
                column: "PatioIdPatio");

            migrationBuilder.AddForeignKey(
                name: "FK_dados_funcionario_FuncionarioIdFuncionario",
                table: "dados",
                column: "FuncionarioIdFuncionario",
                principalTable: "funcionario",
                principalColumn: "IdFuncionario",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dados_motorista_MotoristaIdMotorista",
                table: "dados",
                column: "MotoristaIdMotorista",
                principalTable: "motorista",
                principalColumn: "IdMotorista",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_endereco_filial_FilialIdFilial",
                table: "endereco",
                column: "FilialIdFilial",
                principalTable: "filial",
                principalColumn: "IdFilial",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dados_funcionario_FuncionarioIdFuncionario",
                table: "dados");

            migrationBuilder.DropForeignKey(
                name: "FK_dados_motorista_MotoristaIdMotorista",
                table: "dados");

            migrationBuilder.DropForeignKey(
                name: "FK_endereco_filial_FilialIdFilial",
                table: "endereco");

            migrationBuilder.DropTable(
                name: "MotoSituacao");

            migrationBuilder.DropTable(
                name: "moto");

            migrationBuilder.DropTable(
                name: "situacao");

            migrationBuilder.DropTable(
                name: "modelo");

            migrationBuilder.DropTable(
                name: "setor");

            migrationBuilder.DropTable(
                name: "patio");

            migrationBuilder.DropTable(
                name: "funcionario");

            migrationBuilder.DropTable(
                name: "motorista");

            migrationBuilder.DropTable(
                name: "dados");

            migrationBuilder.DropTable(
                name: "filial");

            migrationBuilder.DropTable(
                name: "endereco");
        }
    }
}
