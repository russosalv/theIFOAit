using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IFOA.DB.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ifoa");

            migrationBuilder.CreateTable(
                name: "Candidates",
                schema: "ifoa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Nationality = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResidenceCountry = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ranking = table.Column<int>(type: "int", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobFunctions",
                schema: "ifoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobFunctions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateCertifications",
                schema: "ifoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Graduation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DocumentLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateCertifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateCertifications_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "ifoa",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidatePreferredLocations",
                schema: "ifoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatePreferredLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidatePreferredLocations_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "ifoa",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateProjects",
                schema: "ifoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateProjects_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "ifoa",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidateSpokenLanguages",
                schema: "ifoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateSpokenLanguages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidateSpokenLanguages_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "ifoa",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CandidatePreferredJobFunctions",
                schema: "ifoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobFunctionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatePreferredJobFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidatePreferredJobFunctions_Candidates_CandidateId",
                        column: x => x.CandidateId,
                        principalSchema: "ifoa",
                        principalTable: "Candidates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidatePreferredJobFunctions_JobFunctions_JobFunctionId",
                        column: x => x.JobFunctionId,
                        principalSchema: "ifoa",
                        principalTable: "JobFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateCertifications_CandidateId",
                schema: "ifoa",
                table: "CandidateCertifications",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatePreferredJobFunctions_CandidateId",
                schema: "ifoa",
                table: "CandidatePreferredJobFunctions",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatePreferredJobFunctions_JobFunctionId",
                schema: "ifoa",
                table: "CandidatePreferredJobFunctions",
                column: "JobFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatePreferredLocations_CandidateId",
                schema: "ifoa",
                table: "CandidatePreferredLocations",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateProjects_CandidateId",
                schema: "ifoa",
                table: "CandidateProjects",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSpokenLanguages_CandidateId",
                schema: "ifoa",
                table: "CandidateSpokenLanguages",
                column: "CandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateCertifications",
                schema: "ifoa");

            migrationBuilder.DropTable(
                name: "CandidatePreferredJobFunctions",
                schema: "ifoa");

            migrationBuilder.DropTable(
                name: "CandidatePreferredLocations",
                schema: "ifoa");

            migrationBuilder.DropTable(
                name: "CandidateProjects",
                schema: "ifoa");

            migrationBuilder.DropTable(
                name: "CandidateSpokenLanguages",
                schema: "ifoa");

            migrationBuilder.DropTable(
                name: "JobFunctions",
                schema: "ifoa");

            migrationBuilder.DropTable(
                name: "Candidates",
                schema: "ifoa");
        }
    }
}
