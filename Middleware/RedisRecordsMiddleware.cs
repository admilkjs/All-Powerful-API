using QQBot_Jump.Lib;

namespace QQBot_Jump.Middleware;

public class RedisRecordsMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RedisRecordsMiddleware> _logger;
    private readonly Redis _redis;

    public RedisRecordsMiddleware(RequestDelegate next, ILogger<RedisRecordsMiddleware> logger, Redis redis)
    {
        _next = next;
        _logger = logger;
        _redis = redis;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // var ip = context.Connection.RemoteIpAddress?.ToString();
            // Test CF Zero Trust 
            var ip = context.Request.Headers["Cf-Connecting-Ip"];

            if (!string.IsNullOrWhiteSpace(ip))
            {
                string redisKey = "api:Statistics:ip";
                var value = await _redis.GetHashAsync(redisKey, ip);

                if (value == null)
                {
                    await _redis.SetHashAsync(redisKey, ip, "1");
                }
                else
                {
                    if (int.TryParse(value, out int count))
                    {
                        await _redis.SetHashAsync(redisKey, ip, (count + 1).ToString());
                    }
                    else
                    {
                        // 非法值，重置
                        await _redis.SetHashAsync(redisKey, ip, "1");
                    }
                }
            }
        }
        catch (Exception e)
        {
            _logger.LogError("[{DateTime:yyyy-MM-dd HH:mm:ss}][Redis][ERROR] {EMessage}", DateTime.Now, e.Message);
            throw;
        }

        await _next(context);
    }
}