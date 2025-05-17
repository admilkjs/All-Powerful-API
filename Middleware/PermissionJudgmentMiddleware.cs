namespace QQBot_Jump.Middleware;

public class PermissionJudgmentMiddleware(RequestDelegate next, ILogger<PermissionJudgmentMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var ip = context.GetClientIp();
        PermissionJudgment(ip, context);
        await next(context);
    }

    public bool PermissionJudgment(string? ip, HttpContext context)
    {
        var url = context.Request.Path.ToString();
        // TODO: 权限判断逻辑
        bool result = true;
        if (result)
        {
            // 允许访问
            logger.LogDebug("[{time}] [URL] {url} [IP]{ip} [权限] 允许访问", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                url, ip);
            return true;
        }
        else
        {
            // 禁止访问
            context.Abort(); // 立即断开 socket，客户端会收到连接重置
            logger.LogWarning("[{time}] [URL] {url} [IP]{ip} [权限] 禁止访问", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                url, ip);
            return false;
        }
    }
}