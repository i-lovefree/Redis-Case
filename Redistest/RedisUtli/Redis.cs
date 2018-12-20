using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Redistest.RedisUtli
{
    public class Redis : ICache
    {
        int Default_Timeout = 60;//默认超时时间（单位秒）
        string address;
        string dbpwd;
        JsonSerializerSettings jsonConfig = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };
        ConnectionMultiplexer connectionMultiplexer;
        IDatabase database;

        class CacheObject<T>
        {
            public int ExpireTime { get; set; }
            public bool ForceOutofDate { get; set; }
            public T Value { get; set; }
        }

        public Redis()
        {
            this.address = ConfigurationManager.AppSettings["RedisServer"];
            this.dbpwd = ConfigurationManager.AppSettings["RedisServer_pwd"];
            if (this.address == null || string.IsNullOrWhiteSpace(this.address.ToString()))
            {
                throw new ApplicationException("配置文件中未找到RedisServer的有效配置");
            }
            if (string.IsNullOrEmpty(dbpwd) || string.IsNullOrWhiteSpace(dbpwd))
            {
                connectionMultiplexer = ConnectionMultiplexer.Connect(address);
            }
            else
            {
                var options = ConfigurationOptions.Parse(address);
                options.Password = dbpwd;
                connectionMultiplexer = ConnectionMultiplexer.Connect(options);
            }
            
            //database = connectionMultiplexer.GetDatabase();
        }

        /// <summary>
        /// 连接超时设置
        /// </summary>
        public int TimeOut
        {
            get
            {
                return Default_Timeout;
            }
            set
            {
                Default_Timeout = value;
            }
        }

        public object Get(string key, int db_index = -1)
        {
            return Get<object>(key,db_index);
        }

        public T Get<T>(string key, int db_index = -1)
        {
            database = connectionMultiplexer.GetDatabase(db: db_index);

            var cacheValue = database.StringGet(key);

            var value = default(T);
            if (!cacheValue.IsNull)
            {
                var cacheObject = JsonConvert.DeserializeObject<CacheObject<T>>(cacheValue, jsonConfig);
                if (!cacheObject.ForceOutofDate)
                {
                    database.KeyExpire(key, new TimeSpan(0, 0, cacheObject.ExpireTime));
                }
                value = cacheObject.Value;
            }

            return value;
        }
        /// <summary>
        /// 添加一个Redis-Key
        /// </summary>
        /// <param name="key">Key的名字</param>
        /// <param name="data">Key的值</param>
        /// <param name="db_index">第几个redis库(默认-1)</param>
        public void Insert(string key, object data,int db_index=-1)
        {
            database = connectionMultiplexer.GetDatabase(db: db_index);

            var jsonData = GetJsonData(data, TimeOut, false);
            database.StringSet(key, jsonData);
        }
        /// <summary>
        /// 添加一个Redis-Key
        /// </summary>
        /// <param name="key">Key的名字</param>
        /// <param name="data">Key的值</param>
        /// <param name="cacheTime">多少秒之后失效</param>
        /// <param name="db_index">第几个redis库(默认-1)</param>
        public void Insert(string key, object data, int cacheTime, int db_index = -1)
        {
            database = connectionMultiplexer.GetDatabase(db: db_index);

            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = GetJsonData(data, TimeOut, true);
            database.StringSet(key, jsonData, timeSpan);
        }
        /// <summary>
        /// 添加一个Redis-Key
        /// </summary>
        /// <param name="key">Key的名字</param>
        /// <param name="data">Key的值</param>
        /// <param name="cacheTime">在什么时间后失效</param>
        /// <param name="db_index">第几个redis库(默认-1)</param>
        public void Insert(string key, object data, DateTime cacheTime, int db_index = -1)
        {
            database = connectionMultiplexer.GetDatabase(db: db_index);

            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = GetJsonData(data, TimeOut, true);
            database.StringSet(key, jsonData, timeSpan);
        }

        public void Insert<T>(string key, T data, int db_index = -1)
        {
            database = connectionMultiplexer.GetDatabase(db: db_index);

            var jsonData = GetJsonData<T>(data, TimeOut, false);
            database.StringSet(key, jsonData);
        }

        public void Insert<T>(string key, T data, int cacheTime, int db_index = -1)
        {
            database = connectionMultiplexer.GetDatabase(db: db_index);

            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = GetJsonData<T>(data, TimeOut, true);
            database.StringSet(key, jsonData, timeSpan);
        }

        public void Insert<T>(string key, T data, DateTime cacheTime, int db_index = -1)
        {
            database = connectionMultiplexer.GetDatabase(db: db_index);

            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = GetJsonData<T>(data, TimeOut, true);
            database.StringSet(key, jsonData, timeSpan);
        }


        string GetJsonData(object data, int cacheTime, bool forceOutOfDate)
        {
            var cacheObject = new CacheObject<object>() { Value = data, ExpireTime = cacheTime, ForceOutofDate = forceOutOfDate };
            //return JsonConvert.SerializeObject(cacheObject);
            return JsonConvert.SerializeObject(cacheObject, Formatting.None, jsonConfig);//序列化对象
        }

        string GetJsonData<T>(T data, int cacheTime, bool forceOutOfDate)
        {
            var cacheObject = new CacheObject<T>() { Value = data, ExpireTime = cacheTime, ForceOutofDate = forceOutOfDate };
            //return JsonConvert.SerializeObject(cacheObject);
            return JsonConvert.SerializeObject(cacheObject, Formatting.None, jsonConfig);//序列化对象
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key, int db_index = -1)
        {
            database = connectionMultiplexer.GetDatabase(db: db_index);

            database.KeyDelete(key, CommandFlags.HighPriority);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        public bool Exists(string key, int db_index = -1)
        {
            database = connectionMultiplexer.GetDatabase(db: db_index);

            return database.KeyExists(key);
        }
    }
}