namespace OperationService.Entities;

public class WalletEntity
{
    public Guid Id { get; set; }
    public string WalletName { get; set; }
    public Guid OwnerUserGuid { get; set; }
    public ICollection<OperationEntity> WalletOperations { get; set; }
    public ICollection<PlannedOperationEntity> PlannedOperations {get;set;}
    public ICollection<TypeFamilyEntity> TypeFamilies { get; set; }
}