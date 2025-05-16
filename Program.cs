using QQBot_Jump.Lib;
using StackExchange.Redis;
namespace QQBot_Jump
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // 将服务添加到容器中.
            builder.Services.AddControllers();
            // Redis 服务
            var redisEnabled = builder.Configuration.GetValue<bool>("Redis:Enabled");
            var redisConnStr = builder.Configuration.GetValue<string>("Redis:ConnectionString");
            if (redisEnabled)
            {
                try
                {
                    builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
                        ConnectionMultiplexer.Connect(redisConnStr?? "localhost:6379"));
                    builder.Services.AddScoped<Redis>();
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Redis 连接成功");
                }
                catch  (Exception e)
                {
                    Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Redis连接失败:" + e.Message);
                }
            }
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
            // TODO: Redis统计中间件
            
            app.MapControllers();
            app.Run();
        }
    }
}
