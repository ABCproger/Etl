using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simple_ETL_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexesToTripDatasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "store_and_fwd_flag",
                table: "trip_datas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "ix_pickup_dropoff_datetime",
                table: "trip_datas",
                columns: new[] { "trep_pick_up_date_time", "trep_drop_off_date_time" });

            migrationBuilder.CreateIndex(
                name: "ix_pulocation_id",
                table: "trip_datas",
                column: "pu_location_id");

            migrationBuilder.CreateIndex(
                name: "ix_trip_distance",
                table: "trip_datas",
                column: "trip_distance");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_pickup_dropoff_datetime",
                table: "trip_datas");

            migrationBuilder.DropIndex(
                name: "ix_pulocation_id",
                table: "trip_datas");

            migrationBuilder.DropIndex(
                name: "ix_trip_distance",
                table: "trip_datas");

            migrationBuilder.AlterColumn<string>(
                name: "store_and_fwd_flag",
                table: "trip_datas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
