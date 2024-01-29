using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SamplePipeDelimiter.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "pubacc_co",
                schema: "main",
                columns: table => new
                {
                    record_type = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    unique_system_identifier = table.Column<decimal>(type: "numeric(10,0)", precision: 10, scale: 0, nullable: false),
                    uls_file_num = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    callsign = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    comment_date = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    status_code = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: true),
                    status_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pubacc_co", x => x.record_type);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pubacc_co",
                schema: "main");
        }
    }
}
