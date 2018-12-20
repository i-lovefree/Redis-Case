using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Redistest.view
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            RedisUtli.CacheHelper.Insert<string>("name", "zhangsan", DateTime.Now.AddSeconds(60));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            var name = RedisUtli.CacheHelper.Get<string>("name");
            this.TextBox5.Text = name == null ? "NULL" : name;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            RedisUtli.CacheHelper.Remove("name");
        }
    }
}