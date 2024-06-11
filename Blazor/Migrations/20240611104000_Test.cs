using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "ReservationCount",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "Workspaces",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_RoomId",
                table: "Workspaces",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_Rooms_RoomId",
                table: "Workspaces",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_Rooms_RoomId",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Workspaces_RoomId",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Workspaces");

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Rooms",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservationCount",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
