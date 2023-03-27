using System.ComponentModel.DataAnnotations;

namespace OperationService.Entities;

public class UserEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime DateOfBirth { get; set; }
    //public ICollection<Wallet> Wallets { get; set; } //user will only refer to wallet by external key
}