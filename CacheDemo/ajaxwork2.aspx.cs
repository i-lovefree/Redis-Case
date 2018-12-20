using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CacheDemo
{
    public partial class ajaxwork2 : System.Web.UI.Page
    {
        private string type2 = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            type2 = Request.QueryString["type2"];

            if (Request.QueryString["key2"] == null)
            {
                Start(type2);
            }
            else
            {
                ajaxResponse();
            }
        }

        private void DoWork1()
        {
            //var stus = GetList("ooo_"+i+"_ooo");
            DateTime starttime = DateTime.Now;
            for (int i = 1; i <= 100000; i++)
            {
                CacheHelper2.SetCache("stu" + i, "ooo_" + i + "_ooo");
                if (i % (100000 / 100) == 0)
                {
                    Session["count11"] = "{'curr':'" + (i / (100000 / 100)).ToString() + "','time':'" + (DateTime.Now - starttime) + "'}";
                    //File.AppendAllText(@"D:\log.txt", Session["count11"] + "\n");
                }
            }
        }

        private void DoWork2()
        {
            //List<string> stu = null;
            DateTime starttime = DateTime.Now;
            for (int i = 1; i <= 100000; i++)
            {
                CacheHelper2.GetCache("stu" + i);
                if (i % (100000 / 100) == 0)
                {
                    Session["count12"] = "{'curr':'" + (i / (100000 / 100)).ToString() + "','time':'" + (DateTime.Now - starttime) + "'}"; //反馈当前任务状态
                }
            }
        }

        private void DoWork3()
        {
            DateTime starttime = DateTime.Now;
            for (int i = 1; i <= 100000; i++)
            {
                CacheHelper2.RemoveAllCache("stu" + i);
                if (i % (100000 / 100) == 0)
                {
                    Session["count13"] = "{'curr':'" + (i / (100000 / 100)).ToString() + "','time':'" + (DateTime.Now - starttime) + "'}"; //反馈当前任务状态
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
                    Session["count11"] = "{'curr':'0','time':'00:00:00.0000000'}";
                    Response.Write(Session["count11"]);
                    break;
                case "2"://查询测试
                    t = new Thread(DoWork2);
                    t.Start();
                    Session["count12"] = "{'curr':'0','time':'00:00:00.0000000'}";
                    Response.Write(Session["count12"]);
                    break;
                case "3"://删除测试
                    t = new Thread(DoWork3);
                    t.Start();
                    Session["count13"] = "{'curr':'0','time':'00:00:00.0000000'}";
                    Response.Write(Session["count13"]);
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
            //File.AppendAllText(@"D:\log.txt", Session["count1" + type2] + "\n");
            Response.Write(Session["count1" + type2]);
            Response.Flush();
            Response.End();
        }

        private List<string> GetList(string name)
        {
            List<string> stu_s = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                stu_s.Add(name + i);
            }
            return stu_s;
        }
    }
}