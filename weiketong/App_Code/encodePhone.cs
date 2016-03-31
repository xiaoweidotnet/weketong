using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

public class encodePhone
{
	private static string a = "1234567890";

	private static string b = "12345678";

	public static string Encrypt(string sourceString)
	{
		if (string.IsNullOrEmpty(sourceString))
		{
			return string.Empty;
		}
		byte[] bytes = Encoding.Default.GetBytes(encodePhone.b);
		byte[] bytes2 = Encoding.Default.GetBytes(encodePhone.a);
		DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
		string result;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			byte[] bytes3 = Encoding.Default.GetBytes(sourceString);
			try
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(bytes, bytes2), CryptoStreamMode.Write))
				{
					cryptoStream.Write(bytes3, 0, bytes3.Length);
					cryptoStream.FlushFinalBlock();
				}
				result = Convert.ToBase64String(memoryStream.ToArray());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		return result;
	}

	public static string Decrypt(string encryptedString)
	{
		if (string.IsNullOrEmpty(encryptedString))
		{
			return string.Empty;
		}
		byte[] bytes = Encoding.Default.GetBytes(encodePhone.b);
		byte[] bytes2 = Encoding.Default.GetBytes(encodePhone.a);
		DESCryptoServiceProvider dESCryptoServiceProvider = new DESCryptoServiceProvider();
		string @string;
		using (MemoryStream memoryStream = new MemoryStream())
		{
			byte[] array = Convert.FromBase64String(encryptedString);
			try
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(bytes, bytes2), CryptoStreamMode.Write))
				{
					cryptoStream.Write(array, 0, array.Length);
					cryptoStream.FlushFinalBlock();
				}
				@string = Encoding.Default.GetString(memoryStream.ToArray());
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
		return @string;
	}

	public static string ToUrlEncode(string strCode)
	{
		StringBuilder stringBuilder = new StringBuilder();
		byte[] bytes = Encoding.UTF8.GetBytes(strCode);
		Regex regex = new Regex("^[A-Za-z0-9]+$");
		for (int i = 0; i < bytes.Length; i++)
		{
			string text = Convert.ToChar(bytes[i]).ToString();
			if (regex.IsMatch(text))
			{
				stringBuilder.Append(text);
			}
			else
			{
				stringBuilder.Append("%" + Convert.ToString(bytes[i], 16));
			}
		}
		return stringBuilder.ToString();
	}
}
