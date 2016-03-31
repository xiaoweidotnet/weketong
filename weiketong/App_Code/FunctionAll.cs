using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

public class FunctionAll
{
	public static DataTable dt = null;

	public static string temps = "";

	public static void GetDropListNewsClass(DropDownList ddl, string sql)
	{
		DataTable dataTable = new DataBase().GetDataTable(sql);
		FunctionAll.GetDropListNewsClassBangDing(ddl, "0", "", dataTable);
	}

	public static void GetDropListNewsClass(DropDownList ddl, string sql, string ID)
	{
		DataTable dataTable = new DataBase().GetDataTable(sql);
		FunctionAll.GetDropListNewsClassBangDing(ddl, ID, "", dataTable);
	}

	public static void GetDropListNewsClassBangDing(DropDownList ddl, string ID, string back, DataTable dts)
	{
		string back2 = back + "";
		DataRow[] array = dts.Select("parent_id=" + ID);
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow dataRow = array2[i];
			ListItem listItem = new ListItem();
			listItem.Value = dataRow["lm_id"].ToString();
			if (ID == "0")
			{
				listItem.Text = dataRow["lm_name"].ToString();
			}
			else
			{
				listItem.Text = back + "├ " + dataRow["lm_name"].ToString();
			}
			ddl.Items.Add(listItem);
			FunctionAll.GetDropListNewsClassBangDing(ddl, dataRow["lm_id"].ToString(), back2, dts);
		}
	}

	public static bool GetNewsClassIsChildClass(string ID)
	{
		DataTable dataTable = new DataBase().GetDataTable("select ClassID from mNewsClass where ParentID=" + ID);
		return dataTable.Rows.Count != 0;
	}

	public static string GetDropListRedoutClass(string sql, string href)
	{
		FunctionAll.temps = "";
		string str = "<SELECT name=select2 onchange=javascript:location.href='" + href + "?cid='+this.options[this.selectedIndex].value size=1 style='width: 200px'>";
		DataTable dataTable = new DataBase().GetDataTable(sql);
		FunctionAll.GetDropListRedoutClassBangDing("0", "", dataTable);
		if (FunctionAll.temps.Trim() == "")
		{
			FunctionAll.temps = "<option value='0' selected>无子类别</option>";
		}
		FunctionAll.temps += "</select>";
		return str + FunctionAll.temps;
	}

	public static void GetDropListRedoutClassBangDing(string ID, string back, DataTable dts)
	{
		try
		{
			string back2 = back + "";
			DataRow[] array = dts.Select("ParentID=" + ID);
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				string text = FunctionAll.temps;
				FunctionAll.temps = string.Concat(new string[]
				{
					text,
					"<option value='",
					dataRow["ClassID"].ToString().Trim(),
					"' selected>",
					back,
					"├ ",
					dataRow["ClassName"].ToString(),
					"</option>"
				});
				FunctionAll.GetDropListRedoutClassBangDing(dataRow["ClassID"].ToString(), back2, dts);
			}
		}
		catch
		{
		}
	}

	public static void GetDropListProClass(DropDownList ddl, string sql)
	{
		DataTable dataTable = new DataBase().GetDataTable(sql);
		FunctionAll.GetDropListProClassBangDing(ddl, "0", "", dataTable);
	}

	public static void GetDropListProClass(DropDownList ddl, string sql, string ID)
	{
		DataTable dataTable = new DataBase().GetDataTable(sql);
		FunctionAll.GetDropListProClassBangDing(ddl, ID, "", dataTable);
	}

	public static void GetDropListProClassBangDing(DropDownList ddl, string ID, string back, DataTable dts)
	{
		string back2 = back + "";
		DataRow[] array = dts.Select("ParentID=" + ID);
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow dataRow = array2[i];
			ListItem listItem = new ListItem();
			listItem.Value = dataRow["ClassID"].ToString();
			if (ID == "0")
			{
				listItem.Text = dataRow["ClassName"].ToString();
			}
			else
			{
				listItem.Text = back + "├ " + dataRow["ClassName"].ToString();
			}
			ddl.Items.Add(listItem);
			FunctionAll.GetDropListProClassBangDing(ddl, dataRow["ClassID"].ToString(), back2, dts);
		}
	}

	public static bool GetProsClassIsChildClass(string ID)
	{
		DataTable dataTable = new DataBase().GetDataTable("select ClassID from mProClass where ParentID=" + ID);
		return dataTable.Rows.Count != 0;
	}

	public static string GetClassBigSmall(string ID, string Table)
	{
		string text = "";
		DataTable dataTable = new DataBase().GetDataTable("select ClassID from " + Table + " where ParentID=" + ID);
		foreach (DataRow dataRow in dataTable.Rows)
		{
			string text2 = "";
			FunctionAll.GetClassBigSmall(text2, dataRow["ClassID"].ToString(), Table);
			text = text + dataRow["ClassID"].ToString() + ",";
			text += text2;
		}
		return text;
	}

	public static void GetClassBigSmall(string temp, string ID, string Table)
	{
		DataTable dataTable = new DataBase().GetDataTable("select ClassID from " + Table + " where ParentID=" + ID);
		foreach (DataRow dataRow in dataTable.Rows)
		{
			FunctionAll.GetClassBigSmall(temp, dataRow["ClassID"].ToString(), Table);
			temp = temp + dataRow["ClassID"].ToString() + ",";
		}
	}

	public static string GetMu()
	{
		DateTime.Now.ToLongDateString();
		if (!Directory.Exists(HttpContext.Current.Server.MapPath("../UploadFile/" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString())))
		{
			Directory.CreateDirectory(HttpContext.Current.Server.MapPath("../UploadFile/" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString()));
		}
		return string.Concat(new string[]
		{
			"UploadFile/",
			DateTime.Now.Year.ToString(),
			"-",
			DateTime.Now.Month.ToString(),
			"/"
		});
	}

	public static string GetMu(string Mapths)
	{
		DateTime.Now.ToLongDateString();
		if (!Directory.Exists(HttpContext.Current.Server.MapPath("../UploadFile/" + Mapths)))
		{
			Directory.CreateDirectory(HttpContext.Current.Server.MapPath("../UploadFile/" + Mapths));
		}
		return "UploadFile/" + Mapths + "/";
	}

	public static string GetLanMuType(string id)
	{
		string result = "";
		if (int.Parse(id) == 0)
		{
			result = "无";
		}
		else if (int.Parse(id) == 1)
		{
			result = "新闻";
		}
		else if (int.Parse(id) == 2)
		{
			result = "文本";
		}
		else if (int.Parse(id) == 3)
		{
			result = "超链接";
		}
		else if (int.Parse(id) == 4)
		{
			result = "相册";
		}
		else if (int.Parse(id) == 5)
		{
			result = "留言板";
		}
		else if (int.Parse(id) == 6)
		{
			result = "友情链接";
		}
		return result;
	}

	public static string GetUrlClass(string tablename, string ID)
	{
		FunctionAll.temps = "";
		FunctionAll.GetUrlClass(tablename, ID);
		return FunctionAll.temps;
	}

	public static void GetUrlClass(string tablename, string ID, string temp)
	{
		DataRow dataRow = new DataBase().GetDataTable("select * from " + tablename + " where ClassID=" + ID).Rows[0];
		if (int.Parse(dataRow["ParentID"].ToString()) != 0)
		{
			string text = FunctionAll.temps;
			FunctionAll.temps = string.Concat(new string[]
			{
				text,
				"<a href='admin_lanmu.aspx?cid=",
				dataRow["ClassID"].ToString(),
				"'>",
				dataRow["ClassName"].ToString(),
				"</a>"
			});
			FunctionAll.GetUrlClass(tablename, dataRow["ParentID"].ToString(), temp);
		}
	}
}
