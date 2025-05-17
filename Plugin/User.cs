using Microsoft.AspNetCore.Mvc;
using QQBot_Jump.Data;

namespace QQBot_Jump.Plugin;

[ApiController]
[Route("[controller]")]
public class AddUserController : ControllerBase
{
    [HttpGet(Name = "AddUser")]
    public IActionResult Get()
    {
        try
        {
            UserRepository.AddUser("MapleLeaf", "Admin");
            return Ok("User Added");
        }
        catch (Exception e)
        {
            Console.WriteLine($"[SQLite] [ERROR] {e.Message}");
            return StatusCode(418, "I'm a teapot ☕");
        }
    }
}