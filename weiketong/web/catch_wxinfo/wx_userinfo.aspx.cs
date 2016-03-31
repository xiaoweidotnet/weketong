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
    public partial class wx_userinfo : System.Web.UI.Page
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
            [CompilerGenerated]
            private long a;

            [CompilerGenerated]
            private int b;

            [CompilerGenerated]
            private string c;

            public long id
            {
                get;
                set;
            }

            public int type
            {
                get;
                set;
            }

            public string value
            {
                get;
                set;
            }
        }

        protected string userinfojson = "";

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
                this.userinfojson = base.Request.Form["userinfo"];
                this.userinfojson = HttpUtility.UrlDecode(this.userinfojson, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                object obj = this.title;
                this.title = string.Concat(new object[]
			{
				obj,
				"requeat is Err||",
				ex,
				"userinfojson=",
				this.userinfojson
			});
            }
            try
            {
                WxUidCross wxUidCross = new JavaScriptSerializer
                {
                    MaxJsonLength = 2147483647
                }.Deserialize<WxUidCross>(this.userinfojson);
                if (new gongneng().limit_time(wxUidCross.user_cross_wx, "userinfojson", 50))
                {
                    new DataSet();
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("user_cross_wx", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("userid", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("id", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("type", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("value", Type.GetType("System.String")));
                    if (wxUidCross.user_cross_wx != 0 && wxUidCross.userid != 0)
                    {
                        for (int i = 0; i < wxUidCross.wx_info.Count; i++)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["user_cross_wx"] = wxUidCross.user_cross_wx;
                            dataRow["userid"] = wxUidCross.userid;
                            dataRow["id"] = wxUidCross.wx_info[i].id;
                            dataRow["type"] = wxUidCross.wx_info[i].type;
                            dataRow["value"] = wxUidCross.wx_info[i].value;
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    new gongneng().wx_userinfo(dataTable);
                }
            }
            catch (Exception ex2)
            {
                object obj2 = this.title;
                this.title = string.Concat(new object[]
			{
				obj2,
				"userinfo,json is Err||",
				ex2,
				"userinfojson=",
				this.userinfojson,
				"<<<<<<<<<<<<<<<<<<<<<<<"
			});
                new NetLog().WriteTextLog(this.title + "wx_userinfo", "end");
            }
        }
    }
}