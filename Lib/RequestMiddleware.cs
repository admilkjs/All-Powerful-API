using QQBot_Jump.Middleware;

namespace QQBot_Jump.Lib;

public static class RequestMiddleware
{
    // 请求日志
    public static void UseRequestLogging(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<RequestLoggingMiddleware>();
    }
    
    // 权限判断
    public static void UsePermissionJudgment(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<PermissionJudgmentMiddleware>();
    }
    
    // Redis记录
    public static void UseRedisRecords(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<RedisRecordsMiddleware>();
    }
}