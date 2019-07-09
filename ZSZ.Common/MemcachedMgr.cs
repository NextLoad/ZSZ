using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace ZSZ.Common
{
    public class MemcachedMgr
    {
        private MemcachedClient client;
        public static MemcachedMgr Instance;

        static MemcachedMgr()
        {
            Instance = new MemcachedMgr();
        }

        private MemcachedMgr()
        {
            MemcachedClientConfiguration config = new MemcachedClientConfiguration();
            config.Servers.Add(new IPEndPoint(IPAddress.Loopback, 11211));
            config.Protocol = MemcachedProtocol.Binary;
            client = new MemcachedClient(config);
        }

        public void SetValue(string key, object value, TimeSpan expires)
        {
            if (!value.GetType().IsSerializable)
            {
                throw new Exception("value必须是可序列话的对象");
            }
            client.Store(Enyim.Caching.Memcached.StoreMode.Set, key, value, expires);//还可以指定第四个
        }

        public T GetValue<T>(string key)
        {
            return client.Get<T>(key);
        }
    }
}
