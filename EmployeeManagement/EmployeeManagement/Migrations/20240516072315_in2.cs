using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.Migrations
{
    /// <inheritdoc />
    public partial class in2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7736eb93-da2c-415c-84da-268f768951bb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c2568ef1-0303-4abd-a12b-da535a14930d"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ec7bf05a-86cb-4d35-bc0a-ea7ca4a860db"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f4698dae-6762-4cb8-90cf-3753b7776adc"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("e5c30779-587a-46a2-921c-53c4daaa6f6b"), null, "Admin", "ADMIN" },
                    { new Guid("f1edd2a6-6edd-464b-b752-d60873d13dd5"), null, "Employee", "EMPLOYEE" },
                    { new Guid("fffa10d2-899d-48e6-9e64-29e13f76a951"), null, "Manager", "MANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("6f72194c-3806-4398-bbd3-8c5e73a15d21"), 0, "50990762-a2d7-428c-be15-df8a0370595d", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEMG+S7KrAvShSQxsyAWT/sNrJLdrAvmG8BH8rHvUlZ1dyrrbj87iuoN+5CCrXwIoDQ==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin@gmail.com", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e5c30779-587a-46a2-921c-53c4daaa6f6b"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f1edd2a6-6edd-464b-b752-d60873d13dd5"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fffa10d2-899d-48e6-9e64-29e13f76a951"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("6f72194c-3806-4398-bbd3-8c5e73a15d21"));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7736eb93-da2c-415c-84da-268f768951bb"), null, "Manager", "MANAGER" },
                    { new Guid("c2568ef1-0303-4abd-a12b-da535a14930d"), null, "Employee", "EMPLOYEE" },
                    { new Guid("ec7bf05a-86cb-4d35-bc0a-ea7ca4a860db"), null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "FirstName", "IsActive", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ResetToken", "ResetTokenExpire", "SecurityStamp", "TwoFactorEnabled", "UserName", "VerificationToken" },
                values: new object[] { new Guid("f4698dae-6762-4cb8-90cf-3753b7776adc"), 0, "ccef46f1-15e3-4c85-9994-973389391d0d", null, "admin@gmail.com", true, "admin", true, "admin", false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEIlc4n0IBJ3hPycasgygmpgvzh4d+OGXhKjl0dL9tT7e6MxmbG66HvBqw7eFcUCjkg==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin@gmail.com", null });
        }
    }
}
