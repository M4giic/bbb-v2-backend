namespace OperationService.Entities;

public class BasicOperationEntity
{
    
    public decimal Value {get;set;}
    public string Currency {get;set;}
    public TypeEntity Type { get; set; }
    public int DesireLevel { get; set; }
}