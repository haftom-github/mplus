using Dd.Api.Shared.Domain;
using Dd.Api.Shared.Domain.Entities;
using Dd.Api.Shared.Domain.MasterData;
using Microsoft.EntityFrameworkCore;

namespace Dd.Api.Shared.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options) {
    // disease entities
    public DbSet<Disease>? Diseases { get; set; }
    public DbSet<DiseaseCategory>? DiseaseCategories { get; set; }
    public DbSet<DiseaseSubCategory>? DiseaseSubCategories { get; set; }
    
    
    // medical personnel entities
    public DbSet<MedicalPersonnel>? MedicalPersonnel { get; set; }
    public DbSet<EtMedicalPersonnel>? EtMedicalPersonnel { get; set; }
    public DbSet<ContractMedicalPersonnel>? ContractMedicalPersonnel { get; set; }
    public DbSet<Specialization>? Specializations { get; set; }
    public DbSet<Role>? Roles { get; set; }
    
    // diagnostics and procedures
    public DbSet<Diagnostic>? Diagnostics { get; set; }
    public DbSet<LabTest>? LabTests { get; set; }
    public DbSet<LabDepartment>? LabDepartments { get; set; }
    
    public DbSet<Procedure>? Procedures { get; set; }

    // measurement units and currencies
    public DbSet<MeasurementUnit>? MeasurementUnits { get; set; }
    public DbSet<Currency>? Currencies { get; set; }
    
    // injury entities
    public DbSet<Injury>? Injuries { get; set; }
    public DbSet<InjuryExtent>? InjuryExtents { get; set; }
    
    // other
    public DbSet<AffiliateType>? Countries { get; set; }
    public DbSet<Affiliate>? Affiliates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
        ConfigureEntities(modelBuilder);
        modelBuilder.Entity<Injury>()
            .HasQueryFilter(i => i.Status != RecordStatus.Deleted);

        modelBuilder.Entity<MedicalPersonnel>()
            .HasMany(mp => mp.Specializations)
            .WithMany()
            .UsingEntity<MedicalPersonnelSpecializations>(
                j => j.HasOne(mp => mp.Specialization).WithMany(),
                j => j.HasOne(mp => mp.MedicalPersonnel).WithMany(),
                j => j.HasKey(mp => new { mp.MedicalPersonnelId, mp.SpecializationId }
                )
            );
        
        modelBuilder.Entity<MedicalPersonnel>()
            .HasMany(mp => mp.WorkSchedules)
            .WithMany()
            .UsingEntity<PersonnelWorkSchedules>(
                j => j.HasOne(ws => ws.WorkSchedule).WithMany(),
                j => j.HasOne(ws => ws.MedicalPersonnel).WithMany(),
                j => j.HasKey(ws => new { ws.MedicalPersonnelId, ws.WorkScheduleId }
                )
            );
        
        modelBuilder.Entity<MedicalPersonnel>()
            .HasMany(mp => mp.BlockedSchedules)
            .WithMany()
            .UsingEntity<PersonnelBlockedSchedules>(
                j => j.HasOne(bs => bs.BlockedSchedule).WithMany(),
                j => j.HasOne(bs => bs.MedicalPersonnel).WithMany(),
                j => j.HasKey(bs => new { bs.MedicalPersonnelId, bs.BlockedScheduleId }
                )
            );
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        ChangeTracker.DetectChanges();

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>()) {
            switch (entry.State) {
                case EntityState.Added:
                    entry.Entity.Status = RecordStatus.Active;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.Status = RecordStatus.Deleted;
                    entry.Entity.StatusChangedAt = DateTime.UtcNow;
                    break;
                
                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    break;
            }
        }
        
        return await base.SaveChangesAsync(cancellationToken);
    }
    
    private void ConfigureEntities(ModelBuilder modelBuilder) {
        
        var entities = modelBuilder.Model.GetEntityTypes()
            .Where(t => t.ClrType.IsClass);
        
        foreach (var entityType in entities) {
            if (typeof(IHasName).IsAssignableFrom(entityType.ClrType)) {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(IHasName.Name))
                    .HasMaxLength(100);
            }
            
            if (typeof(IHasDescription).IsAssignableFrom(entityType.ClrType)) {
                modelBuilder.Entity(entityType.ClrType)
                    .Property(nameof(IHasDescription.Description))
                    .HasMaxLength(500);
            }
        }
    }
}