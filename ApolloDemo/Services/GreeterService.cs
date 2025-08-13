using ApolloDemo.Core;
using ApolloDemo.Model;
using Grpc.Core;

namespace ApolloDemo.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IConfigService _config;
        private readonly IConfiguration _configuration;

        public GreeterService(ILogger<GreeterService> logger, IConfigService config, IConfiguration configuration)
        {
            _logger = logger;
            _config = config;
            _configuration = configuration;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            //自定义接口使用
            var mysql = _config.MySqlConfig;
            var redis = _config.RedisConfig;
            var mongo = _config.MongoConfigs;
            var viserion = _config.Viserion;

            //扩展IConfiguration直接使用
            var mysql1 = _configuration.GetApolloValue<MySqlConnectionConfig>("DbConnect");
            var redis1 = _configuration.GetApolloValue<RedisConfig>("RedisSettings");
            var mongo1 = _configuration.GetApolloValue<List<MongoDbConfig>>("Mongos");
            var viserion1 = _configuration.GetApolloValue<string>("TraceDebug");

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
