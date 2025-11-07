using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokiMani.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EnvelopeTransactiondoesnotneedenvelopeoraccounttransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnvelopeTransactions_AccountTransactions_AccountTransaction~",
                table: "EnvelopeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_EnvelopeTransactions_Envelopes_EnvelopeId",
                table: "EnvelopeTransactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnvelopeId",
                table: "EnvelopeTransactions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountTransactionId",
                table: "EnvelopeTransactions",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_EnvelopeTransactions_AccountTransactions_AccountTransaction~",
                table: "EnvelopeTransactions",
                column: "AccountTransactionId",
                principalTable: "AccountTransactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnvelopeTransactions_Envelopes_EnvelopeId",
                table: "EnvelopeTransactions",
                column: "EnvelopeId",
                principalTable: "Envelopes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnvelopeTransactions_AccountTransactions_AccountTransaction~",
                table: "EnvelopeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_EnvelopeTransactions_Envelopes_EnvelopeId",
                table: "EnvelopeTransactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnvelopeId",
                table: "EnvelopeTransactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountTransactionId",
                table: "EnvelopeTransactions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EnvelopeTransactions_AccountTransactions_AccountTransaction~",
                table: "EnvelopeTransactions",
                column: "AccountTransactionId",
                principalTable: "AccountTransactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnvelopeTransactions_Envelopes_EnvelopeId",
                table: "EnvelopeTransactions",
                column: "EnvelopeId",
                principalTable: "Envelopes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
