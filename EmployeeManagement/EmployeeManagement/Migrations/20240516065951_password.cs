using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class password : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("91ec1da3-0600-4b57-ab13-630a3b8bef14"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ac2729af-209e-4ad2-8232-6a858496a12f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f879669d-f30a-4bbd-90aa-be7a6c7e9805"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cc840d9c-9fe5-4d2d-94f8-ffa590622ca1"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("91ec1da3-0600-4b57-ab13-630a3b8bef14"), null, "Admin", "ADMIN" },
                    { new Guid("ac2729af-209e-4ad2-8232-6a858496a12f"), null, "Manager", "MANAGER" },
                    { new Guid("f879669d-f30a-4bbd-90aa-be7a6c7e9805"), null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("cc840d9c-9fe5-4d2d-94f8-ffa590622ca1"), 0, "98587b21-16ba-4e81-ad21-ce45899b0383", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", null, null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin", null });
        }
    }
}
