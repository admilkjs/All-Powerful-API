using Microsoft.AspNetCore.Mvc;
using QQBot_Jump.Data;

namespace QQBot_Jump.Plugin;

[ApiController]
[Route("[controller]")]
public class RoleTestController : ControllerBase
{
    [HttpGet(Name = "RoleTest")]
    public IActionResult Get([FromQuery] string name)
    {
        var res = UserRepository.GetUserRole(name);
        return Ok(res);
    }
}