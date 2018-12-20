using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CacheDemo
{
    public partial class ajaxwork : System.Web.UI.Page
    {
        private string type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            type = Request.QueryString["type"];

            if (Request.QueryString["key"] == null)
            {
                Start(type);
            }
            else
            {
                ajaxResponse();
            }
        }

        private void DoWork1()
        {
            //var stus = GetList();
            DateTime starttime = DateTime.Now;
            for (int i = 1; i <= 100000; i++)
            {
                CacheHelper.Insert<String>("stu" + i, "ioo_" + i + "_ooi");
                if (i % (100000 / 100) == 0)
                {
                    Session["count1"] = "{'curr':'" + (i / (100000 / 100)).ToString() + "','time':'" + (DateTime.Now - starttime) + "'}";
                }
            }
        }

        private void DoWork2()
        {
            //List<string> stu = null;
            DateTime starttime = DateTime.Now;
            for (int i = 1; i <= 100000; i++)
            {
                CacheHelper.Get<String>("stu" + i);
                if (i % (100000 / 100) == 0)
                {
                    Session["count2"] = "{'curr':'" + (i / (100000 / 100)).ToString() + "','time':'" + (DateTime.Now - starttime) + "'}"; //反馈当前任务状态
                }
            }
        }

        private void DoWork3()
        {
            DateTime starttime = DateTime.Now;
            for (int i = 1; i <= 100000; i++)
            {
                CacheHelper.Remove("stu" + i);
                if (i % (100000 / 100) == 0)
                {
                    Session["count3"] = "{'curr':'" + (i / (100000 / 100)).ToString() + "','time':'" + (DateTime.Now - starttime) + "'}"; //反馈当前任务状态
                }
            }
        }

        /// <summary>
        /// 线程启动
        /// </summary>
        private void Start(string type)
        {
            Thread t = null;
            switch (type)
            {
                case "1"://新增测试
                    t = new Thread(DoWork1);
                    t.Start();
                    Session["count1"] = "{'curr':'0','time':'00:00:00.0000000'}";
                    Response.Write(Session["count1"]);
                    break;
                case "2"://查询测试
                    t = new Thread(DoWork2);
                    t.Start();
                    Session["count2"] = "{'curr':'0','time':'00:00:00.0000000'}";
                    Response.Write(Session["count2"]);
                    break;
                case "3"://删除测试
                    t = new Thread(DoWork3);
                    t.Start();
                    Session["count3"] = "{'curr':'0','time':'00:00:00.0000000'}";
                    Response.Write(Session["count3"]);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 获取任务的实时信息
        /// </summary>
        private void ajaxResponse()
        {
            Response.Write(Session["count"+type]);
            Response.Flush();
            Response.End();
        }

        private List<string> GetList()
        {
            List<string> stu_s = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                stu_s.Add("zhangsan" + i);
            }
            return stu_s;
        }
    }
}