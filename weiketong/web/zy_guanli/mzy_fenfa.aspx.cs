using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using App_Code;

using System.Web.Profile;

namespace web.zy_guanli
{
    public partial class mzy_fenfa : System.Web.UI.Page
    {
        public class login_info
        {
            

            public string islogin
            {
                get;
                set;
            }

            public string user_id
            {
                get;
                set;
            }
        }

        public class telnum
        {
           

            public string name
            {
                get;
                set;
            }

            public string num
            {
                get;
                set;
            }
        }

        protected string json = "null";

        protected string ff_how = "0";

        protected DefaultProfile Profile
        {
            get
            {
                return (DefaultProfile)this.Context.Profile;
            }
        }

        protected HttpApplication ApplicationInstance
        {
            get
            {
                return this.Context.ApplicationInstance;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Settings settings = Configure.GetSettings();
            if (settings != null)
            {
                this.ff_how = settings.Catch_count.ToString();
            }
            string a = "";
            try
            {
                a = base.Request.Params["login"];
            }
            catch
            {
            }
            if (a == "no")
            {
                try
                {
                    DataTable dataTable = new DataBase().GetDataTable(string.Concat(new string[]
				{
					"select * from user_info where can_use=1 and work_id='",
					base.Request.Params["username"],
					"' and password='",
					base.Request.Params["password"],
					"'"
				}));
                    List<login_info> list = new List<login_info>();
                    login_info login_info = new login_info();
                    int num = 18;
                    int num2 = 0;
                    try
                    {
                        num2 = int.Parse(base.Request.Params["banben"]);
                    }
                    catch (Exception)
                    {
                        num2 = 1;
                    }
                    if (dataTable.Rows.Count > 0 && num2 > num && new ChickServer().System_canused(base.Request.Params["username"]))
                    {
                        login_info.islogin = "success";
                        login_info.user_id = dataTable.Rows[0]["user_id"].ToString();
                    }
                    else
                    {
                        login_info.islogin = "faild";
                        login_info.user_id = "";
                    }
                    list.Add(login_info);
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    this.json = javaScriptSerializer.Serialize(list);
                    base.Response.Write(this.json);
                }
                catch
                {
                    base.Response.Write("Login Error");
                }
            }
            if (a == "yes")
            {
                TimeSpan timeSpan = new ChickServer().overtime() - DateTime.Now;
                string text = "1=0";
                if (timeSpan.TotalSeconds > 0.0)
                {
                    text = "1=1";
                }
                string text2 = "";
                try
                {
                    List<telnum> list2 = new List<telnum>();
                    DataTable dataTable2 = new DataBase().GetDataTable(string.Concat(new string[]
				{
					"select top ",
					this.ff_how,
					" * from zy_fenfa where ",
					text,
					" and isused=0 and user_id=",
					base.Request.Params["userid"],
					" order by id desc"
				}));
                    for (int i = 0; i < dataTable2.Rows.Count; i++)
                    {
                        list2.Add(new telnum
                        {
                            name = dataTable2.Rows[i]["name"].ToString(),
                            num = dataTable2.Rows[i]["num"].ToString()
                        });
                        text2 = text2 + dataTable2.Rows[i]["id"].ToString() + ",";
                    }
                    text2 = text2.Substring(0, text2.Length - 1);
                    int num3 = 0;
                    try
                    {
                        num3 = new DataBase().UpdataData("zy_fenfa", " type_fenfa=2,isused=1,use_time='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'", "id in (" + text2 + ")");
                    }
                    catch
                    {
                        num3 = 0;
                    }
                    if (num3 > 0)
                    {
                        JavaScriptSerializer javaScriptSerializer2 = new JavaScriptSerializer();
                        this.json = javaScriptSerializer2.Serialize(list2);
                    }
                    base.Response.Write(this.json);
                }
                catch
                {
                    base.Response.Write(this.json);
                }
            }
        }
    }
}