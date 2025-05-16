using Microsoft.AspNetCore.Mvc;
using QQBot_Jump.Lib;

namespace QQBot_Jump.Plugin
{
    [ApiController]
    [Route("[controller]")]
    public class RedisTestController : ControllerBase
    {
        private readonly Redis _redis;

        public RedisTestController(Redis redis)
        {
            _redis = redis;
        }

        [HttpGet("set")]
        public async Task<IActionResult> SetValue()
        {
            await _redis.SetStringAsync("test:name", "MapleLeaf", TimeSpan.FromMinutes(10));
            return Ok("写入成功！");
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetValue()
        {
            var value = await _redis.GetStringAsync("test:name");
            return Ok(new { value });
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete()
        {
            var result = await _redis.DeleteKeyAsync("test:name");
            return Ok(new { deleted = result });
        }
    }
}