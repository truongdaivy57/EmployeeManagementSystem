using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class seed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4b15da5a-231f-436a-a83a-e35b73f3027a"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("8b49936c-2a7f-444a-b915-1ffdc6b62cb5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f48fde38-c4ff-404d-ac53-4add6ee5b9a1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d8b66d9f-f479-4420-927e-7116b228cb3a"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("4b15da5a-231f-436a-a83a-e35b73f3027a"), null, "Admin", "ADMIN" },
                    { new Guid("8b49936c-2a7f-444a-b915-1ffdc6b62cb5"), null, "Employee", "EMPLOYEE" },
                    { new Guid("f48fde38-c4ff-404d-ac53-4add6ee5b9a1"), null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("d8b66d9f-f479-4420-927e-7116b228cb3a"), 0, "86a09c25-b0de-412f-9aa9-a91970e2aa09", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAENbKQrrXzUaorBMJ21vR0OLzt7DOJC1rii1thUsnoC9xhB+zSWyh+uyVnXrMAuklLw==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin", null });
        }
    }
}
