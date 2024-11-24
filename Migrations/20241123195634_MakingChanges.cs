using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6212___CMCS___ST10082700.Migrations
{
    /// <inheritdoc />
    public partial class MakingChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "public",
                table: "Claims",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ClaimName",
                schema: "public",
                table: "Claims",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "SupportingDocumentPath",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                schema: "public",
                table: "Claims",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SupportingDocumentPath",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                schema: "public",
                table: "Claims");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "ClaimName",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }
    }
}
