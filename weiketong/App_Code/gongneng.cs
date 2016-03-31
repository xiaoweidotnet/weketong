using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public class gongneng
{
	private string a;

	private DataTable b;

	public gongneng()
	{
		this.a = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
	}

	public bool isRepeat(string tablename, string chickname, string chickvalue)
	{
		DataTable dataTable = new DataBase().GetDataTable(string.Concat(new string[]
		{
			"select ",
			chickname,
			" as isnull from ",
			tablename,
			" where ",
			chickname,
			"='",
			chickvalue,
			"'"
		}));
		return dataTable.Rows.Count > 0;
	}

	public int cuchuguocheng(DataSet ds, string zy_type)
	{
		int result = 0;
		DateTime now = DateTime.Now;
		string value = now.ToString("yyyy-MM-dd HH:mm:ss");
		long num = long.Parse(now.ToString("yyyyMMddHHmmss"));
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlCommand.CommandText = "Telnum_In";
		sqlCommand.CommandType = CommandType.StoredProcedure;
		sqlConnection.Open();
		for (int i = 0; i < ds.Tables["[Sheet1$]"].Rows.Count; i++)
		{
			try
			{
				string s = ds.Tables["[Sheet1$]"].Rows[i]["num"].ToString();
				long num2 = long.Parse(s);
				string value2 = ds.Tables["[Sheet1$]"].Rows[i]["name"].ToString();
				sqlCommand.Parameters.Clear();
				sqlCommand.Parameters.Add("@zy_type", zy_type);
				sqlCommand.Parameters.Add("@str_Name", value2);
				sqlCommand.Parameters.Add("@int_Telnum", num2);
				sqlCommand.Parameters.Add("@daoru_date", value);
				sqlCommand.Parameters.Add("@num_bak_id", num);
			}
			catch (Exception)
			{
				new NetLog().WriteTextLog("导入异常数据", ds.Tables["[Sheet1$]"].Rows[i]["num"].ToString());
			}
			sqlCommand.ExecuteNonQuery();
		}
		sqlConnection.Close();
		return result;
	}

	public int zy_back(string dept_id, string user_id)
	{
		string str = "";
		if (dept_id != "" && dept_id != "0")
		{
			str = " and dept_id in (" + dept_id + ")";
		}
		if (user_id != "" && user_id != "0")
		{
			str = " and user_id in (" + user_id + ")";
		}
		return new DataBase().UpdataData("zy_fenfa", "dept_id=0,user_id=0 ", "isused=0 " + str);
	}

	public int wx_msgin(DataTable dt)
	{
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
		DataTable dataTable2 = new DataBase().GetDataTable("select top 20 msgid,user_cross_wx from wx_message where user_cross_wx=" + dt.Rows[0]["user_cross_wx"].ToString() + " order by msgid desc");
		int result = 0;
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		for (int i = dt.Rows.Count - 1; i >= 0; i--)
		{
			int num = 0;
			for (int j = dataTable2.Rows.Count - 1; j >= 0; j--)
			{
				string text = dt.Rows[i]["msgid"].ToString();
				string text2 = dataTable2.Rows[j]["msgid"].ToString();
				if (text == text2)
				{
					num = 1;
					break;
				}
			}
			if (num == 0)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["user_cross_wx"] = dt.Rows[i]["user_cross_wx"];
				dataRow["userid"] = dt.Rows[i]["userid"];
				dataRow["msgId"] = dt.Rows[i]["msgId"];
				dataRow["msgSvrId"] = dt.Rows[i]["msgSvrId"];
				dataRow["type"] = dt.Rows[i]["type"];
				dataRow["status"] = dt.Rows[i]["status"];
				dataRow["isSend"] = dt.Rows[i]["isSend"];
				dataRow["isShowTimer"] = dt.Rows[i]["isShowTimer"];
				dataRow["createTime"] = dt.Rows[i]["createTime"];
				dataRow["talker"] = dt.Rows[i]["talker"];
				dataRow["content"] = dt.Rows[i]["content"];
				dataRow["imgPath"] = dt.Rows[i]["imgPath"];
				dataRow["reserved"] = dt.Rows[i]["reserved"];
				dataRow["transContent"] = dt.Rows[i]["transContent"];
				dataRow["transBrandWording"] = dt.Rows[i]["transBrandWording"];
				dataRow["talkerId"] = dt.Rows[i]["talkerId"];
				dataRow["bizClientMsgId"] = dt.Rows[i]["bizClientMsgId"];
				dataRow["bizChatId"] = dt.Rows[i]["bizChatId"];
				dataRow["bizChatUserId"] = dt.Rows[i]["bizChatUserId"];
				dataTable.Rows.Add(dataRow);
			}
		}
		dt.AcceptChanges();
		SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection);
		try
		{
			sqlBulkCopy.DestinationTableName = "wx_message";
			sqlBulkCopy.ColumnMappings.Add("user_cross_wx", "user_cross_wx");
			sqlBulkCopy.ColumnMappings.Add("userid", "userid");
			sqlBulkCopy.ColumnMappings.Add("msgId", "msgId");
			sqlBulkCopy.ColumnMappings.Add("msgSvrId", "msgSvrId");
			sqlBulkCopy.ColumnMappings.Add("type", "type");
			sqlBulkCopy.ColumnMappings.Add("status", "status");
			sqlBulkCopy.ColumnMappings.Add("isSend", "isSend");
			sqlBulkCopy.ColumnMappings.Add("isShowTimer", "isShowTimer");
			sqlBulkCopy.ColumnMappings.Add("createTime", "createTime");
			sqlBulkCopy.ColumnMappings.Add("talker", "talker");
			sqlBulkCopy.ColumnMappings.Add("content", "content");
			sqlBulkCopy.ColumnMappings.Add("imgPath", "imgPath");
			sqlBulkCopy.ColumnMappings.Add("reserved", "reserved");
			sqlBulkCopy.ColumnMappings.Add("transContent", "transContent");
			sqlBulkCopy.ColumnMappings.Add("transBrandWording", "transBrandWording");
			sqlBulkCopy.ColumnMappings.Add("talkerId", "talkerId");
			sqlBulkCopy.ColumnMappings.Add("bizClientMsgId", "bizClientMsgId");
			sqlBulkCopy.ColumnMappings.Add("bizChatId", "bizChatId");
			sqlBulkCopy.ColumnMappings.Add("bizChatUserId", "bizChatUserId");
			sqlBulkCopy.WriteToServer(dataTable);
		}
		catch (Exception ex)
		{
			new NetLog().WriteTextLog(">>>>>>>>>>>>>wx_msgin", ex.Message);
		}
		finally
		{
			sqlConnection.Close();
		}
		return result;
	}

	public int wx_rcontin(DataTable dt)
	{
		int result = 0;
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			sqlCommand.CommandText = "delete from wx_rcontact where user_cross_wx=" + dt.Rows[i]["user_cross_wx"].ToString();
			int num = sqlCommand.ExecuteNonQuery();
		}
		SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection);
		try
		{
			sqlBulkCopy.DestinationTableName = "wx_rcontact";
			sqlBulkCopy.ColumnMappings.Add("user_cross_wx", "user_cross_wx");
			sqlBulkCopy.ColumnMappings.Add("userid", "userid");
			sqlBulkCopy.ColumnMappings.Add("username", "username");
			sqlBulkCopy.ColumnMappings.Add("alias", "alias");
			sqlBulkCopy.ColumnMappings.Add("conRemark", "conRemark");
			sqlBulkCopy.ColumnMappings.Add("domainList", "domainList");
			sqlBulkCopy.ColumnMappings.Add("nickname", "nickname");
			sqlBulkCopy.ColumnMappings.Add("pyInitial", "pyInitial");
			sqlBulkCopy.ColumnMappings.Add("quanPin", "quanPin");
			sqlBulkCopy.ColumnMappings.Add("showHead", "showHead");
			sqlBulkCopy.ColumnMappings.Add("type", "type");
			sqlBulkCopy.ColumnMappings.Add("weiboFlag", "weiboFlag");
			sqlBulkCopy.ColumnMappings.Add("weiboNickname", "weiboNickname");
			sqlBulkCopy.ColumnMappings.Add("conRemarkPYFull", "conRemarkPYFull");
			sqlBulkCopy.ColumnMappings.Add("conRemarkPYShort", "conRemarkPYShort");
			sqlBulkCopy.ColumnMappings.Add("verifyFlag", "verifyFlag");
			sqlBulkCopy.ColumnMappings.Add("encryptUsername", "encryptUsername");
			sqlBulkCopy.ColumnMappings.Add("chatroomFlag", "chatroomFlag");
			sqlBulkCopy.ColumnMappings.Add("deleteFlag", "deleteFlag");
			sqlBulkCopy.ColumnMappings.Add("contactLabelIds", "contactLabelIds");
			sqlBulkCopy.WriteToServer(dt);
		}
		catch (Exception ex)
		{
			new NetLog().WriteTextLog(">>>>>>>>>>>>>wx_rcontin", ex.Message);
		}
		finally
		{
			sqlCommand.CommandText = string.Concat(new string[]
			{
				"update wx_cust_count set cust_num=(select count(user_cross_wx) from wx_rcontact where user_cross_wx=",
				dt.Rows[0]["user_cross_wx"].ToString(),
				") where user_cross_wx=",
				dt.Rows[0]["user_cross_wx"].ToString(),
				" and user_id=",
				dt.Rows[0]["userid"].ToString(),
				" and thedate='",
				DateTime.Now.ToString("yyyy-MM-dd"),
				"'"
			});
			if (sqlCommand.ExecuteNonQuery() == 0)
			{
				string commandText = string.Concat(new object[]
				{
					"insert into wx_cust_count (cust_num,user_cross_wx,user_id,thedate) values (",
					dt.Rows.Count,
					",",
					dt.Rows[0]["user_cross_wx"].ToString(),
					",",
					dt.Rows[0]["userid"].ToString(),
					",'",
					DateTime.Now.ToString("yyyy-MM-dd"),
					"')"
				});
				sqlCommand.CommandText = commandText;
				int num = sqlCommand.ExecuteNonQuery();
			}
			sqlConnection.Close();
		}
		return result;
	}

	public int wx_img_flag(DataTable dt)
	{
		int result = 0;
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			sqlCommand.CommandText = "delete from wx_img_flag where user_cross_wx=" + dt.Rows[i]["user_cross_wx"].ToString();
			sqlCommand.ExecuteNonQuery();
		}
		SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection);
		try
		{
			sqlBulkCopy.DestinationTableName = "wx_img_flag";
			sqlBulkCopy.ColumnMappings.Add("user_cross_wx", "user_cross_wx");
			sqlBulkCopy.ColumnMappings.Add("userid", "userid");
			sqlBulkCopy.ColumnMappings.Add("username", "username");
			sqlBulkCopy.ColumnMappings.Add("imgflag", "imgflag");
			sqlBulkCopy.ColumnMappings.Add("lastupdatetime", "lastupdatetime");
			sqlBulkCopy.ColumnMappings.Add("reserved1", "reserved1");
			sqlBulkCopy.ColumnMappings.Add("reserved2", "reserved2");
			sqlBulkCopy.ColumnMappings.Add("reserved3", "reserved3");
			sqlBulkCopy.ColumnMappings.Add("reserved4", "reserved4");
			sqlBulkCopy.WriteToServer(dt);
		}
		catch (Exception ex)
		{
			new NetLog().WriteTextLog(">>>>>>>>>>>>>wx_img_flag", ex.Message);
		}
		finally
		{
			sqlConnection.Close();
		}
		return result;
	}

	public int wx_appmessage(DataTable dt)
	{
		int result = 0;
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		sqlCommand.CommandText = "delete from wx_appmessage where user_cross_wx=" + dt.Rows[0]["user_cross_wx"].ToString();
		sqlCommand.ExecuteNonQuery();
		SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection);
		try
		{
			sqlBulkCopy.DestinationTableName = "wx_appmessage";
			sqlBulkCopy.ColumnMappings.Add("user_cross_wx", "user_cross_wx");
			sqlBulkCopy.ColumnMappings.Add("userid", "userid");
			sqlBulkCopy.ColumnMappings.Add("msgid", "msgid");
			sqlBulkCopy.ColumnMappings.Add("appid", "appid");
			sqlBulkCopy.ColumnMappings.Add("title", "title");
			sqlBulkCopy.ColumnMappings.Add("description", "description");
			sqlBulkCopy.ColumnMappings.Add("type", "type");
			sqlBulkCopy.WriteToServer(dt);
		}
		catch (Exception ex)
		{
			new NetLog().WriteTextLog(">>>>>>>>>>>>>wx_appmessage", ex.Message);
		}
		finally
		{
			sqlConnection.Close();
		}
		return result;
	}

	public int wx_netstat(DataTable dt)
	{
		int result = 0;
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		sqlCommand.CommandText = "delete from wx_netstat where user_cross_wx=" + dt.Rows[0]["user_cross_wx"].ToString() + " and ll_date=convert(char(10),GETDATE(),120)";
		sqlCommand.ExecuteNonQuery();
		SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection);
		try
		{
			sqlBulkCopy.DestinationTableName = "wx_netstat";
			sqlBulkCopy.ColumnMappings.Add("user_cross_wx", "user_cross_wx");
			sqlBulkCopy.ColumnMappings.Add("userid", "userid");
			sqlBulkCopy.ColumnMappings.Add("id", "id");
			sqlBulkCopy.ColumnMappings.Add("peroid", "peroid");
			sqlBulkCopy.ColumnMappings.Add("textcountcn", "textcountcn");
			sqlBulkCopy.ColumnMappings.Add("textbytesIn", "textbytesIn");
			sqlBulkCopy.ColumnMappings.Add("imageCountIn", "imageCountIn");
			sqlBulkCopy.ColumnMappings.Add("imageBytesIn", "imageBytesIn");
			sqlBulkCopy.ColumnMappings.Add("voiceCountIn", "voiceCountIn");
			sqlBulkCopy.ColumnMappings.Add("voiceBytesIn", "voiceBytesIn");
			sqlBulkCopy.ColumnMappings.Add("videoCountIn", "videoCountIn");
			sqlBulkCopy.ColumnMappings.Add("videoBytesIn", "videoBytesIn");
			sqlBulkCopy.ColumnMappings.Add("mobileBytesIn", "mobileBytesIn");
			sqlBulkCopy.ColumnMappings.Add("wifiBytesIn", "wifiBytesIn");
			sqlBulkCopy.ColumnMappings.Add("sysMobileBytesIn", "sysMobileBytesIn");
			sqlBulkCopy.ColumnMappings.Add("sysWifiBytesIn", "sysWifiBytesIn");
			sqlBulkCopy.ColumnMappings.Add("textCountOut", "textCountOut");
			sqlBulkCopy.ColumnMappings.Add("textBytesOut", "textBytesOut");
			sqlBulkCopy.ColumnMappings.Add("imageCountOut", "imageCountOut");
			sqlBulkCopy.ColumnMappings.Add("imageBytesOut", "imageBytesOut");
			sqlBulkCopy.ColumnMappings.Add("voiceCountOut", "voiceCountOut");
			sqlBulkCopy.ColumnMappings.Add("voiceBytesOut", "voiceBytesOut");
			sqlBulkCopy.ColumnMappings.Add("videoCountOut", "videoCountOut");
			sqlBulkCopy.ColumnMappings.Add("videoBytesOut", "videoBytesOut");
			sqlBulkCopy.ColumnMappings.Add("mobileBytesOut", "mobileBytesOut");
			sqlBulkCopy.ColumnMappings.Add("wifiBytesOut", "wifiBytesOut");
			sqlBulkCopy.ColumnMappings.Add("sysMobileBytesOut", "sysMobileBytesOut");
			sqlBulkCopy.ColumnMappings.Add("sysWifiBytesOut", "sysWifiBytesOut");
			sqlBulkCopy.ColumnMappings.Add("reserved1", "reserved1");
			sqlBulkCopy.ColumnMappings.Add("reserved2", "reserved2");
			sqlBulkCopy.ColumnMappings.Add("realMobileBytesIn", "realMobileBytesIn");
			sqlBulkCopy.ColumnMappings.Add("realWifiBytesIn", "realWifiBytesIn");
			sqlBulkCopy.ColumnMappings.Add("realMobileBytesOut", "realMobileBytesOut");
			sqlBulkCopy.ColumnMappings.Add("realWifiBytesOut", "realWifiBytesOut");
			sqlBulkCopy.WriteToServer(dt);
		}
		catch (Exception ex)
		{
			new NetLog().WriteTextLog(">>>>>>>>>>>>>wx_netstat", ex.Message);
		}
		finally
		{
			sqlConnection.Close();
		}
		return result;
	}

	public int wx_userinfo(DataTable dt)
	{
		int result = 0;
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		sqlCommand.CommandText = "delete from wx_userinfo where user_cross_wx=" + dt.Rows[0]["user_cross_wx"].ToString();
		sqlCommand.ExecuteNonQuery();
		SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection);
		try
		{
			sqlBulkCopy.DestinationTableName = "wx_userinfo";
			sqlBulkCopy.ColumnMappings.Add("user_cross_wx", "user_cross_wx");
			sqlBulkCopy.ColumnMappings.Add("userid", "userid");
			sqlBulkCopy.ColumnMappings.Add("id", "id");
			sqlBulkCopy.ColumnMappings.Add("type", "type");
			sqlBulkCopy.ColumnMappings.Add("value", "value");
			sqlBulkCopy.WriteToServer(dt);
		}
		catch (Exception ex)
		{
			new NetLog().WriteTextLog(">>>>>>>>>>>>>wx_userinfo", ex.Message);
		}
		finally
		{
			sqlConnection.Close();
		}
		return result;
	}

	public int wx_chatroom(DataTable dt)
	{
		int result = 0;
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		sqlCommand.CommandText = "delete from wx_chatroom where user_cross_wx=" + dt.Rows[0]["user_cross_wx"].ToString();
		sqlCommand.ExecuteNonQuery();
		SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection);
		try
		{
			sqlBulkCopy.DestinationTableName = "wx_chatroom";
			sqlBulkCopy.ColumnMappings.Add("user_cross_wx", "user_cross_wx");
			sqlBulkCopy.ColumnMappings.Add("userid", "userid");
			sqlBulkCopy.ColumnMappings.Add("chatroomname", "chatroomname");
			sqlBulkCopy.ColumnMappings.Add("addtime", "addtime");
			sqlBulkCopy.ColumnMappings.Add("memberlist", "memberlist");
			sqlBulkCopy.ColumnMappings.Add("displayname", "displayname");
			sqlBulkCopy.ColumnMappings.Add("chatroomnick", "chatroomnick");
			sqlBulkCopy.ColumnMappings.Add("roomflag", "roomflag");
			sqlBulkCopy.ColumnMappings.Add("roomowner", "roomowner");
			sqlBulkCopy.ColumnMappings.Add("roomdata", "roomdata");
			sqlBulkCopy.ColumnMappings.Add("isShowname", "isShowname");
			sqlBulkCopy.ColumnMappings.Add("selfDisplayName", "selfDisplayName");
			sqlBulkCopy.ColumnMappings.Add("style", "style");
			sqlBulkCopy.ColumnMappings.Add("chatroomdataflag", "chatroomdataflag");
			sqlBulkCopy.ColumnMappings.Add("modifytime", "modifytime");
			sqlBulkCopy.ColumnMappings.Add("chatroomnotice", "chatroomnotice");
			sqlBulkCopy.ColumnMappings.Add("chatroomnoticeNewVersion", "chatroomnoticeNewVersion");
			sqlBulkCopy.ColumnMappings.Add("chatroomnoticeOldVersion", "chatroomnoticeOldVersion");
			sqlBulkCopy.ColumnMappings.Add("chatroomnoticeEditor", "chatroomnoticeEditor");
			sqlBulkCopy.ColumnMappings.Add("chatroomnoticePublishTime", "chatroomnoticePublishTime");
			sqlBulkCopy.WriteToServer(dt);
		}
		catch (Exception ex)
		{
			new NetLog().WriteTextLog(">>>>>>>>>>>>>wx_chatroom", ex.Message);
		}
		finally
		{
			sqlConnection.Close();
		}
		return result;
	}

	public int wx_userinfo2(DataTable dt)
	{
		int result = 0;
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand();
		sqlCommand.Connection = sqlConnection;
		sqlConnection.Open();
		sqlCommand.CommandText = "delete from wx_userinfo2 where user_cross_wx=" + dt.Rows[0]["user_cross_wx"].ToString();
		sqlCommand.ExecuteNonQuery();
		SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection);
		try
		{
			sqlBulkCopy.DestinationTableName = "wx_userinfo2";
			sqlBulkCopy.ColumnMappings.Add("user_cross_wx", "user_cross_wx");
			sqlBulkCopy.ColumnMappings.Add("userid", "userid");
			sqlBulkCopy.ColumnMappings.Add("sid", "sid");
			sqlBulkCopy.ColumnMappings.Add("type", "type");
			sqlBulkCopy.ColumnMappings.Add("value", "value");
			sqlBulkCopy.WriteToServer(dt);
		}
		catch (Exception ex)
		{
			new NetLog().WriteTextLog(">>>>>>>>>>>>>wx_userinfo2", ex.Message);
		}
		finally
		{
			sqlConnection.Close();
		}
		return result;
	}

	public bool isbaohan(string str)
	{
		bool flag = true;
		flag = (flag && str.IndexOf("@") == -1);
		flag = (flag && str.Substring(0, 3) != "gh_");
		flag = (flag && str.IndexOf("filehelper") == -1);
		flag = (flag && str.IndexOf("qqmail") == -1);
		flag = (flag && str.IndexOf("qqsync") == -1);
		flag = (flag && str.IndexOf("floatbottle") == -1);
		flag = (flag && str.IndexOf("shakeapp") == -1);
		flag = (flag && str.IndexOf("lbsapp") == -1);
		flag = (flag && str.IndexOf("medianote") == -1);
		flag = (flag && str.IndexOf("newsappnewsapp") == -1);
		flag = (flag && str.IndexOf("facebookapp") == -1);
		flag = (flag && str.IndexOf("qqfriend") == -1);
		flag = (flag && str.IndexOf("masssendapp") == -1);
		flag = (flag && str.IndexOf("feedsapp") == -1);
		flag = (flag && str.IndexOf("qmessage") == -1);
		flag = (flag && str.IndexOf("voipapp") == -1);
		flag = (flag && str.IndexOf("officialaccounts") == -1);
		flag = (flag && str.IndexOf("voicevoipapp") == -1);
		flag = (flag && str.IndexOf("voiceinputapp") == -1);
		flag = (flag && str.IndexOf("googlecontact") == -1);
		flag = (flag && str.IndexOf("linkedinplugin") == -1);
		flag = (flag && str.IndexOf("notifymessage") == -1);
		flag = (flag && str.IndexOf("tmessage") == -1);
		flag = (flag && str.IndexOf("fmessage") == -1);
		flag = (flag && str.IndexOf("newsapp") == -1);
		if (str.IndexOf("weixin") != -1)
		{
			flag = (flag && str.Length != 6);
		}
		return flag;
	}

	public bool limit_time(int user_cross_wx, string data_title, int how_min)
	{
		bool result = false;
		DateTime now = DateTime.Now;
		DataTable dataTable = new DataBase().GetDataTable(string.Concat(new object[]
		{
			"select id,user_cross_wx,data_title,lasttime from updata_limit where user_cross_wx=",
			user_cross_wx,
			" and data_title='",
			data_title,
			"' order by id desc"
		}));
		if (dataTable.Rows.Count > 0)
		{
			if (now > Convert.ToDateTime(dataTable.Rows[0]["lasttime"]).AddMinutes((double)how_min))
			{
				new DataBase().UpdataData("updata_limit", "lasttime='" + now + "'", "id=" + dataTable.Rows[0]["id"].ToString());
				result = true;
			}
		}
		else
		{
			bool flag = new DataBase().InsertData("updata_limit", "user_cross_wx, data_title,lasttime", string.Concat(new object[]
			{
				user_cross_wx,
				",'",
				data_title,
				"','",
				now,
				"'"
			}));
			if (flag)
			{
				result = true;
			}
		}
		return result;
	}
}
