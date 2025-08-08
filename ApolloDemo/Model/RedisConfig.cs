namespace ApolloDemo.Model
{
    /// <summary>
    /// 缓存配置
    /// </summary>
    public class RedisConfig
    {
        /// <summary>
        /// 要选择的Redis的数据库
        /// </summary>
        public int Database { get; set; }

        /// <summary>
        /// 缓存键前缀
        /// </summary>
        public string RedisKeyPrefix { get; set; } = "";

        /// <summary>
        /// Redis的连接串
        /// </summary>
        public string RedisConnect { get; set; } = "";

        /// <summary>
        /// 是否开启日志
        /// </summary>
        public bool HaveLog { get; set; }

        /// <summary>
        /// 是否关闭缓存
        /// </summary>
        public bool CloseRedis { get; set; }

        /// <summary>
        /// 服务器策略
        /// </summary>
        public ServerEnumerationStrategy ServerEnumerationStrategy { get; set; }
    }

    public class ServerEnumerationStrategy
    {
        public enum ModeOptions
        {
            All = 0,
            Single
        }

        public enum TargetRoleOptions
        {
            Any = 0,
            PreferSlave
        }

        public enum UnreachableServerActionOptions
        {
            Throw = 0,
            IgnoreIfOtherAvailable
        }

        public ModeOptions Mode { get; set; }

        public TargetRoleOptions TargetRole { get; set; }

        public UnreachableServerActionOptions UnreachableServerAction { get; set; }
    }
}
