using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoAppBE.Migrations
{
    /// <inheritdoc />
    public partial class createdFileshare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileShares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileShares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileShares_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FolderResponseDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ParentFolderId = table.Column<int>(type: "int", nullable: true),
                    IsFile = table.Column<bool>(type: "bit", nullable: false),
                    IsFolder = table.Column<bool>(type: "bit", nullable: false),
                    FileSize = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "WrapperData",
                columns: table => new
                {
                    Messages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Unspecified).AddTicks(3032), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Unspecified).AddTicks(3035), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Unspecified).AddTicks(3038), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Unspecified).AddTicks(3038), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Unspecified).AddTicks(3163), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Unspecified).AddTicks(3164), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified", "RefreshTokenExpiry" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Unspecified).AddTicks(3146), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Unspecified).AddTicks(3146), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Utc).AddTicks(3148) });

            migrationBuilder.CreateIndex(
                name: "IX_FileShares_FileId",
                table: "FileShares",
                column: "FileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileShares");

            migrationBuilder.DropTable(
                name: "FolderResponseDTO");

            migrationBuilder.DropTable(
                name: "WrapperData");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 28, 4, 42, 39, 505, DateTimeKind.Unspecified).AddTicks(9575), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 28, 4, 42, 39, 505, DateTimeKind.Unspecified).AddTicks(9575), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 28, 4, 42, 39, 505, DateTimeKind.Unspecified).AddTicks(9577), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 28, 4, 42, 39, 505, DateTimeKind.Unspecified).AddTicks(9577), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 28, 4, 42, 39, 505, DateTimeKind.Unspecified).AddTicks(9859), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 28, 4, 42, 39, 505, DateTimeKind.Unspecified).AddTicks(9859), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified", "RefreshTokenExpiry" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 28, 4, 42, 39, 505, DateTimeKind.Unspecified).AddTicks(9813), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 28, 4, 42, 39, 505, DateTimeKind.Unspecified).AddTicks(9812), new TimeSpan(0, 0, 0, 0, 0)), new DateTime(2025, 9, 28, 4, 42, 39, 505, DateTimeKind.Utc).AddTicks(9814) });
        }
    }
}
