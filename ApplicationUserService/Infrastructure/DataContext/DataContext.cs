using BBBv2.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BBBv2.Infrastructure.DataContext;

public class DataContext : DbContext
{
    public DataContext() 
    {
        
    }
    public DataContext(DbContextOptions<DataContext> options) : base (options)
    {

    }
    
    public DbSet<AccountEntity> _accounts { get; set; }
    
    public DbSet<SingleSignOnTokenEntity> _singleSignOnTokens { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountEntity>()
            .Property(b => b.AccountStatus)
            .HasDefaultValue(AccountStatus.Registered);
        
    }
    
}

