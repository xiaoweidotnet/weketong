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

namespace web.zy_guanli
{
    public partial class overtime : System.Web.UI.Page
    {
        protected string id = "";

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
                this.id = base.Request.Params["id"];
            }
            catch (Exception arg)
            {
                base.Response.Write("到期时间系统错误。" + arg);
            }
            finally
            {
                base.Response.Write(new ChickServer().overtime());
            }
        }
    }
}