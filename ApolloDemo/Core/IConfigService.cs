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
        private IConfiguration _configuration;

        public ConfigService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MySqlConnectionConfig MySqlConfig => _configuration.GetApolloValue<MySqlConnectionConfig>("DbConnect");

        public RedisConfig RedisConfig => _configuration.GetApolloValue<RedisConfig>("RedisSettings");

        public List<MongoDbConfig> MongoConfigs => _configuration.GetApolloValue<List<MongoDbConfig>>("Mongos");

        public string Viserion => _configuration.GetApolloValue<string>("TraceDebug");
    }


    public static class ApolloExtensions
    {
        /// <summary>
        /// 从配置中获取字符串并转换为指定类型
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="configuration">IConfiguration实例</param>
        /// <param name="key">配置键</param>
        /// <returns>转换后的对象，如果转换失败则返回默认值</returns>
        public static T GetApolloValue<T>(this IConfiguration configuration, string key)
        {
            try
            {
                //var value = _configuration.GetSection(key).Value;
                var value = configuration[key];
                if (string.IsNullOrEmpty(value))
                    return default;

                if (typeof(T).IsPrimitive || typeof(T) == typeof(string))
                    return (T)Convert.ChangeType(value, typeof(T));

                return JsonConvert.DeserializeObject<T>(value);
            }
            catch
            {
                return default;
            }
        }

    }

}
