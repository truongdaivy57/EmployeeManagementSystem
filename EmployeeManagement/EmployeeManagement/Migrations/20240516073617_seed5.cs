using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class seed5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("052c78e9-1e99-4bff-8d57-5f152a79fa84"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("95238d51-f271-40b9-b147-c0c7d00e14ed"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c84a6f84-852a-467c-92d5-1ef666205b3f"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("3aa9eab5-bfdb-453a-94d7-7d19e2cd5956"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1651528e-187f-4aad-a4e1-5e33405f86d8"), null, "Admin", "ADMIN" },
                    { new Guid("8bfdcfc7-4a58-4b7c-b3ad-90b992212bb3"), null, "Employee", "EMPLOYEE" },
                    { new Guid("b1a823ca-b67d-4f93-a450-c3509fbd8686"), null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("b935613f-57bc-4a62-9957-d02eaf41710f"), 0, "9b82eb59-0c1e-46bf-88fd-2c973c617f9b", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEBtA0rTuFKpo/DSQ34flJv85CYCvKKcvAoftzuj3G+OtyAPi3zYvnH3CCklPDN8TZg==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "39faf954-aeed-4dfa-99fc-3b8d4e5ddfee", false, "admin@gmail.com", null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("1651528e-187f-4aad-a4e1-5e33405f86d8"), new Guid("b935613f-57bc-4a62-9957-d02eaf41710f") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8bfdcfc7-4a58-4b7c-b3ad-90b992212bb3"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b1a823ca-b67d-4f93-a450-c3509fbd8686"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("1651528e-187f-4aad-a4e1-5e33405f86d8"), new Guid("b935613f-57bc-4a62-9957-d02eaf41710f") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("1651528e-187f-4aad-a4e1-5e33405f86d8"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("b935613f-57bc-4a62-9957-d02eaf41710f"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("052c78e9-1e99-4bff-8d57-5f152a79fa84"), null, "Employee", "EMPLOYEE" },
                    { new Guid("95238d51-f271-40b9-b147-c0c7d00e14ed"), null, "Admin", "ADMIN" },
                    { new Guid("c84a6f84-852a-467c-92d5-1ef666205b3f"), null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("3aa9eab5-bfdb-453a-94d7-7d19e2cd5956"), 0, "e273ce96-dd4a-431f-9c46-ed750f3e4b3c", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEHhc2tk8/2lw/REiRawv99zHD+MolS1A2NnWm16AKFICcmrZJ5+zSGvXusfpC9EJCg==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "371582d6-5a9b-45ff-a5f9-1c24d08a5a57", false, "admin@gmail.com", null });
        }
    }
}
