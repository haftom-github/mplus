using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dd.Api.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeletesAddedToAuditable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Specializations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Specializations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Specializations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "Specializations",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Specializations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Roles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Roles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Roles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "Roles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Roles",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Procedures",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Procedures",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Procedures",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "Procedures",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Procedures",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Privilege",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Privilege",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Privilege",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "Privilege",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Privilege",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PersonnelWorkSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PersonnelWorkSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PersonnelWorkSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "PersonnelWorkSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "PersonnelWorkSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PersonnelBlockedSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "PersonnelBlockedSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PersonnelBlockedSchedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "PersonnelBlockedSchedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "PersonnelBlockedSchedules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MedicalPersonnel",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "MedicalPersonnel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "MedicalPersonnel",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "MedicalPersonnel",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "MedicalPersonnel",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MeasurementUnits",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "MeasurementUnits",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "MeasurementUnits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "MeasurementUnits",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "MeasurementUnits",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LabTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "LabTests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LabTests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "LabTests",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "LabTests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "LabDepartments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "LabDepartments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LabDepartments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "LabDepartments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "LabDepartments",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "InjuryExtents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "InjuryExtents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "InjuryExtents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "InjuryExtents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "InjuryExtents",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Injuries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Injuries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Injuries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "Injuries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Injuries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DiseaseSubCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "DiseaseSubCategories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "DiseaseSubCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "DiseaseSubCategories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "DiseaseSubCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Diseases",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Diseases",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Diseases",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "Diseases",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Diseases",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DiseaseCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "DiseaseCategories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "DiseaseCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "DiseaseCategories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "DiseaseCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Diagnostics",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Diagnostics",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Diagnostics",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "Diagnostics",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Diagnostics",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Currencies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Currencies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Currencies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "Currencies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Currencies",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Countries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Countries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Countries",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "Countries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Countries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Affiliates",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Affiliates",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Affiliates",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusChangedAt",
                table: "Affiliates",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Affiliates",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Specializations");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Privilege");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Privilege");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Privilege");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "Privilege");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Privilege");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PersonnelWorkSchedules");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PersonnelWorkSchedules");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PersonnelWorkSchedules");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "PersonnelWorkSchedules");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PersonnelWorkSchedules");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PersonnelBlockedSchedules");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "PersonnelBlockedSchedules");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PersonnelBlockedSchedules");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "PersonnelBlockedSchedules");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PersonnelBlockedSchedules");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MedicalPersonnel");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "MedicalPersonnel");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MedicalPersonnel");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "MedicalPersonnel");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MedicalPersonnel");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MeasurementUnits");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "MeasurementUnits");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MeasurementUnits");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "MeasurementUnits");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "MeasurementUnits");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LabTests");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "LabDepartments");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "LabDepartments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LabDepartments");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "LabDepartments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "LabDepartments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "InjuryExtents");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "InjuryExtents");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "InjuryExtents");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "InjuryExtents");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "InjuryExtents");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Injuries");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Injuries");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Injuries");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "Injuries");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Injuries");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DiseaseSubCategories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "DiseaseSubCategories");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DiseaseSubCategories");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "DiseaseSubCategories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DiseaseSubCategories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DiseaseCategories");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "DiseaseCategories");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "DiseaseCategories");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "DiseaseCategories");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DiseaseCategories");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Diagnostics");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Diagnostics");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Diagnostics");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "Diagnostics");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Diagnostics");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Affiliates");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Affiliates");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Affiliates");

            migrationBuilder.DropColumn(
                name: "StatusChangedAt",
                table: "Affiliates");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Affiliates");
        }
    }
}
