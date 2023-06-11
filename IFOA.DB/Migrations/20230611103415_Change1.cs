using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IFOA.DB.Migrations
{
    /// <inheritdoc />
    public partial class Change1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Icon",
                schema: "ifoa",
                table: "JobFunctions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ListeningCefrLevel",
                schema: "ifoa",
                table: "CandidateSpokenLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReadingCefrLevel",
                schema: "ifoa",
                table: "CandidateSpokenLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SpeakingCefrLevel",
                schema: "ifoa",
                table: "CandidateSpokenLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WritingCefrLevel",
                schema: "ifoa",
                table: "CandidateSpokenLanguages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Icon",
                schema: "ifoa",
                table: "JobFunctions");

            migrationBuilder.DropColumn(
                name: "ListeningCefrLevel",
                schema: "ifoa",
                table: "CandidateSpokenLanguages");

            migrationBuilder.DropColumn(
                name: "ReadingCefrLevel",
                schema: "ifoa",
                table: "CandidateSpokenLanguages");

            migrationBuilder.DropColumn(
                name: "SpeakingCefrLevel",
                schema: "ifoa",
                table: "CandidateSpokenLanguages");

            migrationBuilder.DropColumn(
                name: "WritingCefrLevel",
                schema: "ifoa",
                table: "CandidateSpokenLanguages");
        }
    }
}
