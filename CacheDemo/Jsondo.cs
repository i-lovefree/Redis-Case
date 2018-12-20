using Commons.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CacheDemo
{
    public class Jsondo
    {
        

        private void sjon()
        {
            var person = new Person
            {
                Name = "Joe",
                Age = 25,
                Nationality = "US",
                Gender = "Male"
            };
            string a = JsonMapper.ToJson<Person>(person);

            Commons.Pool.PoolManager pool = new Commons.Pool.PoolManager();
            var pll=pool.NewPool<Person>();
            
        }
    }
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public string Gender { get; set; }
    }
}