using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

public class ChickServer
{
	private string a;

	private DataTable b;

	public ChickServer()
	{
		this.a = ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString;
	}

	public bool System_canused(string work_id)
	{
        //bool result = false;
        //DateTime d = this.overtime();
        //if ((d - DateTime.Now).TotalSeconds >= 0.0)
        //{
        //    result = true;
        //}
        //return result;

        return true;
	}

	public string Decrypt(string toDecrypt, string key)
	{
		string result;
		try
		{
			toDecrypt = new ChickServer().newkey(toDecrypt);
			byte[] bytes = Encoding.UTF8.GetBytes(key);
			byte[] array = Convert.FromBase64String(toDecrypt);
			ICryptoTransform cryptoTransform = new RijndaelManaged
			{
				Key = bytes,
				Mode = CipherMode.ECB,
				Padding = PaddingMode.PKCS7
			}.CreateDecryptor();
			byte[] bytes2 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
			Encoding.UTF8.GetString(bytes2);
			result = Encoding.UTF8.GetString(bytes2);
		}
		catch (Exception)
		{
			result = "";
		}
		return result;
	}

	public static string SendSms()
	{
        //StringBuilder stringBuilder = new StringBuilder();
        //byte[] bytes = Encoding.GetEncoding("GBK").GetBytes(stringBuilder.ToString());
        //return ChickServer.a("www.zh171.com/system/?", bytes);
        return string.Empty;
	}

	private static string ba(string A_0, byte[] A_1)
	{
		string empty = string.Empty;
		string result;
		try
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(A_0);
			httpWebRequest.Timeout = 5000;
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.ContentLength = (long)A_1.Length;
			Stream requestStream = httpWebRequest.GetRequestStream();
			requestStream.Write(A_1, 0, A_1.Length);
			requestStream.Close();
			result = empty;
		}
		catch (Exception ex)
		{
			new NetLog().WriteTextLog("POST发送数据LOG", ex.ToString());
			result = empty;
		}
		return result;
	}

	public string newkey(string key)
	{
		string text = "";
		for (int i = key.Length - 1; i >= 0; i--)
		{
			text += key.Substring(i, 1);
		}
		return text;
	}

	public DateTime overtime()
	{
		DateTime result = default(DateTime);
		string key = "";
		string toDecrypt = "";
		string value = "";
		Settings settings = Configure.GetSettings();
		if (settings != null)
		{
			key = settings.On_Key.ToString();
			toDecrypt = settings.On_Value.ToString();
			value = settings.WebName.ToString();
		}
		string text = new ChickServer().Decrypt(toDecrypt, key);
		if (text.IndexOf(value) > -1)
		{
			text = text.Substring(0, 10);
			result = DateTime.ParseExact(text, "yyyy-MM-dd", CultureInfo.InvariantCulture).AddHours(13.0);
		}
		else
		{
			result = DateTime.Now;
		}
		return result;
	}
}
