using BBBv2.Entities;
using Microsoft.EntityFrameworkCore;

namespace BBBv2.Infrastructure.DataContext;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base (options)
    {

    }
    
    public DbSet<TagsEntity> _walletEntities { get; set; }
    
    public DbSet<OperationEntity> _operationEntities { get; set; }
}