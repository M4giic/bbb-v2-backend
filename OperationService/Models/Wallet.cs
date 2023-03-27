using OperationService.Controllers;

namespace OperationService.Models;

public class Wallet
{
    public Guid Id { get; set; }
    public string WalletName { get; set; }
    public ICollection<BasicOperation> WalletOperations { get; set; }
    public ICollection<PlannedOperation> PlannedOperations {get;set;}
}