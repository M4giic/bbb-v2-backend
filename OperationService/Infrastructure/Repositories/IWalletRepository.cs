using OperationService.Entities;

namespace OperationService.Infrastructure.Repositories;

public interface IWalletRepository
{
    Task<Guid> AddWalletAsync(WalletEntity walletEntity);
    Task<WalletEntity?> GetWalletByGuid(Guid walletGuid);
    Task<List<WalletEntity?>> GetWalletsByOwnerGuid(Guid ownerGuid);
    Task DeleteWalletByGuid(Guid walletGuid, bool force);
    Task<WalletEntity?> UpdateWallet(WalletEntity walletEntity);
}