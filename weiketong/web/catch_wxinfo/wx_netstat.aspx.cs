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
    public partial class wx_netstat : System.Web.UI.Page
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
           

            public int id
            {
                get;
                set;
            }

            public int peroid
            {
                get;
                set;
            }

            public int textcountcn
            {
                get;
                set;
            }

            public int textbytesIn
            {
                get;
                set;
            }

            public int imageCountIn
            {
                get;
                set;
            }

            public int imageBytesIn
            {
                get;
                set;
            }

            public int voiceCountIn
            {
                get;
                set;
            }

            public int voiceBytesIn
            {
                get;
                set;
            }

            public int videoCountIn
            {
                get;
                set;
            }

            public int videoBytesIn
            {
                get;
                set;
            }

            public int mobileBytesIn
            {
                get;
                set;
            }

            public int wifiBytesIn
            {
                get;
                set;
            }

            public int sysMobileBytesIn
            {
                get;
                set;
            }

            public int sysWifiBytesIn
            {
                get;
                set;
            }

            public int textCountOut
            {
                get;
                set;
            }

            public int textBytesOut
            {
                get;
                set;
            }

            public int imageCountOut
            {
                get;
                set;
            }

            public int imageBytesOut
            {
                get;
                set;
            }

            public int voiceCountOut
            {
                get;
                set;
            }

            public int voiceBytesOut
            {
                get;
                set;
            }

            public int videoCountOut
            {
                get;
                set;
            }

            public int videoBytesOut
            {
                get;
                set;
            }

            public int mobileBytesOut
            {
                get;
                set;
            }

            public int wifiBytesOut
            {
                get;
                set;
            }

            public int sysMobileBytesOut
            {
                get;
                set;
            }

            public int sysWifiBytesOut
            {
                get;
                set;
            }

            public int reserved1
            {
                get;
                set;
            }

            public int reserved2
            {
                get;
                set;
            }

            public int realMobileBytesIn
            {
                get;
                set;
            }

            public int realWifiBytesIn
            {
                get;
                set;
            }

            public int realMobileBytesOut
            {
                get;
                set;
            }

            public int realWifiBytesOut
            {
                get;
                set;
            }
        }

        protected string netstatjson = "";

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
                this.netstatjson = base.Request.Form["netstat"];
                this.netstatjson = HttpUtility.UrlDecode(this.netstatjson, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                object obj = this.title;
                this.title = string.Concat(new object[]
			{
				obj,
				"requeat is Err||",
				ex,
				"netstatjson=",
				this.netstatjson
			});
            }
            try
            {
                WxUidCross wxUidCross = new JavaScriptSerializer
                {
                    MaxJsonLength = 2147483647
                }.Deserialize<WxUidCross>(this.netstatjson);
                if (new gongneng().limit_time(wxUidCross.user_cross_wx, "netstatjson", 50))
                {
                    new DataSet();
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("user_cross_wx", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("userid", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("id", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("peroid", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("textcountcn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("textbytesIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("imageCountIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("imageBytesIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("voiceCountIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("voiceBytesIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("videoCountIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("videoBytesIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("mobileBytesIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("wifiBytesIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("sysMobileBytesIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("sysWifiBytesIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("textCountOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("textBytesOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("imageCountOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("imageBytesOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("voiceCountOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("voiceBytesOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("videoCountOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("videoBytesOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("mobileBytesOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("wifiBytesOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("sysMobileBytesOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("sysWifiBytesOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("reserved1", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("reserved2", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("realMobileBytesIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("realWifiBytesIn", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("realMobileBytesOut", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("realWifiBytesOut", Type.GetType("System.Int32")));
                    if (wxUidCross.user_cross_wx != 0 && wxUidCross.userid != 0)
                    {
                        for (int i = 0; i < wxUidCross.wx_info.Count; i++)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["user_cross_wx"] = wxUidCross.user_cross_wx;
                            dataRow["userid"] = wxUidCross.userid;
                            dataRow["id"] = wxUidCross.wx_info[i].id;
                            dataRow["peroid"] = wxUidCross.wx_info[i].peroid;
                            dataRow["textcountcn"] = wxUidCross.wx_info[i].textcountcn;
                            dataRow["textbytesIn"] = wxUidCross.wx_info[i].textbytesIn;
                            dataRow["imageCountIn"] = wxUidCross.wx_info[i].imageCountIn;
                            dataRow["imageBytesIn"] = wxUidCross.wx_info[i].imageBytesIn;
                            dataRow["voiceCountIn"] = wxUidCross.wx_info[i].voiceCountIn;
                            dataRow["voiceBytesIn"] = wxUidCross.wx_info[i].voiceBytesIn;
                            dataRow["videoCountIn"] = wxUidCross.wx_info[i].videoCountIn;
                            dataRow["videoBytesIn"] = wxUidCross.wx_info[i].videoBytesIn;
                            dataRow["mobileBytesIn"] = wxUidCross.wx_info[i].mobileBytesIn;
                            dataRow["wifiBytesIn"] = wxUidCross.wx_info[i].wifiBytesIn;
                            dataRow["sysMobileBytesIn"] = wxUidCross.wx_info[i].sysMobileBytesIn;
                            dataRow["sysWifiBytesIn"] = wxUidCross.wx_info[i].sysWifiBytesIn;
                            dataRow["textCountOut"] = wxUidCross.wx_info[i].textCountOut;
                            dataRow["textBytesOut"] = wxUidCross.wx_info[i].textBytesOut;
                            dataRow["imageCountOut"] = wxUidCross.wx_info[i].imageCountOut;
                            dataRow["imageBytesOut"] = wxUidCross.wx_info[i].imageBytesOut;
                            dataRow["voiceCountOut"] = wxUidCross.wx_info[i].voiceCountOut;
                            dataRow["voiceBytesOut"] = wxUidCross.wx_info[i].voiceBytesOut;
                            dataRow["videoCountOut"] = wxUidCross.wx_info[i].videoCountOut;
                            dataRow["videoBytesOut"] = wxUidCross.wx_info[i].videoBytesOut;
                            dataRow["mobileBytesOut"] = wxUidCross.wx_info[i].mobileBytesOut;
                            dataRow["wifiBytesOut"] = wxUidCross.wx_info[i].wifiBytesOut;
                            dataRow["sysMobileBytesOut"] = wxUidCross.wx_info[i].sysMobileBytesOut;
                            dataRow["sysWifiBytesOut"] = wxUidCross.wx_info[i].sysWifiBytesOut;
                            dataRow["reserved1"] = wxUidCross.wx_info[i].reserved1;
                            dataRow["reserved2"] = wxUidCross.wx_info[i].reserved2;
                            dataRow["realMobileBytesIn"] = wxUidCross.wx_info[i].realMobileBytesIn;
                            dataRow["realWifiBytesIn"] = wxUidCross.wx_info[i].realWifiBytesIn;
                            dataRow["realMobileBytesOut"] = wxUidCross.wx_info[i].realMobileBytesOut;
                            dataRow["realWifiBytesOut"] = wxUidCross.wx_info[i].realWifiBytesOut;
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    new gongneng().wx_netstat(dataTable);
                }
            }
            catch (Exception ex2)
            {
                object obj2 = this.title;
                this.title = string.Concat(new object[]
			{
				obj2,
				"netstat,json is Err||",
				ex2,
				"netstatjson=",
				this.netstatjson,
				"<<<<<<<<<<<<<<<<<<<<<"
			});
                new NetLog().WriteTextLog(this.title + "netstat", "end");
            }
        }
    }
}