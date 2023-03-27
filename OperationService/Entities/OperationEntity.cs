using System.ComponentModel.DataAnnotations;

namespace OperationService.Entities;

public class OperationEntity : BasicOperationEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime DateWhenHappened { get; set; }
    public WalletEntity Wallet { get; set; }
    public Guid WalletId { get; set; }
}