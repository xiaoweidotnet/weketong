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
    public partial class wx_img_flag : System.Web.UI.Page
    {

        public class Wx_info
        {


            public string username
            {
                get;
                set;
            }

            public long imgflag
            {
                get;
                set;
            }

            public long lastupdatetime
            {
                get;
                set;
            }

            public string reserved1
            {
                get;
                set;
            }

            public string reserved2
            {
                get;
                set;
            }

            public long reserved3
            {
                get;
                set;
            }

            public long reserved4
            {
                get;
                set;
            }
        }

        public class WxUidCross
        {

            private List<Wx_info> c;

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
      

        protected string img_flagjson = "";

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
                this.img_flagjson = base.Request.Form["img_flag"];
                this.img_flagjson = HttpUtility.UrlDecode(this.img_flagjson, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                object obj = this.title;
                this.title = string.Concat(new object[]
			{
				obj,
				"requeat is Err||",
				ex,
				"img_flagjson=",
				this.img_flagjson
			});
            }
            try
            {
                WxUidCross wxUidCross = new JavaScriptSerializer
                {
                    MaxJsonLength = 2147483647
                }.Deserialize<WxUidCross>(this.img_flagjson);
                if (new gongneng().limit_time(wxUidCross.user_cross_wx, "img_flagjson", 50))
                {
                    new DataSet();
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("user_cross_wx", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("userid", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("username", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("imgflag", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("lastupdatetime", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("reserved1", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("reserved2", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("reserved3", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("reserved4", Type.GetType("System.Int32")));
                    if (wxUidCross.user_cross_wx != 0 && wxUidCross.userid != 0)
                    {
                        for (int i = 0; i < wxUidCross.wx_info.Count; i++)
                        {
                            if (new gongneng().isbaohan(wxUidCross.wx_info[i].username.ToString()))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                dataRow["user_cross_wx"] = wxUidCross.user_cross_wx;
                                dataRow["userid"] = wxUidCross.userid;
                                dataRow["username"] = wxUidCross.wx_info[i].username;
                                dataRow["imgflag"] = wxUidCross.wx_info[i].imgflag;
                                dataRow["lastupdatetime"] = wxUidCross.wx_info[i].lastupdatetime;
                                dataRow["reserved1"] = wxUidCross.wx_info[i].reserved1;
                                dataRow["reserved2"] = wxUidCross.wx_info[i].reserved2;
                                dataRow["reserved3"] = wxUidCross.wx_info[i].reserved3;
                                dataRow["reserved4"] = wxUidCross.wx_info[i].reserved4;
                                dataTable.Rows.Add(dataRow);
                            }
                        }
                    }
                    new gongneng().wx_img_flag(dataTable);
                }
            }
            catch (Exception ex2)
            {
                object obj2 = this.title;
                this.title = string.Concat(new object[]
			{
				obj2,
				"img_flag,json is Err",
				ex2,
				"||img_flagjson=",
				this.img_flagjson,
				"<<<<<<<<<<<<<<<<"
			});
                new NetLog().WriteTextLog(this.title + "img_flag", "end");
            }
        }
    }


 
}