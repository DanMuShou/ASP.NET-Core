using DanMuPan.API;
using Serilog;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    // 根据环境变量加载对应的配置文件，如果未设置环境变量则默认为Production环境
    .AddJsonFile(
        $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json",
        optional: true
    )
    .Build();

// 配置Serilog日志记录器，从配置文件中读取日志配置
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

try
{
    // 记录应用程序启动日志
    Log.Information("Start Web Log");

    // 创建主机构建器，配置Serilog和WebHost
    var hostBuilder = Host.CreateDefaultBuilder(args)
        .UseSerilog()
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });

    // 构建并运行主机
    hostBuilder.Build().Run();
}
catch (Exception ex)
{
    // 记录应用程序异常终止的致命错误
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    // 关闭并刷新日志记录器，确保所有日志都被写入
    Log.CloseAndFlush();
}
