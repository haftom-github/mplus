using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dd.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diagnostics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnostics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Injuries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Injuries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InjuryExtents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InjuryExtents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabDepartments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabDepartments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MeasurementUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Notation = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasurementUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Affiliates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AffiliateTypeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affiliates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Affiliates_Countries_AffiliateTypeId",
                        column: x => x.AffiliateTypeId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiseaseSubCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DiseaseCategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseaseSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiseaseSubCategories_DiseaseCategories_DiseaseCategoryId",
                        column: x => x.DiseaseCategoryId,
                        principalTable: "DiseaseCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabTests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    LabDepartmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MeasurementUnitId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MaleLowerLimit = table.Column<double>(type: "REAL", nullable: true),
                    MaleNormalValue = table.Column<double>(type: "REAL", nullable: true),
                    MaleUpperLimit = table.Column<double>(type: "REAL", nullable: true),
                    FemaleLowerLimit = table.Column<double>(type: "REAL", nullable: true),
                    FemaleNormalValue = table.Column<double>(type: "REAL", nullable: true),
                    FemaleUpperLimit = table.Column<double>(type: "REAL", nullable: true),
                    ChildLowerLimit = table.Column<double>(type: "REAL", nullable: true),
                    ChildNormalValue = table.Column<double>(type: "REAL", nullable: true),
                    ChildUpperLimit = table.Column<double>(type: "REAL", nullable: true),
                    InfantLowerLimit = table.Column<double>(type: "REAL", nullable: true),
                    InfantNormalValue = table.Column<double>(type: "REAL", nullable: true),
                    InfantUpperLimit = table.Column<double>(type: "REAL", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EffectiveTo = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    LastPrice = table.Column<double>(type: "REAL", nullable: false),
                    LastPriceEffectiveFrom = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    LastPriceEffectiveTo = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabTests_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabTests_LabDepartments_LabDepartmentId",
                        column: x => x.LabDepartmentId,
                        principalTable: "LabDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabTests_MeasurementUnits_MeasurementUnitId",
                        column: x => x.MeasurementUnitId,
                        principalTable: "MeasurementUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DiseaseCode = table.Column<string>(type: "TEXT", nullable: false),
                    SubCategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diseases_DiseaseSubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "DiseaseSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlockedSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    BlocksAllPhysicians = table.Column<bool>(type: "INTEGER", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: true),
                    ContractMedicalPersonnelId = table.Column<Guid>(type: "TEXT", nullable: true),
                    EtMedicalPersonnelId = table.Column<Guid>(type: "TEXT", nullable: true),
                    RecurrenceType = table.Column<int>(type: "INTEGER", nullable: false),
                    RecurrenceInterval = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractMedicalPersonnel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    MiddleName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    ContractStartDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    ContractEndDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ActiveSpecializationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MaxAppointmentsPerDay = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractMedicalPersonnel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractMedicalPersonnel_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EtMedicalPersonnel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true),
                    ActiveSpecializationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RoleId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MaxAppointmentsPerDay = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EtMedicalPersonnel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EtMedicalPersonnel_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ContractMedicalPersonnelId = table.Column<Guid>(type: "TEXT", nullable: true),
                    EtMedicalPersonnelId = table.Column<Guid>(type: "TEXT", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Specializations_ContractMedicalPersonnel_ContractMedicalPersonnelId",
                        column: x => x.ContractMedicalPersonnelId,
                        principalTable: "ContractMedicalPersonnel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Specializations_EtMedicalPersonnel_EtMedicalPersonnelId",
                        column: x => x.EtMedicalPersonnelId,
                        principalTable: "EtMedicalPersonnel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ContractMedicalPersonnelId = table.Column<Guid>(type: "TEXT", nullable: true),
                    EtMedicalPersonnelId = table.Column<Guid>(type: "TEXT", nullable: true),
                    RecurrenceType = table.Column<int>(type: "INTEGER", nullable: false),
                    RecurrenceInterval = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkSchedule_ContractMedicalPersonnel_ContractMedicalPersonnelId",
                        column: x => x.ContractMedicalPersonnelId,
                        principalTable: "ContractMedicalPersonnel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkSchedule_EtMedicalPersonnel_EtMedicalPersonnelId",
                        column: x => x.EtMedicalPersonnelId,
                        principalTable: "EtMedicalPersonnel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Procedures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    UpdatedBy = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CurrencyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Price = table.Column<double>(type: "REAL", nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EffectiveTo = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    LastPrice = table.Column<double>(type: "REAL", nullable: false),
                    LastPriceEffectiveFrom = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    LastPriceEffectiveTo = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Procedures_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Procedures_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Affiliates_AffiliateTypeId",
                table: "Affiliates",
                column: "AffiliateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedSchedule_ContractMedicalPersonnelId",
                table: "BlockedSchedule",
                column: "ContractMedicalPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedSchedule_EtMedicalPersonnelId",
                table: "BlockedSchedule",
                column: "EtMedicalPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractMedicalPersonnel_ActiveSpecializationId",
                table: "ContractMedicalPersonnel",
                column: "ActiveSpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractMedicalPersonnel_RoleId",
                table: "ContractMedicalPersonnel",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_SubCategoryId",
                table: "Diseases",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DiseaseSubCategories_DiseaseCategoryId",
                table: "DiseaseSubCategories",
                column: "DiseaseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_EtMedicalPersonnel_ActiveSpecializationId",
                table: "EtMedicalPersonnel",
                column: "ActiveSpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_EtMedicalPersonnel_RoleId",
                table: "EtMedicalPersonnel",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_CurrencyId",
                table: "LabTests",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_LabDepartmentId",
                table: "LabTests",
                column: "LabDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_LabTests_MeasurementUnitId",
                table: "LabTests",
                column: "MeasurementUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_CurrencyId",
                table: "Procedures",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_SpecializationId",
                table: "Procedures",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_ContractMedicalPersonnelId",
                table: "Specializations",
                column: "ContractMedicalPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_EtMedicalPersonnelId",
                table: "Specializations",
                column: "EtMedicalPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedule_ContractMedicalPersonnelId",
                table: "WorkSchedule",
                column: "ContractMedicalPersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkSchedule_EtMedicalPersonnelId",
                table: "WorkSchedule",
                column: "EtMedicalPersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedSchedule_ContractMedicalPersonnel_ContractMedicalPersonnelId",
                table: "BlockedSchedule",
                column: "ContractMedicalPersonnelId",
                principalTable: "ContractMedicalPersonnel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockedSchedule_EtMedicalPersonnel_EtMedicalPersonnelId",
                table: "BlockedSchedule",
                column: "EtMedicalPersonnelId",
                principalTable: "EtMedicalPersonnel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractMedicalPersonnel_Specializations_ActiveSpecializationId",
                table: "ContractMedicalPersonnel",
                column: "ActiveSpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EtMedicalPersonnel_Specializations_ActiveSpecializationId",
                table: "EtMedicalPersonnel",
                column: "ActiveSpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_ContractMedicalPersonnel_ContractMedicalPersonnelId",
                table: "Specializations");

            migrationBuilder.DropForeignKey(
                name: "FK_Specializations_EtMedicalPersonnel_EtMedicalPersonnelId",
                table: "Specializations");

            migrationBuilder.DropTable(
                name: "Affiliates");

            migrationBuilder.DropTable(
                name: "BlockedSchedule");

            migrationBuilder.DropTable(
                name: "Diagnostics");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Injuries");

            migrationBuilder.DropTable(
                name: "InjuryExtents");

            migrationBuilder.DropTable(
                name: "LabTests");

            migrationBuilder.DropTable(
                name: "Procedures");

            migrationBuilder.DropTable(
                name: "WorkSchedule");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "DiseaseSubCategories");

            migrationBuilder.DropTable(
                name: "LabDepartments");

            migrationBuilder.DropTable(
                name: "MeasurementUnits");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "DiseaseCategories");

            migrationBuilder.DropTable(
                name: "ContractMedicalPersonnel");

            migrationBuilder.DropTable(
                name: "EtMedicalPersonnel");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
