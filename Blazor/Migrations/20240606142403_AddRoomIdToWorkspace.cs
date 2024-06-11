using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blazor.Migrations
{
    /// <inheritdoc />
    public partial class AddRoomIdToWorkspace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoomId",
                table: "Workspaces",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_RoomId",
                table: "Workspaces",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_Rooms_RoomId",
                table: "Workspaces",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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
        }
    }
}
