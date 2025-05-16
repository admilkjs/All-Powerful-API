using Microsoft.AspNetCore.Mvc;

namespace QQBot_Jump.Plugin
{
    [ApiController]
    [Route("/")]
    public class Root: ControllerBase
    {
        [HttpGet(Name = "RootPath")]
        public IActionResult Get()
        {
            var data = new { Message = "Hello", Time = DateTime.Now };
            return Ok(data);
        }
    }

    [ApiController]
    [Route("/favicon.ico")]
    public class Favicon : ControllerBase
    {
        [HttpGet(Name="Favicon")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}