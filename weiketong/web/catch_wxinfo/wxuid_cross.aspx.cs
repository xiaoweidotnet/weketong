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

namespace web.catch_wxinfo
{
    public partial class wxuid_cross : System.Web.UI.Page
    {
        public class WxUidCross
        {

            public string userid
            {
                get;
                set;
            }

            public string wxid
            {
                get;
                set;
            }
        }

        protected string testjson = "cd";

        protected string title = "";

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
            try
            {
                this.testjson = base.Request.Form["wxuidjson"];
                this.testjson = HttpUtility.UrlDecode(this.testjson, Encoding.UTF8);
                try
                {
                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                    WxUidCross wxUidCross = javaScriptSerializer.Deserialize<WxUidCross>(this.testjson);
                    if (wxUidCross.userid.Trim() != "" && wxUidCross.wxid.Trim() != "")
                    {
                        DataTable dataTable = new DataBase().GetDataTable(string.Concat(new string[]
					{
						"select cross_id from wx_user_cross where user_id=",
						wxUidCross.userid,
						" and wxid='",
						wxUidCross.wxid,
						"'"
					}));
                        if (dataTable.Rows.Count > 0)
                        {
                            base.Response.Write(dataTable.Rows[0]["cross_id"].ToString());
                            new DataBase().UpdataData("wx_user_cross", "edittime=GETDATE()", "cross_id=" + dataTable.Rows[0]["cross_id"].ToString());
                        }
                        else
                        {
                            base.Response.Write(wxuid_cross.a(wxUidCross.userid, wxUidCross.wxid));
                        }
                    }
                }
                catch (Exception ex)
                {
                    HttpResponse arg_1A8_0 = base.Response;
                    object obj = this.title;
                    arg_1A8_0.Write(this.title = string.Concat(new object[]
				{
					obj,
					"wxuid_cross,Json is Err||",
					ex,
					"wxuid_crossjson=",
					this.testjson,
					"<<<<<<<<<<<<<<<<<<<<<"
				}));
                }
            }
            catch (Exception ex2)
            {
                HttpResponse arg_20F_0 = base.Response;
                object obj2 = this.title;
                arg_20F_0.Write(this.title = string.Concat(new object[]
			{
				obj2,
				"wxuid_cross,request is Err||",
				ex2,
				"wxuid_crossjson=",
				this.testjson,
				"<<<<<<<<<<<<<<<<<<<<<"
			}));
                new NetLog().WriteTextLog(this.title + "wxuid_cross", "end");
            }
        }

        private static string a(string A_0, string A_1)
        {
            new DataBase().InsertData("wx_user_cross", "user_id,wxid,add_time", string.Concat(new string[]
		{
			A_0,
			",'",
			A_1,
			"','",
			DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
			"'"
		}));
            DataTable dataTable = new DataBase().GetDataTable(string.Concat(new string[]
		{
			"select cross_id from wx_user_cross where user_id=",
			A_0,
			" and wxid='",
			A_1,
			"'"
		}));
            return dataTable.Rows[0]["cross_id"].ToString();
        }
    }
}