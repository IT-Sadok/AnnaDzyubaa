using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HealthcareApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorAppointmentTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Спочатку видаляємо старий індекс (зберігаємо твій автозгенерований код)
            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId_AppointmentDate_StartTime",
                table: "Appointments");

            // 2. Створюємо ТИМЧАСОВІ колонки правильного типу для міграції даних
            migrationBuilder.AddColumn<DateTime>(
                name: "TempStartTime",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TempEndTime",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            // 3. ПЕРЕНОСИМО ДАНІ: додаємо дату і час засобами PostgreSQL
            migrationBuilder.Sql(
                @"UPDATE ""Appointments"" 
          SET ""TempStartTime"" = ""AppointmentDate"" + ""StartTime"",
              ""TempEndTime"" = ""AppointmentDate"" + ""EndTime"";");

            // 4. Тепер безпечно видаляємо старі колонки (як і хотів EF Core)
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Appointments");

            // 5. Перейменовуємо тимчасові колонки на фінальні назви
            migrationBuilder.RenameColumn(
                name: "TempStartTime",
                table: "Appointments",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "TempEndTime",
                table: "Appointments",
                newName: "EndTime");

            // 6. Створюємо новий правильний індекс (зберігаємо твій автозгенерований код)
            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Appointments",
                type: "interval",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EndTime",
                table: "Appointments",
                type: "interval",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDate",
                table: "Appointments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId_AppointmentDate_StartTime",
                table: "Appointments",
                columns: new[] { "DoctorId", "AppointmentDate", "StartTime" },
                unique: true);
        }
    }
}
