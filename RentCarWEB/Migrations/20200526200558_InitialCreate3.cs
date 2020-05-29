using Microsoft.EntityFrameworkCore.Migrations;

namespace RentCarWEB.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Coupons_CouponCode",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CouponCode",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CouponCode",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "CouponId",
                table: "Reservations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CouponId",
                table: "Reservations",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Coupons_CouponId",
                table: "Reservations",
                column: "CouponId",
                principalTable: "Coupons",
                principalColumn: "CouponCode",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Coupons_CouponId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CouponId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "CouponId",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CouponCode",
                table: "Reservations",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CouponCode",
                table: "Reservations",
                column: "CouponCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Coupons_CouponCode",
                table: "Reservations",
                column: "CouponCode",
                principalTable: "Coupons",
                principalColumn: "CouponCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
