using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LTPhoto.Helpers;
using LTPhoto.Helpers.Attributes;

namespace LTPhoto
{
    public partial class admin : BasePage
    {
        [ViewStateProperty]
        public string Searchkey { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pageindex = rq<int>("page", 1);
                BindData(pageindex);

            }

        }

        private void BindData(int pageindex)
        {
            var total = 0;
            var pagesize = 10;
            Searchkey = tv(txKey).Trim();
            var where = "";
            if (!string.IsNullOrEmpty(Searchkey))
            {
                where = $" WHERE XM LIKE '%{Searchkey}%' OR Mobile LIKE '%{Searchkey}%' ";
            }
            var ds = LtDataHelper.GetPageDataSet(pagesize, pageindex, out total,where);
            if (pageindex == 1) pager1.RecordCount = total;
            pager1.PageSize = pagesize;
            pager1.CurrentPageIndex = pageindex;

            rp.DataSource = ds.Tables[0];
            rp.DataBind();
        }


        protected void AspNetPager4_OnPageChanged(object sender, EventArgs e)
        {
            BindData(pager1.CurrentPageIndex);
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            var b = sender as Button;
            var id = b.CommandArgument.Split('|')[0];
            if (!string.IsNullOrEmpty(id))
            {
                LtDataHelper.Remove(id);
                var path = b.CommandArgument.Split('|')[1];
                var local = PathHelper.MapPath(path);
                //删除
                if (File.Exists(local))
                {
                    try
                    {
                         File.Delete(local);
                    }
                    catch (Exception exception)
                    {
                        //ingorn
                    }
                }
                BindData(pager1.CurrentPageIndex);
            }
        }

        protected void rp_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!IsValidRepeaterRow(e.Item)) return;
            var da = (DataRowView)e.Item.DataItem;
            var id = da["Id"].ToString(); var path = da["PhotoSnapPath"].ToString();
            var btnDelete = e.Item.FindControl("btnDelete") as Button;
            btnDelete.CommandArgument = id+"|"+path;
           
          
        }

        protected void lkCancel_OnClick(object sender, EventArgs e)
        {
            txKey.Text = "";
            BindData(1);
            lkCancel.Visible = false;
        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tv(txKey))) return;
            BindData(1);
            lkCancel.Visible = true;
        }

        protected string Sugg(object eval)
        {
            if (string.IsNullOrEmpty(Searchkey)) return eval.ToString();

            return eval.ToString().Replace(Searchkey, "<span class='match'>" + Searchkey + "</span>");
        }
    }
}