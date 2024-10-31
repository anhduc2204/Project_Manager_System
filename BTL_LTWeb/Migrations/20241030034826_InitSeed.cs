using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BTL_LTWeb.Migrations
{
    public partial class InitSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "Priority" },
                values: new object[] { new Guid("473c2a9e-320e-4194-8672-d9929f90588a"), "admin", "ADMIN", 9999 });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "Priority" },
                values: new object[] { new Guid("b18fb4d4-9541-4096-a728-496f769ec947"), "user", "USER", 0 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Birthday", "CreatedAt", "Email", "FullName", "PasswordHash", "PhoneNumber" },
                values: new object[] { new Guid("a429ed06-491b-4d90-862a-fb1fb6f10cec"), "Khoa CNTT", null, new DateTime(2024, 10, 30, 3, 48, 25, 924, DateTimeKind.Utc).AddTicks(2897), "admin@fithou.com", "Administrator", "AQAAAAEAACcQAAAAEAJFLXXlb97B1RA1Uj49grORsHKEBplbo5Bb/yw04vdkq/uGO9CoMjNodQQMygrjwg==", "0123456789" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("473c2a9e-320e-4194-8672-d9929f90588a"), new Guid("a429ed06-491b-4d90-862a-fb1fb6f10cec") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b18fb4d4-9541-4096-a728-496f769ec947"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("473c2a9e-320e-4194-8672-d9929f90588a"), new Guid("a429ed06-491b-4d90-862a-fb1fb6f10cec") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("473c2a9e-320e-4194-8672-d9929f90588a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a429ed06-491b-4d90-862a-fb1fb6f10cec"));
        }
    }
}
