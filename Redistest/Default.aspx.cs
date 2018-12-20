using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Redistest
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TextBox1.Text = Request.ServerVariables.Get("Server_Name").ToString();
            this.TextBox2.Text = Request.ServerVariables.Get("Local_Addr").ToString();
            this.TextBox3.Text = Request.ServerVariables.Get("LOCAL_ADDR").ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //RedisUtli.CacheHelper.Insert<string>("name", "zhangsan", DateTime.Now.AddSeconds(60));
            Response.Write("<script>alert('a')</script>");
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