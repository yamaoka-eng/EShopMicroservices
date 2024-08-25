var builder = WebApplication.CreateBuilder(args);

// 注册 Carter 作为处理 HTTP 端点的服务。
builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    // 注册 MediatR，用于在处理程序之间中介请求和响应。
    // 从当前程序集（Program 所在的程序集）注册所有 MediatR 相关的服务。
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddMarten(opts =>
{
    // 配置 Marten 作为文档数据库。
    opts.Connection(builder.Configuration.GetConnectionString("Database")!); // 获取并设置数据库连接字符串。
}).UseLightweightSessions(); // 使用轻量级会话与 Marten 文档存储交互。

var app = builder.Build();

app.MapCarter(); // 将 Carter 注册到应用程序中，以便它可以处理 HTTP 端点

app.Run();
