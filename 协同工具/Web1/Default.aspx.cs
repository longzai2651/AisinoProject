using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CryptCtlDll;

namespace Web1
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Fun1();
        }

        public void Fun1()
        {
            //return "success";
            CryptCtlDll.Common common = new CryptCtlDll.Common();
            if (string.IsNullOrEmpty(Request["zdtbdata"]))
            {
                Response.Write("<script LANGUAGE='javascript'>alert('false');</script>");
            }
            else
            {
                string str1 = Request["zdtbdata"].ToString();
                string str2 = common.UnicodeToString(str1);
                Response.Write("<script LANGUAGE='javascript'>alert('" + str1 +";"+ str2 + "');</script>");
            }

            return;
        }

        protected void Fun1(object sender, EventArgs e)
        {
            Fun1();
        }


    }
}