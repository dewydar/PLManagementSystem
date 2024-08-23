using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonGroupsDaysTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LessonGroups_Dayes_DayId",
                table: "LessonGroups");

            migrationBuilder.DropIndex(
                name: "IX_LessonGroups_DayId",
                table: "LessonGroups");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "LessonGroups");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "LessonGroups");

            migrationBuilder.CreateTable(
                name: "LessonGroupsDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LessonGroupId = table.Column<int>(type: "int", nullable: false),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonGroupsDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonGroupsDays_Dayes_DayId",
                        column: x => x.DayId,
                        principalTable: "Dayes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonGroupsDays_LessonGroups_LessonGroupId",
                        column: x => x.LessonGroupId,
                        principalTable: "LessonGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LessonGroupsDays_DayId",
                table: "LessonGroupsDays",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonGroupsDays_LessonGroupId",
                table: "LessonGroupsDays",
                column: "LessonGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonGroupsDays");

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "LessonGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "Time",
                table: "LessonGroups",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_LessonGroups_DayId",
                table: "LessonGroups",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_LessonGroups_Dayes_DayId",
                table: "LessonGroups",
                column: "DayId",
                principalTable: "Dayes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
