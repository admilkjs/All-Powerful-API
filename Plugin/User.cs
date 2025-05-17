using Microsoft.AspNetCore.Mvc;
using QQBot_Jump.Data;

namespace QQBot_Jump.Plugin;

[ApiController]
[Route("[controller]")]
public class AddUserController : ControllerBase
{
    private readonly UserRepository _userRepo;

    public AddUserController()
    {
        UserRepository userRepo = new();
        _userRepo = userRepo;
    }

    [HttpGet(Name = "AddUser")]
    public IActionResult Get()
    {
        try
        {
            _userRepo.AddUser("MapleLeaf", "114514", "Admin");
            return Ok("User Added");
        }
        catch (Exception e)
        {
            // 建议使用 ILogger 而不是 Console
            Console.WriteLine($"[SQLite] [ERROR] {e.Message}");
            return StatusCode(418, "I'm a teapot ☕");
        }
    }
}