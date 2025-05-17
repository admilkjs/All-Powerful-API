namespace QQBot_Jump.Middleware;

/// <summary>
/// 封装一些HttpContext的扩展方法(在context塞一些奇奇怪怪的方法)
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// 获取客户端IP(CloudFlare Zero Trust)
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string? GetClientIp(this HttpContext context)
    {
        return context.Request.Headers["Cf-Connecting-Ip"].FirstOrDefault()
               ?? context.Connection.RemoteIpAddress?.ToString();
    }
}
