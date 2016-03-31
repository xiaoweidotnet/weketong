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
    public partial class wx_message : System.Web.UI.Page
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
            

            public long msgId
            {
                get;
                set;
            }

            public long msgSvrId
            {
                get;
                set;
            }

            public int type
            {
                get;
                set;
            }

            public int status
            {
                get;
                set;
            }

            public int isSend
            {
                get;
                set;
            }

            public long isShowTimer
            {
                get;
                set;
            }

            public long createTime
            {
                get;
                set;
            }

            public string talker
            {
                get;
                set;
            }

            public string content
            {
                get;
                set;
            }

            public string imgPath
            {
                get;
                set;
            }

            public string reserved
            {
                get;
                set;
            }

            public string transContent
            {
                get;
                set;
            }

            public string transBrandWording
            {
                get;
                set;
            }

            public long talkerId
            {
                get;
                set;
            }

            public string bizClientMsgId
            {
                get;
                set;
            }

            public long bizChatId
            {
                get;
                set;
            }

            public string bizChatUserId
            {
                get;
                set;
            }
        }

        protected string messagejson = "";

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
                this.messagejson = base.Request.Form["message"];
                this.messagejson = HttpUtility.UrlDecode(this.messagejson, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                object obj = this.title;
                this.title = string.Concat(new object[]
			{
				obj,
				"requeat is Err||",
				ex,
				"messagejson=",
				this.messagejson
			});
            }
            try
            {
                WxUidCross wxUidCross = new JavaScriptSerializer
                {
                    MaxJsonLength = 2147483647
                }.Deserialize<WxUidCross>(this.messagejson);
                if (new gongneng().limit_time(wxUidCross.user_cross_wx, "messagejson", 15))
                {
                    new DataSet();
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("user_cross_wx", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("userid", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("msgId", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("msgSvrId", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("type", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("status", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("isSend", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("isShowTimer", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("createTime", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("talker", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("content", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("imgPath", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("reserved", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("transContent", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("transBrandWording", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("talkerId", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("bizClientMsgId", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("bizChatId", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("bizChatUserId", Type.GetType("System.String")));
                    if (wxUidCross.user_cross_wx != 0 && wxUidCross.userid != 0)
                    {
                        for (int i = 0; i < wxUidCross.wx_info.Count; i++)
                        {
                            if (new gongneng().isbaohan(wxUidCross.wx_info[i].talker.ToString()))
                            {
                                DataRow dataRow = dataTable.NewRow();
                                dataRow["user_cross_wx"] = wxUidCross.user_cross_wx;
                                dataRow["userid"] = wxUidCross.userid;
                                dataRow["msgId"] = wxUidCross.wx_info[i].msgId;
                                dataRow["msgSvrId"] = wxUidCross.wx_info[i].msgSvrId;
                                dataRow["type"] = wxUidCross.wx_info[i].type;
                                dataRow["status"] = wxUidCross.wx_info[i].status;
                                dataRow["isSend"] = wxUidCross.wx_info[i].isSend;
                                dataRow["isShowTimer"] = wxUidCross.wx_info[i].isShowTimer;
                                dataRow["createTime"] = wxUidCross.wx_info[i].createTime;
                                dataRow["talker"] = wxUidCross.wx_info[i].talker;
                                dataRow["content"] = wxUidCross.wx_info[i].content;
                                dataRow["imgPath"] = wxUidCross.wx_info[i].imgPath;
                                dataRow["reserved"] = wxUidCross.wx_info[i].reserved;
                                dataRow["transContent"] = wxUidCross.wx_info[i].transContent;
                                dataRow["transBrandWording"] = wxUidCross.wx_info[i].transBrandWording;
                                dataRow["talkerId"] = wxUidCross.wx_info[i].talkerId;
                                dataRow["bizClientMsgId"] = wxUidCross.wx_info[i].bizClientMsgId;
                                dataRow["bizChatId"] = wxUidCross.wx_info[i].bizChatId;
                                dataRow["bizChatUserId"] = wxUidCross.wx_info[i].bizChatUserId;
                                dataTable.Rows.Add(dataRow);
                            }
                        }
                    }
                    new gongneng().wx_msgin(dataTable);
                }
            }
            catch (Exception ex2)
            {
                object obj2 = this.title;
                this.title = string.Concat(new object[]
			{
				obj2,
				"message,json is Err||",
				ex2,
				"messagejson=",
				this.messagejson
			});
                new NetLog().WriteTextLog(this.title + "message", "end");
            }
        }
    }
}