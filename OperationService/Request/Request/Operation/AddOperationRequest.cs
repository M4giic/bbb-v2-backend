using OperationService.Models;

namespace OperationService.Request.Request.Operation;

public class AddOperationRequest : BasicOperation
{
    public DateTime DateWhenHappened { get; set; }
    public Guid WalletId { get; set; }
}