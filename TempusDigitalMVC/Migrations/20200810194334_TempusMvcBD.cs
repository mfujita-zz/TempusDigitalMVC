using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TempusDigitalMVC.Migrations
{
    public partial class TempusMvcBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CadastroCliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    CPF = table.Column<string>(unicode: false, maxLength: 10, nullable: false),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "getdate()"),
                    RendaFamiliar = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CadastroCliente", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CadastroCliente");
        }
    }
}
