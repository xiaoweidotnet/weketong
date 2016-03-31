using System;
using System.IO;
using System.Text;

public class NetLog
{
	public void WriteTextLog(string action, string strMessage)
	{
		string text = AppDomain.CurrentDomain.BaseDirectory + "System\\Log\\";
		DateTime now = DateTime.Now;
		if (!Directory.Exists(text))
		{
			Directory.CreateDirectory(text);
		}
		string path = text + now.ToString("yyyy-MM-dd") + ".System.txt";
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("Time:    " + now.ToString() + "\r\n");
		stringBuilder.Append("Action:  " + action + "\r\n");
		stringBuilder.Append("Message: " + strMessage + "\r\n");
		stringBuilder.Append("-----------------------------------------------------------\r\n\r\n");
		StreamWriter streamWriter;
		if (!File.Exists(path))
		{
			streamWriter = File.CreateText(path);
		}
		else
		{
			streamWriter = File.AppendText(path);
		}
		streamWriter.WriteLine(stringBuilder.ToString());
		streamWriter.Close();
	} 
}
