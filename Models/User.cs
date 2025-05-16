namespace QQBot_Jump.Models;

// TODO: 实现用户系统
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