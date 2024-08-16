using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignPC.Migrations
{
    /// <inheritdoc />
    public partial class DBFodido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "4040, 1"),
                    NomeEquipa = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipamento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "3030, 1"),
                    NomeEquipamento = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipamento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Membro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumProcesso = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NomeMembro = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Genero = table.Column<int>(type: "int", nullable: false),
                    EmailMembro = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Curso = table.Column<int>(type: "int", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefEquipa = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioContas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1010, 1"),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioContas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipa_NomeEquipa",
                table: "Equipa",
                column: "NomeEquipa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Membro_EmailMembro",
                table: "Membro",
                column: "EmailMembro",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Membro_NumProcesso",
                table: "Membro",
                column: "NumProcesso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioContas_Email",
                table: "UsuarioContas",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioContas_Nome",
                table: "UsuarioContas",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioContas_Senha",
                table: "UsuarioContas",
                column: "Senha",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipa");

            migrationBuilder.DropTable(
                name: "Equipamento");

            migrationBuilder.DropTable(
                name: "Membro");

            migrationBuilder.DropTable(
                name: "UsuarioContas");
        }
    }
}
