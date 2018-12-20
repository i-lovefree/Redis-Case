using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CacheDemo
{
    public partial class Default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private List<string> GetList()
        {
            List<string> stu_s = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                stu_s.Add("zhangsan"+i);
            }
            return stu_s;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            var stus=GetList();
            var time1 = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                CacheHelper.Insert<List<string>>("stu" + i, stus);
            }
            var time2 = DateTime.Now;

            Response.Write("<script>alert('时间消耗：" + (time2 - time1).TotalSeconds + "');</script>");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            List<string> stu = null;
            var time1 = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                stu=CacheHelper.Get<List<string>>("stu"+i);
            }
            var time2 = DateTime.Now;

            Response.Write("<script>alert('时间消耗：" + (time2 - time1).TotalSeconds + "');</script>");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            var time1 = DateTime.Now;
            for (int i = 0; i < 100000; i++)
            {
                CacheHelper.Remove("stu" + i);
            }
            var time2 = DateTime.Now;

            Response.Write("<script>alert('时间消耗：" + (time2 - time1).TotalSeconds + "');</script>");
        }
    }
}