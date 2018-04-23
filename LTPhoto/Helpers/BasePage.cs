using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LTPhoto.Helpers.Attributes;

namespace LTPhoto.Helpers
{
    public class BasePage : Page
    {
        #region ViewStateProperty

        private readonly PropertyInfo[] ViewStateProperties;

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);
            foreach (PropertyInfo property in ViewStateProperties)
            {
                var attributes = (ViewStateProperty[])property.GetCustomAttributes(typeof(ViewStateProperty), false);
                var localName = (string.Empty == attributes[0].ViewStateName) ? property.Name : attributes[0].ViewStateName;
                if (ViewState[localName] != null)
                {
                    property.SetValue(this, ViewState[localName], null);
                }
            }
        }

        protected override object SaveViewState()
        {
            foreach (var property in ViewStateProperties)
            {
                var attributes = (ViewStateProperty[])property.GetCustomAttributes(typeof(ViewStateProperty), false);
                var localName = (string.Empty == attributes[0].ViewStateName) ? property.Name : attributes[0].ViewStateName;
                ViewState[localName] = property.GetValue(this, null);
            }
            return base.SaveViewState();
        }

        public BasePage()
        {
            ViewStateProperties = GetType().GetProperties(
               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public).Where(
                   p => p.GetCustomAttributes(typeof(ViewStateProperty), true).Length > 0).ToArray();
        }

        #endregion

        public T rq<T>(string n, T _defaut = default(T))
        {
            try
            {
              
                return (T)Convert.ChangeType(HttpContext.Current.Request.QueryString[n], typeof(T));
            }
            catch (Exception)
            {
                return _defaut;
            }
        }

        /// <summary>
        /// 辅助判断是否处于可读写行
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public bool IsValidRepeaterRow(ListItemType itemType)
        {
            return new List<ListItemType> { ListItemType.Item, ListItemType.AlternatingItem }.Contains(itemType);
        }

        /// <summary>
        /// 辅助判断是否处于可读写行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsValidRepeaterRow(RepeaterItem item)
        {
            return IsValidRepeaterRow(item.ItemType);
        }

        public void BindDictonary<TKey, TValue>(
            ListControl dp, 
            Type t, 
            string first = null)
        {
            BindDictonary(dp, EnumHelper.ToDictionary<TKey, TValue>(t));
            if (first != null)
            {
                dp.Items.Insert(0, first);
            }

        }

        public void BindDictonary<TKey, TValue>(ListControl dp,
            Dictionary<TKey, TValue> dics,
            string DataTextField = "Value", 
            string DataValueField = "Key")
        {
            dp.DataSource = dics;
            dp.DataTextField = DataTextField;
            dp.DataValueField = DataValueField;
            dp.DataBind();
        }

        public void BindDictonary(ListControl dp, IEnumerable dics, string DataTextField = "Value", string DataValueField = "Key")
        {
            dp.DataSource = dics;
            dp.DataTextField = DataTextField;
            dp.DataValueField = DataValueField;
            dp.DataBind();
        }

        public void BindRepeater<TKey, TValue>(Repeater dp, Dictionary<TKey, TValue> dics)
        {
            dp.DataSource = dics;
            dp.DataBind();
        }
        public void BindRepeater(Repeater dp, IEnumerable datas)
        {
            dp.DataSource = datas;
            dp.DataBind();
        }

        #region 设置获取文本

        /// <summary>
        /// 获取TextBox
        /// </summary>
        /// <param name="tx"></param>
        /// <returns></returns>
        public string tv(TextBox tx)
        {
            return tx.Text.Trim();
        }
     
        /// 设置文本值
        /// </summary>
        /// <param name="tx"></param>
        /// <param name="v"></param>
        public void tv(TextBox tx, string v)
        {
            tx.Text = v.Trim();
        }
     
      

        #endregion


     
    }
}
