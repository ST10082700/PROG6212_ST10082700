using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PROG6212___CMCS___ST10082700.Migrations
{
    /// <inheritdoc />
    public partial class AddRLSAndAuthChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                schema: "public",
                table: "Claims",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmissionDate",
                schema: "public",
                table: "Claims",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "VerificationDate",
                schema: "public",
                table: "Claims",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerifiedBy",
                schema: "public",
                table: "Claims",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "SubmissionDate",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "VerificationDate",
                schema: "public",
                table: "Claims");

            migrationBuilder.DropColumn(
                name: "VerifiedBy",
                schema: "public",
                table: "Claims");
        }
    }
}
