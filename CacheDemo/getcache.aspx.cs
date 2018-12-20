using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CacheDemo
{
    public partial class getcache : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string key = Request.QueryString["key"];
            //var val = (List<string>)CacheHelper2.GetCache(key);
            var val = (String)CacheHelper2.GetCache(key);
            //var str=Newtonsoft.Json.JsonConvert.SerializeObject(val);

            Response.Write("{'val':'" + val + "'}");
            Response.Flush();
            Response.End();
        }
    }
}