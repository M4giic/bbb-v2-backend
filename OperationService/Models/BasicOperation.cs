namespace OperationService.Models;

public class BasicOperation
{
    public decimal Value {get;set;}
    public string Currency {get;set;}
    public int Type {get;set;}
    public int DesireLevel { get; set; }
}