using System.ComponentModel.DataAnnotations;

namespace OperationService.Entities;

public class PlannedOperationEntity : BasicOperationEntity
{
    [Key]
    public int Id { get; set; }
    public WalletEntity Wallet { get; set; }
    public Guid WalletId { get; set; }
    public DateTime WhenHappens { get; set; }
    //TODO: this needs to account for unevens months
    //maybe add filed type: Daily (DaysItRepeats matter), Monthly, Yearly, Weekly (Sets days to 7) 
    public int DaysItRepeats { get; set; }
    public DateTime LastHappend { get; set; }
}