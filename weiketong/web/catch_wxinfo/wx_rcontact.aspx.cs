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
    public partial class wx_rcontact : System.Web.UI.Page
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


            public string username
            {
                get;
                set;
            }

            public string alias
            {
                get;
                set;
            }

            public string conRemark
            {
                get;
                set;
            }

            public string domainList
            {
                get;
                set;
            }

            public string nickname
            {
                get;
                set;
            }

            public string pyInitial
            {
                get;
                set;
            }

            public string quanPin
            {
                get;
                set;
            }

            public long showHead
            {
                get;
                set;
            }

            public long type
            {
                get;
                set;
            }

            public long weiboFlag
            {
                get;
                set;
            }

            public string weiboNickname
            {
                get;
                set;
            }

            public string conRemarkPYFull
            {
                get;
                set;
            }

            public string conRemarkPYShort
            {
                get;
                set;
            }

            public long verifyFlag
            {
                get;
                set;
            }

            public string encryptUsername
            {
                get;
                set;
            }

            public long chatroomFlag
            {
                get;
                set;
            }

            public long deleteFlag
            {
                get;
                set;
            }

            public string contactLabelIds
            {
                get;
                set;
            }
        }

        protected string rcontactjson = "";

        protected string title = "";

        protected string log_msg = "";

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
                this.rcontactjson = base.Request.Form["rcontact"];
                this.rcontactjson = HttpUtility.UrlDecode(this.rcontactjson, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                object obj = this.title;
                this.title = string.Concat(new object[]
			{
				obj,
				"Request is Err||",
				ex,
				"rcontactjson=",
				this.rcontactjson
			});
            }
            try
            {
                WxUidCross wxUidCross = new JavaScriptSerializer
                {
                    MaxJsonLength = 2147483647
                }.Deserialize<WxUidCross>(this.rcontactjson);
                if (new gongneng().limit_time(wxUidCross.user_cross_wx, "rcontactjson", 35))
                {
                    new DataSet();
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("user_cross_wx", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("userid", Type.GetType("System.Int32")));
                    dataTable.Columns.Add(new DataColumn("username", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("alias", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("conRemark", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("domainList", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("nickname", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("pyInitial", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("quanPin", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("showHead", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("type", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("weiboFlag", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("weiboNickname", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("conRemarkPYFull", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("conRemarkPYShort", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("verifyFlag", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("encryptUsername", Type.GetType("System.String")));
                    dataTable.Columns.Add(new DataColumn("chatroomFlag", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("deleteFlag", Type.GetType("System.Int64")));
                    dataTable.Columns.Add(new DataColumn("contactLabelIds", Type.GetType("System.String")));
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
                                dataRow["alias"] = wxUidCross.wx_info[i].alias;
                                dataRow["conRemark"] = wxUidCross.wx_info[i].conRemark;
                                dataRow["domainList"] = wxUidCross.wx_info[i].domainList;
                                dataRow["nickname"] = wxUidCross.wx_info[i].nickname;
                                dataRow["pyInitial"] = wxUidCross.wx_info[i].pyInitial;
                                dataRow["quanPin"] = wxUidCross.wx_info[i].quanPin;
                                dataRow["showHead"] = wxUidCross.wx_info[i].showHead;
                                dataRow["type"] = wxUidCross.wx_info[i].type;
                                dataRow["weiboFlag"] = wxUidCross.wx_info[i].weiboFlag;
                                dataRow["weiboNickname"] = wxUidCross.wx_info[i].weiboNickname;
                                dataRow["conRemarkPYFull"] = wxUidCross.wx_info[i].conRemarkPYFull;
                                dataRow["conRemarkPYShort"] = wxUidCross.wx_info[i].conRemarkPYShort;
                                dataRow["verifyFlag"] = wxUidCross.wx_info[i].verifyFlag;
                                dataRow["encryptUsername"] = wxUidCross.wx_info[i].encryptUsername;
                                dataRow["chatroomFlag"] = wxUidCross.wx_info[i].chatroomFlag;
                                dataRow["deleteFlag"] = wxUidCross.wx_info[i].deleteFlag;
                                dataRow["contactLabelIds"] = wxUidCross.wx_info[i].contactLabelIds;
                                dataTable.Rows.Add(dataRow);
                            }
                            else
                            {
                                this.log_msg = this.log_msg + "剔除群中好友和系统好友" + wxUidCross.wx_info[i].username.ToString() + ">>>>>>>>";
                            }
                        }
                    }
                    if (this.log_msg.Length > 0)
                    {
                        new NetLog().WriteTextLog(wxUidCross.user_cross_wx.ToString() + "rcontact:", this.log_msg);
                    }
                    new gongneng().wx_rcontin(dataTable);
                }
            }
            catch (Exception ex2)
            {
                object obj2 = this.title;
                this.title = string.Concat(new object[]
			{
				obj2,
				"rcontact,Json is Err||",
				ex2,
				"rcontactjson=",
				this.rcontactjson,
				"<<<<<<<<<<<<<<<"
			});
                new NetLog().WriteTextLog(this.title + "rcontact", "end");
            }
        }
    }
}