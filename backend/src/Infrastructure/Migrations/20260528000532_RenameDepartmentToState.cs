using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameDepartmentToState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Addresses",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "OrderAddresses",
                newName: "State");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Addresses",
                newName: "Department");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "OrderAddresses",
                newName: "Department");
        }
    }
}
