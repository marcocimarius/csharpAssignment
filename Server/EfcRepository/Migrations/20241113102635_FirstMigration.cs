using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfcRepository.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EfcUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EfcUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EfcPost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    EfcUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EfcPost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EfcPost_EfcUser_EfcUserId",
                        column: x => x.EfcUserId,
                        principalTable: "EfcUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EfcComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Body = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    EfcUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    PostId = table.Column<int>(type: "INTEGER", nullable: false),
                    EfcPostId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EfcComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EfcComment_EfcPost_EfcPostId",
                        column: x => x.EfcPostId,
                        principalTable: "EfcPost",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EfcComment_EfcUser_EfcUserId",
                        column: x => x.EfcUserId,
                        principalTable: "EfcUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EfcComment_EfcPostId",
                table: "EfcComment",
                column: "EfcPostId");

            migrationBuilder.CreateIndex(
                name: "IX_EfcComment_EfcUserId",
                table: "EfcComment",
                column: "EfcUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EfcPost_EfcUserId",
                table: "EfcPost",
                column: "EfcUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EfcComment");

            migrationBuilder.DropTable(
                name: "EfcPost");

            migrationBuilder.DropTable(
                name: "EfcUser");
        }
    }
}
