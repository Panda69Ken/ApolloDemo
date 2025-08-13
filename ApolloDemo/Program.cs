using ApolloDemo.Core;
using ApolloDemo.Services;
using Com.Ctrip.Framework.Apollo;

var builder = WebApplication.CreateBuilder(args);

var apollo = builder.Configuration.GetSection("Apollo");

builder.Host.ConfigureAppConfiguration((context, builder) =>
{
    //k8s模式
    if (context.HostingEnvironment.IsDevelopment())
    {
        builder.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json");
    }
    else
    {
        builder.AddJsonFile("appsettings.json");
    }

    //环境变量模式
    //builder.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("RUN_ENV")}.json");

    //apollo 配置
    var config = builder.Build().GetSection("Apollo");
    var apollo = builder.AddApollo(config);
    foreach (var item in config.GetSection("namespaces").GetChildren())
    {
        apollo.AddNamespace(item.Get<string>());
        //json格式配置
        //.AddNamespace(item.Get<string>(), ConfigFileFormat.Json);
    }
});

builder.Services.AddSingleton<IConfigService, ConfigService>();

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
