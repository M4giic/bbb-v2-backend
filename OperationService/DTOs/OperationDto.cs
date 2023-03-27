
using System.ComponentModel.DataAnnotations;
using OperationService.Entities;

namespace OperationService.DTOs;

public class OperationDto : BasicOperationDto
{ 
    [Key]
    public int Id { get; set; }
    public DateTime DateWhenHappened { get; set; }
    public Guid WalletId { get; set; }
    public int PriorityLevel { get; set; }
}