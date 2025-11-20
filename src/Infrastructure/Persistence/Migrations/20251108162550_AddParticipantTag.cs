using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cfo.Cats.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddParticipantTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "Participant",
                columns: table => new
                {
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ParticipantId = table.Column<string>(type: "nvarchar(9)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => new { x.Value, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_Tag_Participant_ParticipantId",
                        column: x => x.ParticipantId,
                        principalSchema: "Participant",
                        principalTable: "Participant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_ParticipantId",
                schema: "Participant",
                table: "Tag",
                column: "ParticipantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tag",
                schema: "Participant");
        }
    }
}
