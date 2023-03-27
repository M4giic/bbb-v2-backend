using OperationService.DTOs;

namespace OperationService.Application;

public interface IWalletService
{
    Task<Guid> AddWallet(WalletDto walletDto);
    Task<WalletDto> GetWalletByGuid(Guid walletGuid);
    Task<List<WalletDto>> GetWalletsByOwnerGuid(Guid ownerGuid);
    Task<WalletDto>  UpdateWallet(WalletDto walletDto);
    Task DeleteWallet(Guid walletGuid, bool force);
}