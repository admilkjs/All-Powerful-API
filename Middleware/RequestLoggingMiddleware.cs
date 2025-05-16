namespace QQBot_Jump.Middleware;

public class RequestLoggingMiddleware
{
    // _next  下一个中间件
    private readonly RequestDelegate _next;
    // _logger 日志记录器
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    /// <summary>
    /// 传入下一个中间件和日志记录器,  用于记录请求和响应信息
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //var ip = context.Connection.RemoteIpAddress
        // Test CF Zero Trust 
        var ip = context.Request.Headers["Cf-Connecting-Ip"];
        // 记录请求信息
        _logger.LogInformation("[{time}][Method] {method} [URL] {url} [IP] {ip}",
            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            context.Request.Method,
            context.Request.Path,
            ip
            );

        // 调用管道中的下一个中间件
        await _next(context);
    }
}