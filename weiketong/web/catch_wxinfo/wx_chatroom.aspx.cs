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
    public partial class wx_chatroom : System.Web.UI.Page
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

            public string chatroomname
            {
                get;
                set;
            }

            public long addtime
            {
                get;
                set;
            }

            public string memberlist
            {
                get;
                set;
            }

            public string displayname
            {
                get;
                set;
            }

            public string chatroomnick
            {
                get;
                set;
            }

            public long roomflag
            {
                get;
                set;
            }

            public string roomowner
            {
                get;
                set;
            }

            public string roomdata
            {
                get;
                set;
            }

            public long isShowname
            {
                get;
                set;
            }

            public string selfDisplayName
            {
                get;
                set;
            }

            public long style
            {
                get;
                set;
            }

            public long chatroomdataflag
            {
                get;
                set;
            }

            public long modifytime
            {
                get;
                set;
            }

            public string chatroomnotice
            {
                get;
                set;
            }

            public long chatroomnoticeNewVersion
            {
                get;
                set;
            }

            public long chatroomnoticeOldVersion
            {
                get;
                set;
            }

            public string chatroomnoticeEditor
            {
                get;
                set;
            }

            public long chatroomnoticePublishTime
            {
                get;
                set;
            }
        }

        protected string chatroomjson = "";

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
                this.chatroomjson = base.Request.Form["chatroom"];
                this.chatroomjson = HttpUtility.UrlDecode(this.chatroomjson, Encoding.UTF8);
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
                }.Deserialize<WxUidCross>(this.chatroomjson);
                if (new gongneng().limit_time(wxUidCross.user_cross_wx, "chatroomjson", 50))
                {
                    new DataSet();
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("user_cross_wx", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("userid", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("chatroomname", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("addtime", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("memberlist", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("displayname", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("chatroomnick", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("roomflag", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("roomowner", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("roomdata", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("isShowname", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("selfDisplayName", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("style", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("chatroomdataflag", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("modifytime", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("chatroomnotice", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("chatroomnoticeNewVersion", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("chatroomnoticeOldVersion", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("chatroomnoticeEditor", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("chatroomnoticePublishTime", Type.GetType("System.Int64")));
                    if (wxUidCross.user_cross_wx != 0 && wxUidCross.userid != 0)
                    {
                        for (int i = 0; i < wxUidCross.wx_info.Count; i++)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["user_cross_wx"] = wxUidCross.user_cross_wx;
                            dataRow["chatroomname"] = wxUidCross.wx_info[i].chatroomname;
                            dataRow["addtime"] = wxUidCross.wx_info[i].addtime;
                            dataRow["memberlist"] = wxUidCross.wx_info[i].memberlist;
                            dataRow["displayname"] = wxUidCross.wx_info[i].displayname;
                            dataRow["chatroomnick"] = wxUidCross.wx_info[i].chatroomnick;
                            dataRow["roomflag"] = wxUidCross.wx_info[i].roomflag;
                            dataRow["roomowner"] = wxUidCross.wx_info[i].roomowner;
                            dataRow["roomdata"] = wxUidCross.wx_info[i].roomdata;
                            dataRow["isShowname"] = wxUidCross.wx_info[i].isShowname;
                            dataRow["selfDisplayName"] = wxUidCross.wx_info[i].selfDisplayName;
                            dataRow["style"] = wxUidCross.wx_info[i].style;
                            dataRow["chatroomdataflag"] = wxUidCross.wx_info[i].chatroomdataflag;
                            dataRow["modifytime"] = wxUidCross.wx_info[i].modifytime;
                            dataRow["chatroomnotice"] = wxUidCross.wx_info[i].chatroomnotice;
                            dataRow["chatroomnoticeNewVersion"] = wxUidCross.wx_info[i].chatroomnoticeNewVersion;
                            dataRow["chatroomnoticeOldVersion"] = wxUidCross.wx_info[i].chatroomnoticeOldVersion;
                            dataRow["chatroomnoticeEditor"] = wxUidCross.wx_info[i].chatroomnoticeEditor;
                            dataRow["chatroomnoticePublishTime"] = wxUidCross.wx_info[i].chatroomnoticePublishTime;
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    new gongneng().wx_chatroom(dataTable);
                }
            }
            catch (Exception ex)
            {
                object obj = this.title;
                this.title = string.Concat(new object[]
			{
				obj,
				"chatroom,json is Err||",
				ex,
				"chatroomjson=",
				this.chatroomjson,
				"<<<<<<<<<<<<<"
			});
                new NetLog().WriteTextLog(this.title + "chatroom", "end");
            }
        }
    }
}