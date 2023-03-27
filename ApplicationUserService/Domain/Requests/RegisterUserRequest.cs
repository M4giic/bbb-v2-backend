using System.ComponentModel.DataAnnotations;

namespace ApplicationUserService.Domain.Dto;

public class RegisterUserRequest
{
    public string Username { get; set; } 

    [MaxLength(30, ErrorMessage = "Password can have maximum length of 30 characters")]
    [MinLength(6, ErrorMessage = "Password must have minimum length of 6 characters")]
    [DataType(dataType:DataType.Password)]
    public string Password { get; set; } 

    [EmailAddress(ErrorMessage ="Invalid email address.")]
    public string EmailAddress { get; set; } 

    public DateTime DateOfBirth { get; set; } 
}