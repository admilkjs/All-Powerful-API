namespace QQBot_Jump.Middleware;

public class PermissionJudgmentMiddleware
{
    private readonly RequestDelegate  _next;
    private readonly ILogger<PermissionJudgmentMiddleware> _logger;
    
    public PermissionJudgmentMiddleware(RequestDelegate next, ILogger<PermissionJudgmentMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context)
    { 
        PermissionJudgment(context.Connection.RemoteIpAddress.ToString(),context);
        await _next(context);
    }

    public bool PermissionJudgment(string? ip,HttpContext context)
    {
        if (ip == null)
        {
            context.Abort(); // ? IP都没有,给我趋势
        }
        // TODO: 权限判断逻辑
        bool result = true;
        if (result)
        {
            // 允许访问
            _logger.LogDebug("[{time}] [IP]{ip} [权限] 允许访问",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),ip);
            return true;
        }
        else
        {
            // 禁止访问
            context.Abort(); // 立即断开 socket，客户端会收到连接重置
            _logger.LogWarning("[{time}] [IP]{ip} [权限] 禁止访问",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),ip);
            return false;
        }
    }
}