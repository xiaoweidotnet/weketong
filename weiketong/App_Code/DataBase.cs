using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
public class DataBase
{
	private string a;

	private string b;

	private DataTable c;

	public int i;

	public DataBase()
	{
		this.a = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
	}

	public bool IsConnected()
	{
		SqlConnection sqlConnection = new SqlConnection(this.a);
		bool result;
		try
		{
			if (sqlConnection.State != ConnectionState.Open)
			{
				sqlConnection.Open();
			}
			result = true;
		}
		catch
		{
			result = false;
		}
		return result;
	}

	public void DataListBangDing(DataList dl, string sql)
	{
		dl.DataSource = new DataBase().GetDataTable(sql);
		dl.DataBind();
	}

	public string TextBangDing(int id)
	{
		string result;
		try
		{
			result = new DataBase().GetDataTable("select Content from mNewsData where ClassID=" + id.ToString()).Rows[0][0].ToString();
		}
		catch
		{
			result = "该信息已不存在！";
		}
		return result;
	}

	public string TextNameBangDing(int id)
	{
		string result;
		try
		{
			result = new DataBase().GetDataTable("select ClassName from mLanMu where ClassID=" + id.ToString()).Rows[0][0].ToString();
		}
		catch
		{
			result = "该信息已不存在！";
		}
		return result;
	}

	public DataTable GetDataTable(string sql)
	{
		this.c = new DataTable();
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
		DataTable result;
		try
		{
			sqlDataAdapter.Fill(this.c);
			result = this.c;
		}
		catch (Exception)
		{
			new NetLog().WriteTextLog("errsql", sql);
			result = this.c;
		}
		finally
		{
			sqlDataAdapter.Dispose();
			sqlConnection.Close();
		}
		return result;
	}

	public DataTable GetDataTable(string tablename, string condition)
	{
		this.c = new DataTable();
		string text = "select * from " + tablename + " where " + condition;
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(text, sqlConnection);
		DataTable result;
		try
		{
			sqlDataAdapter.Fill(this.c);
			result = this.c;
		}
		catch (Exception)
		{
			new NetLog().WriteTextLog("errsql", text);
			result = this.c;
		}
		finally
		{
			sqlDataAdapter.Dispose();
			sqlConnection.Close();
		}
		return result;
	}

	public void AllInsert(string sql, List<object> key)
	{
		this.c = new DataTable();
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
		try
		{
			sqlDataAdapter.Fill(this.c);
			ArrayList arrayList = new ArrayList();
			foreach (object current in key)
			{
				arrayList.Add(current);
				this.c.Rows.Add(new object[]
				{
					current
				});
			}
			new SqlCommandBuilder(sqlDataAdapter);
			sqlDataAdapter.Update(this.c.GetChanges());
			this.c.AcceptChanges();
		}
		catch
		{
			sqlConnection.Close();
		}
		finally
		{
			sqlDataAdapter.Dispose();
			sqlConnection.Close();
		}
	}

	public int GetDataTableCount(string sql)
	{
		this.c = new DataTable();
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
		int count;
		try
		{
			sqlDataAdapter.Fill(this.c);
			count = this.c.Rows.Count;
		}
		catch (Exception ex)
		{
			throw new Exception("Error in " + ex.ToString());
		}
		finally
		{
			sqlDataAdapter.Dispose();
			sqlConnection.Close();
		}
		return count;
	}

	public bool InsertData(string tablename, string field, string values)
	{
		string text = string.Concat(new string[]
		{
			"insert into ",
			tablename,
			"(",
			field,
			") values (",
			values,
			")"
		});
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
		bool result;
		try
		{
			sqlConnection.Open();
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			result = true;
		}
		catch (Exception ex)
		{
			new NetLog().WriteTextLog(tablename, string.Concat(new object[]
			{
				"sql=",
				text,
				"<<<<<<<<<<<<<<<<<<<<<<<<<,ine=",
				ex,
				"<<<<<<<<<<<<<<<<<<<<<<<<<<<"
			}));
			sqlConnection.Close();
			result = false;
		}
		return result;
	}

	public int UpdataData(string tablename, string values, string condition)
	{
		int result = 0;
		string text = string.Concat(new string[]
		{
			"update ",
			tablename,
			" set ",
			values,
			" where ",
			condition
		});
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand(text, sqlConnection);
		try
		{
			sqlConnection.Open();
			result = sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
		}
		catch (Exception ex)
		{
			new NetLog().WriteTextLog(tablename, string.Concat(new object[]
			{
				"sql=",
				text,
				"<<<<<<<<<<<<<<<<<<<<<<,upe=",
				ex,
				"<<<<<<<<<<<<<<<<<<<<<<<<<<<"
			}));
			sqlConnection.Close();
		}
		return result;
	}

	public bool DeleteData(string tablename, string condition)
	{
		string cmdText = "delete from " + tablename + "  where " + condition;
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection);
		bool result;
		try
		{
			sqlConnection.Open();
			sqlCommand.ExecuteNonQuery();
			sqlConnection.Close();
			result = true;
		}
		catch
		{
			sqlConnection.Close();
			result = false;
		}
		return result;
	}

	public DataSet GetPageDataSet(int PageSize, int CurrentPageIndex, string sql, string tablename)
	{
		DataSet dataSet = null;
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
		try
		{
			dataSet = new DataSet();
			sqlDataAdapter.Fill(dataSet, PageSize * (CurrentPageIndex - 1), PageSize, tablename);
		}
		catch (Exception ex)
		{
			throw new Exception("Error in " + ex.ToString());
		}
		finally
		{
			sqlDataAdapter.Dispose();
			sqlConnection.Close();
		}
		return dataSet;
	}

	public int RecordCount(string sql)
	{
		int result = 0;
		this.c = new DataTable();
		SqlConnection sqlConnection = new SqlConnection(this.a);
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sql, sqlConnection);
		sqlDataAdapter.Fill(this.c);
		try
		{
			result = this.c.Rows.Count;
		}
		catch (Exception ex)
		{
			throw new Exception("Error in " + ex.ToString());
		}
		finally
		{
			sqlDataAdapter.Dispose();
			sqlConnection.Close();
		}
		return result;
	}

	public string site(int id)
	{
		string text = null;
		if (new DataBase().TextNameBangDing(id) == "该信息已不存在！")
		{
			text += "<a href=\"default.aspx\">Home</a>";
		}
		else
		{
			this.i++;
			if (this.i == 1)
			{
				text = this.site(Convert.ToInt32(new DataBase().GetDataTable("select ParentID from mLanMu where ClassID=" + id).Rows[0][0].ToString())) + "><span class=\"color1\">" + new DataBase().TextNameBangDing(id) + "</span>";
			}
			else
			{
				text = this.site(Convert.ToInt32(new DataBase().GetDataTable("select ParentID from mLanMu where ClassID=" + id).Rows[0][0].ToString())) + ">" + new DataBase().TextNameBangDing(id);
			}
		}
		return text;
	}
}
