using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class seed2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("2c60b91f-de0d-4f35-8eb2-dc2fb9e19009"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3c625749-651d-4039-8c64-cc7f54868685"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c2f03a6a-4ef6-429a-8870-5806e80ac3c8"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9bb36637-1411-490a-ab9f-6de65d2dbf54"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("55a1f933-d4ce-4a40-aa8c-68915d4e6877"), null, "Admin", "ADMIN" },
                    { new Guid("78f981fa-1bb1-4fa4-a844-cc2702d3fc93"), null, "Employee", "EMPLOYEE" },
                    { new Guid("92426fd8-dfbf-4626-ade6-a8a6a05c5f19"), null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("5ad16b65-7953-4005-bbf2-fb6c75a28f27"), 0, "01a91f12-cea1-4d0c-b9a3-087d2936ec08", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEIpAIztSrBCAj58zv2vrdWwavfGpITCgfdnmZdpOYUQeNlTauHuEiUJsA31sl7Kl9Q==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("55a1f933-d4ce-4a40-aa8c-68915d4e6877"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("78f981fa-1bb1-4fa4-a844-cc2702d3fc93"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("92426fd8-dfbf-4626-ade6-a8a6a05c5f19"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5ad16b65-7953-4005-bbf2-fb6c75a28f27"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("2c60b91f-de0d-4f35-8eb2-dc2fb9e19009"), null, "Employee", "EMPLOYEE" },
                    { new Guid("3c625749-651d-4039-8c64-cc7f54868685"), null, "Admin", "ADMIN" },
                    { new Guid("c2f03a6a-4ef6-429a-8870-5806e80ac3c8"), null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("9bb36637-1411-490a-ab9f-6de65d2dbf54"), 0, "cf298c43-36d2-4988-ab24-e98857acb12b", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAECyE4Z0QgUY7P/psbTuPd0uD+JeEJixejG5lY+Xy61ssXCyxmRXijDcILeNaofgacQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin", null });
        }
    }
}
