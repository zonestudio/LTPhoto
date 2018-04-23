using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LTPhoto.Helpers;
using Newtonsoft.Json;

namespace LTPhoto
{
    public partial class index : BasePage
    {
        public string Json { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            var uinfo = Comm.GetCookie("__uinfo");
            if (!string.IsNullOrEmpty(uinfo))
            {
                Json = uinfo;
            }
            else
            {
                Json = "null";
            }
             
            //var ct = SqlCeHelper.ExecuteScalar(LtDataHelper.CONNECTION_STRING, CommandType.Text,
            //    "SELECT COUNT(*) AS CT FROM [UserPhotoSnap] ");
            //Response.Write(ct);
        }
    }
}