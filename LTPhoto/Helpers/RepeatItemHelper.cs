using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace LTPhoto.Helpers
{
    /// <summary>
    /// 行绑定对象帮助类
    /// </summary>
    public static class RepeatItemHelper
    {
        /// <summary>
        /// 设置Literal值
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ltId"></param>
        /// <param name="value"></param>
        public static void LtValue(this RepeaterItem item, string ltId, object value)
        {
            var lit = item.FindControl(ltId) as Literal;
            lit.Text = value.ToString();
        }

        /// <summary>
        /// 设置Literal值 使用匿名函数
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ltId"></param>
        /// <param name="func"></param>
        public static void LtValue(this RepeaterItem item, string ltId, Func<string> func)
        {
            var lit = item.FindControl(ltId) as Literal;
            var value = func();
            lit.Text = value.ToString();
        }

        /// <summary>
        /// 是否是有效的Repeater数据行
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool IsValidRepeaterRow(this RepeaterItem item)
        {
            return new[] {ListItemType.Item, ListItemType.AlternatingItem}.Contains(item.ItemType);
        }
    }
}