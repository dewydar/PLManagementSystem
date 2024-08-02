using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLessonGroupsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LessonGroups",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "LessonGroups",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "LessonGroups");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "LessonGroups");
        }
    }
}
