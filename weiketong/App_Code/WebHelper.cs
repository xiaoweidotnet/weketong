using System;
using System.Web;

public class WebHelper
{
	public static void Alert(string sMessage)
	{
		HttpContext.Current.Response.Write("<script>alert('" + sMessage + "');</script>");
	}

	public static void AlertAndRefresh(string sMessage)
	{
		HttpContext.Current.Response.Write("<script>alert('" + sMessage + "');location.href=location.href</script>");
	}

	public static void AlertAndRedirect(string sMessage, string sURL)
	{
		HttpContext.Current.Response.Write(string.Concat(new string[]
		{
			"<script>alert('",
			sMessage,
			"');location.href='",
			sURL,
			"'</script>"
		}));
	}
}
