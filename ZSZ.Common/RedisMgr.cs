using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace ZSZ.Common
{
    public class RedisMgr
    {
        private static PooledRedisClientManager prcm;

        /// <summary>
        /// 创建链接池管理对象
        /// </summary>
        private static void CreateManager()
        {
            string[] servers = new[] { "127.0.0.1:6379" };
            //string[] readServerConStr = new[] { "Lg123456@" + Host + ":" + Port };
            //string[] writeServerConStr = new[] { "Lg123456@" + Host + ":" + Port };
            prcm = new PooledRedisClientManager(servers, servers,
                new RedisClientManagerConfig
                {
                    MaxWritePoolSize = 1000,
                    MaxReadPoolSize = 1000,
                    AutoStart = true,
                });
        }
        private static IRedisClient GetClient()
        {
            if (prcm == null)
            {
                CreateManager();
            }
            return prcm.GetClient();
        }

        private static IRedisClient GetReadOnlyClient()
        {
            if (prcm == null)
            {
                CreateManager();
            }
            return prcm.GetReadOnlyClient();
        }

        public void Set(string key, object value, TimeSpan expires)
        {
            using (var client = GetClient())
            {
                client.Set(key, value, expires);
            }
        }

        public T Get<T>(string key)
        {
            using (var client = GetReadOnlyClient())
            {
                return client.Get<T>(key);
            }
        }

    }
}
