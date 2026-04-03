using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_inventory_warehouses_warehouseid",
                table: "users_inventory");

            migrationBuilder.DropIndex(
                name: "ix_users_inventory_warehouseid",
                table: "users_inventory");

            migrationBuilder.DropColumn(
                name: "name",
                table: "warehouses");

            migrationBuilder.RenameColumn(
                name: "warehouseid",
                table: "users_inventory",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "users_inventory",
                newName: "level");

            migrationBuilder.AddColumn<decimal>(
                name: "comapny_balance",
                table: "users",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "company_name",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comapny_balance",
                table: "users");

            migrationBuilder.DropColumn(
                name: "company_name",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "users_inventory",
                newName: "warehouseid");

            migrationBuilder.RenameColumn(
                name: "level",
                table: "users_inventory",
                newName: "quantity");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "warehouses",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_inventory_warehouseid",
                table: "users_inventory",
                column: "warehouseid");

            migrationBuilder.AddForeignKey(
                name: "fk_users_inventory_warehouses_warehouseid",
                table: "users_inventory",
                column: "warehouseid",
                principalTable: "warehouses",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
