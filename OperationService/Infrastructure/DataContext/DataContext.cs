using Microsoft.EntityFrameworkCore;
using OperationService.Entities;

namespace OperationService.Infrastructure.DataContext;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base (options)
    {

    }
    
    public DbSet<WalletEntity> _walletEntities { get; set; }
    
    public DbSet<OperationEntity> _operationEntities { get; set; }
    
    public DbSet<TypeFamilyEntity> _typeFamilyEntities { get; set; }
}