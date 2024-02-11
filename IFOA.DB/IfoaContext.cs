using IFOA.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace IFOA.DB;

public class IfoaContext: DbContext
{
    public const string Schema = "ifoa";
    public const string MigrationHistoryTable = "__IfoaMigration";

    public IfoaContext()
    {
        
    }
    
    public IfoaContext(DbContextOptions<IfoaContext> options) : base(options)
    {
           
    }
    
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<CandidateCertification> CandidateCertifications { get; set; }
    public DbSet<CandidateProject> CandidateProjects { get; set; }
    public DbSet<CandidatePreferredJobFunction> CandidatePreferredJobFunctions { get; set; }
    public DbSet<CandidatePreferredLocation> CandidatePreferredLocations { get; set; }
    public DbSet<CandidateSpokenLanguage> CandidateSpokenLanguages { get; set; }
    public DbSet<JobFunction> JobFunctions { get; set; }
    public DbSet<CandidateExperience> CandidateExperience { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(x => x.MigrationsHistoryTable(MigrationHistoryTable, Schema));
    }
}