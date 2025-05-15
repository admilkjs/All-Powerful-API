namespace QQBot_Jump.Models;

public class User
{
    public string? UserName { get; set; }
    public string? PassWord { get; set; } 
}

public class Login
{
    public string? UserName { get; set; }
    public string? Token { get; set; }
}