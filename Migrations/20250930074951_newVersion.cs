using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoAppBE.Migrations
{
    /// <inheritdoc />
    public partial class newVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FolderResponseDTO");

            migrationBuilder.DropTable(
                name: "WrapperData");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 30, 7, 49, 51, 375, DateTimeKind.Unspecified).AddTicks(1467), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 30, 7, 49, 51, 375, DateTimeKind.Unspecified).AddTicks(1469), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 30, 7, 49, 51, 375, DateTimeKind.Unspecified).AddTicks(1470), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 30, 7, 49, 51, 375, DateTimeKind.Unspecified).AddTicks(1471), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "LastModified" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 30, 7, 49, 51, 375, DateTimeKind.Unspecified).AddTicks(1590), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 9, 30, 7, 49, 51, 375, DateTimeKind.Unspecified).AddTicks(1591), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "Email", "LastModified", "Password", "RefreshTokenExpiry" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 30, 7, 49, 51, 375, DateTimeKind.Unspecified).AddTicks(1574), new TimeSpan(0, 0, 0, 0, 0)), "admin@gmail.com", new DateTimeOffset(new DateTime(2025, 9, 30, 7, 49, 51, 375, DateTimeKind.Unspecified).AddTicks(1574), new TimeSpan(0, 0, 0, 0, 0)), "CB23F8BE8C9244F513F4597229009C78C35EB403AD2D3C215F38F770CD2925FC-7B063C3CEF7D171EE696CB489261AF80", new DateTime(2025, 9, 30, 7, 49, 51, 375, DateTimeKind.Utc).AddTicks(1576) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FolderResponseDTO",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    IsFile = table.Column<bool>(type: "bit", nullable: false),
                    IsFolder = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ParentFolderId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "WrapperData",
                columns: table => new
                {
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Messages = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
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
                columns: new[] { "Created", "Email", "LastModified", "Password", "RefreshTokenExpiry" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Unspecified).AddTicks(3146), new TimeSpan(0, 0, 0, 0, 0)), "admin@example.com", new DateTimeOffset(new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Unspecified).AddTicks(3146), new TimeSpan(0, 0, 0, 0, 0)), "admin123", new DateTime(2025, 9, 30, 7, 45, 13, 636, DateTimeKind.Utc).AddTicks(3148) });
        }
    }
}
