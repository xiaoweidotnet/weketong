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
    


    public partial class wx_appmessage : System.Web.UI.Page
    {
        public class WxUidCross
        {


            public int user_cross_wx
            {
                get;
                set;
            }

            public int userid
            {
                get;
                set;
            }

            public List<Wx_info> wx_info
            {
                get;
                set;
            }
        }

        public class Wx_info
        {


            public long msgid
            {
                get;
                set;
            }

            public string appid
            {
                get;
                set;
            }

            public string title
            {
                get;
                set;
            }

            public string description
            {
                get;
                set;
            }

            public long type
            {
                get;
                set;
            }
        }

        protected string appmessagejson = "";

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
                this.appmessagejson = base.Request.Form["appmessage"];
                this.appmessagejson = HttpUtility.UrlDecode(this.appmessagejson, Encoding.UTF8);
            }
            catch (Exception arg)
            {
                this.title = this.title + "requeat is Err||" + arg;
            }
            try
            {
                WxUidCross wxUidCross = new JavaScriptSerializer
                {
                    MaxJsonLength = 2147483647
                }.Deserialize<WxUidCross>(this.appmessagejson);
                if (new gongneng().limit_time(wxUidCross.user_cross_wx, "appmessagejson", 50))
                {
                    new DataSet();
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("user_cross_wx", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("userid", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("msgid", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("appid", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("title", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("description", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("type", Type.GetType("System.Int64")));
                    if (wxUidCross.user_cross_wx != 0 && wxUidCross.userid != 0)
                    {
                        for (int i = 0; i < wxUidCross.wx_info.Count; i++)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["user_cross_wx"] = wxUidCross.user_cross_wx;
                            dataRow["userid"] = wxUidCross.userid;
                            dataRow["msgid"] = wxUidCross.wx_info[i].msgid;
                            dataRow["appid"] = wxUidCross.wx_info[i].appid;
                            dataRow["title"] = wxUidCross.wx_info[i].title;
                            dataRow["description"] = wxUidCross.wx_info[i].description;
                            dataRow["type"] = wxUidCross.wx_info[i].type;
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    new gongneng().wx_appmessage(dataTable);
                }
            }
            catch (Exception ex)
            {
                object obj = this.title;
                this.title = string.Concat(new object[]
			{
				obj,
				"appmessage,json is Err||",
				ex,
				"appmessagejson=",
				this.appmessagejson,
				"<<<<<<<<<<<<"
			});
                new NetLog().WriteTextLog(this.title + "appmessage", "end");
            }
        }
    }
}