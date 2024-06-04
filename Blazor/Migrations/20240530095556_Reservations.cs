using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.Migrations
{
    /// <inheritdoc />
    public partial class Reservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkspaceId",
                table: "Reservations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_WorkspaceId",
                table: "Reservations",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Workspaces_WorkspaceId",
                table: "Reservations",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Workspaces_WorkspaceId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_WorkspaceId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Reservations");
        }
    }
}
