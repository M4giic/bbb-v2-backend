using System.ComponentModel.DataAnnotations;

namespace ApplicationUserService.Service;

//Account Dto is used within infrastructure to add new entity to DB
public class AccountDto
{
    public Guid Id;
    public string Username;
    
    [DataType(dataType:DataType.Password)]
    public string Password;

    [EmailAddress(ErrorMessage ="Invalid email address.")]
    public string EmailAddress;

    public DateTime DateOfBirth;

}