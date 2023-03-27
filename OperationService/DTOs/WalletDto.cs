
namespace OperationService.DTOs;

public class WalletDto
{
    public string WalletName { get; set; }
    public Guid OwnerUserGuid { get; set; }
    public Guid Id { get; set; }
}