using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DistantWorkCalendarService.Migrations
{
    public partial class EventStatusContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "title",
                schema: "calendar",
                table: "events");

            migrationBuilder.DropColumn(
                name: "type",
                schema: "calendar",
                table: "events");

            migrationBuilder.RenameColumn(
                name: "start_date",
                schema: "calendar",
                table: "events",
                newName: "modified_date");

            migrationBuilder.RenameColumn(
                name: "end_date",
                schema: "calendar",
                table: "events",
                newName: "created_date");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "calendar",
                table: "events",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "statuses",
                schema: "calendar",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    event_type = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.id);
                    table.ForeignKey(
                        name: "FK_statuses_events_EventId",
                        column: x => x.EventId,
                        principalSchema: "calendar",
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_statuses_EventId",
                schema: "calendar",
                table: "statuses",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "statuses",
                schema: "calendar");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "calendar",
                table: "events");

            migrationBuilder.RenameColumn(
                name: "modified_date",
                schema: "calendar",
                table: "events",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "created_date",
                schema: "calendar",
                table: "events",
                newName: "end_date");

            migrationBuilder.AddColumn<string>(
                name: "title",
                schema: "calendar",
                table: "events",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "type",
                schema: "calendar",
                table: "events",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
