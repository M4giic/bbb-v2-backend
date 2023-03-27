namespace ApplicationUserService.Service;


//UserDto is used to login 
public class UserDto
{
    public string Username { get; set; }
    public string Token { get; set; }
    public Guid Id { get; set; }
}