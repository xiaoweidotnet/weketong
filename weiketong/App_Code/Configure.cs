using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Xml.Serialization;

public class Configure
{
	public static Settings GetSettings()
	{
		HttpContext current = HttpContext.Current;
		Settings settings = (Settings)current.Cache["Config"];
		if (settings == null)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
			try
			{
				string text = HttpContext.Current.Request.PhysicalApplicationPath + "/" + ConfigurationManager.AppSettings["Config"].ToString();
				FileStream fileStream = new FileStream(text, FileMode.Open);
				settings = (Settings)xmlSerializer.Deserialize(fileStream);
				fileStream.Close();
				current.Cache.Insert("Config", settings, new CacheDependency(text));
			}
			catch (FileNotFoundException)
			{
				settings = new Settings();
			}
		}
		return settings;
	}

	public static void SaveSettings(Settings data)
	{
		string path = HttpContext.Current.Request.PhysicalApplicationPath + "/" + ConfigurationManager.AppSettings["Config"].ToString();
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(Settings));
		FileStream fileStream = new FileStream(path, FileMode.Create);
		xmlSerializer.Serialize(fileStream, data);
		fileStream.Close();
	}
}
