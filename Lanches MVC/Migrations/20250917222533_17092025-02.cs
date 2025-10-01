using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lanches_MVC.Migrations
{
    /// <inheritdoc />
    public partial class _1709202502 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome, Descricao) VALUES('Natural', 'Lanches naturais e integrais')");
            migrationBuilder.Sql("INSERT INTO Categorias(CategoriaNome, Descricao) VALUES('Frango', 'Lanches com frango')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Categorias");
        }
    }
}
