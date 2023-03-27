using Microsoft.EntityFrameworkCore;
using OperationService.Entities;

namespace OperationService.Infrastructure.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly DataContext.DataContext _dataContext;

    public WalletRepository(DataContext.DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<Guid> AddWalletAsync(WalletEntity walletEntity)
    {
        await _dataContext._walletEntities.AddAsync(walletEntity);

        await _dataContext.SaveChangesAsync();

        return walletEntity.Id;
    }

    public async Task<WalletEntity?> UpdateWallet(WalletEntity walletEntity)
    {
        var wallet = await _dataContext._walletEntities.FindAsync(walletEntity.Id);
        wallet.WalletName = walletEntity.WalletName;

        await _dataContext.SaveChangesAsync();

        return wallet;
    }

    public async Task<WalletEntity?> GetWalletByGuid(Guid walletGuid)
    {
        return await _dataContext._walletEntities.FindAsync(walletGuid);
    }

    public async Task<List<WalletEntity?>> GetWalletsByOwnerGuid(Guid ownerGuid)
    {
        return await _dataContext._walletEntities.Where(x => x.OwnerUserGuid == ownerGuid)
                .ToListAsync();
    }

    public async Task DeleteWalletByGuid(Guid walletGuid, bool force)
    {
        var walletEntity = await _dataContext._walletEntities.FindAsync(walletGuid);
        
        _dataContext._walletEntities.Remove(walletEntity);
        
        await _dataContext.SaveChangesAsync();
    }
}
