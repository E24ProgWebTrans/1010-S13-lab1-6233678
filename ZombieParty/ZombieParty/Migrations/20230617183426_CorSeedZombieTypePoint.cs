using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZombieParty.Migrations
{
    public partial class CorSeedZombieTypePoint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ZombieTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Point",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ZombieTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Point",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ZombieTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Point",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ZombieTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Point",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ZombieTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Point",
                value: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ZombieTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Point",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ZombieTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Point",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ZombieTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Point",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ZombieTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Point",
                value: 0);

            migrationBuilder.UpdateData(
                table: "ZombieTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Point",
                value: 0);
        }
    }
}
