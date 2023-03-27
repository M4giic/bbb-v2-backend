using OperationService.Models;

namespace OperationService.Request.Request.Operation;

public class UpdateOperationRequest : BasicOperation
{
    public int Id { get; set; }
    public DateTime DateWhenHappened { get; set; }
}