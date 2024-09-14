using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "381a3100-8f8f-410d-ac2c-7a5a3d7c0422", null, "Member", "MEMBER" },
                    { "39f0447b-cd7e-4146-98aa-64f9a7d4a570", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "381a3100-8f8f-410d-ac2c-7a5a3d7c0422");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39f0447b-cd7e-4146-98aa-64f9a7d4a570");
        }
    }
}
