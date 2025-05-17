namespace QQBot_Jump.Models;

public class UserDto
{
    public string? UserName { get; set; }
    public string? Token { get; set; }
    
    // 限定一下Role只能是Admin或者User
    private string? _role;
    public string? Role
    {
        get => _role;
        set
        {
            if (value != "Admin" && value != "User")
            {
                throw new ArgumentException("Role must be 'Admin' or 'User'");
            }
            _role = value;
        }
    }
}
