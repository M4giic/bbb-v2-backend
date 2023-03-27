using System.ComponentModel.DataAnnotations;

namespace ApplicationUserService.Service;

public class LoginRequest
{
    // public string Username { get; set; }
    
    [EmailAddress(ErrorMessage ="Invalid email address.")]
    public string Email { get; set; }
    public string Password { get; set; }
}