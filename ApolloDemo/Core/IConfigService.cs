using ApolloDemo.Model;
using Newtonsoft.Json;

namespace ApolloDemo.Core
{
    public interface IConfigService
    {
        MySqlConnectionConfig MySqlConfig { get; }
        RedisConfig RedisConfig { get; }
        List<MongoDbConfig> MongoConfigs { get; }
        string Viserion { get; }
    }

    public class ConfigService : IConfigService
    {
        private ILogger<ConfigService> _logger;
        private IConfiguration _configuration;

        public ConfigService(ILogger<ConfigService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        private T GetSettings<T>(string name)
        {
            var value = _configuration.GetSection(name).Value;
            if (value == null)
            {
                _logger.LogWarning($"配置{name}为Null");
                return default;
            }
            return JsonConvert.DeserializeObject<T>(value);
        }

        private string GetSetting(string name)
        {
            var value = _configuration.GetSection(name).Value;
            if (value == null)
            {
                _logger.LogWarning($"配置{name}为Null");
                return string.Empty;
            }
            return value;
        }

        public MySqlConnectionConfig MySqlConfig => GetSettings<MySqlConnectionConfig>("DbConnect");

        public RedisConfig RedisConfig => GetSettings<RedisConfig>("RedisSettings");

        public List<MongoDbConfig> MongoConfigs => GetSettings<List<MongoDbConfig>>("Mongos");

        public string Viserion => GetSetting("TraceDebug");
    }




}
