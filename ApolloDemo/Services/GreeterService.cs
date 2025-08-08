using ApolloDemo.Core;
using Grpc.Core;

namespace ApolloDemo.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        private readonly IConfigService _config;

        public GreeterService(ILogger<GreeterService> logger, IConfigService config)
        {
            _logger = logger;
            _config = config;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            var mysql = _config.MySqlConfig;

            var redis = _config.RedisConfig;

            var mongo = _config.MongoConfigs;

            var viserion = _config.Viserion;

            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
