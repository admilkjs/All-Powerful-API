using QQBot_Jump.Lib;
    
namespace QQBot_Jump
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // 将服务添加到容器中.
            builder.Services.AddControllers();
            
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            var app = builder.Build();

            // 配置 HTTP 请求管道.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseAuthorization();

            // 请求日志中间件
            app.UseRequestLogging();
            // 权限判断中间件
            app.UsePermissionJudgment();
            
            app.MapControllers();
            app.Run();
        }
    }
}
