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

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        var entries = this.ChangeTracker
            .Entries<IAuditable>()
            .Where(e => e.State is EntityState.Modified or EntityState.Added);

        foreach (var entry in entries) {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        
    }
}