using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoadDataProject.Migrations
{
    /// <inheritdoc />
    public partial class added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_pubacc_em",
                schema: "main",
                table: "pubacc_em",
                column: "unique_system_identifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_pubacc_em",
                schema: "main",
                table: "pubacc_em");
        }
    }
}
