using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simple_ETL_Project.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trip_datas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    trep_pick_up_date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    trep_drop_off_date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    passenger_count = table.Column<int>(type: "int", nullable: false),
                    trip_distance = table.Column<double>(type: "float", nullable: false),
                    store_and_fwd_flag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pu_location_id = table.Column<int>(type: "int", nullable: false),
                    do_location_id = table.Column<int>(type: "int", nullable: false),
                    fare_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    tip_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_trip_datas", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trip_datas");
        }
    }
}
