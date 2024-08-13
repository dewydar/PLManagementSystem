using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDeleteToLessonGroupDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "LessonGroupsDays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "LessonGroupsDays");
        }
    }
}
