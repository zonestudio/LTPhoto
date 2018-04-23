using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LTPhoto.Helpers.Attributes
{
    /*
     * 测试自定义属性
       public class MyEntity {
        public MyEntity() {
            Name = "Jude";
            Age = 11;
        }
        [Name("姓名")]
        public string Name { get; set; }
        [Name("年龄")]
        public int Age { get; set; }
     }
     public enum MyEnum {
        [Name("欧洲")]
        Europe = 0,
        [Name("亚洲")]
        Asia = 1,
        [Name("美洲")]
        America = 2
    }
     * 
     * 
     * 
     * 获取
     *   AttributeHelper<MyEntity> myEntityAttr = new AttributeHelper<MyEntity>();
            MyEntity myEntity = new MyEntity();
            AttributeHelper<MyEnum> myEnumAttr = new AttributeHelper<MyEnum>();
            Response.Write(myEntityAttr.NameFor(it => it.Name) + ":" + myEntity.Name + "\n");//姓名:Jude
            Response.Write(myEntityAttr.NameFor(it => it.Age) + ":" + myEntity.Age + "\n");//年龄:11
            Response.Write(myEntityAttr.PropertyNameFor(it => it.Name) + ":" + myEntity.Name + "\n");//Name:Jude
            Response.Write(myEntityAttr.PropertyNameFor(it => it.Age) + ":" + myEntity.Age + "\n");//Age:11
           
            Response.Write(myEnumAttr.NameFor(MyEnum.America) + ":" + MyEnum.America + "\n");//美洲:America
            Response.Write(myEnumAttr.NameFor(MyEnum.Asia) + ":" + MyEnum.Asia + "\n");//亚洲:Asia
            Response.Write(myEnumAttr.NameFor(MyEnum.Europe) + ":" + MyEnum.Europe + "\n");//欧洲:Europe
     */
    /// <summary>
    /// 获取自定义属性Name或者属性名称
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class AttributeHelper<T> where T : new()
    {
        /// <summary>
        /// 获取枚举类型自定义属性Name
        /// </summary>
        /// <param name="type">枚举</param>
        /// <returns></returns>
        public string NameFor(object type, int index = 0)
        {
            T test = (T)type;
            FieldInfo fieldInfo = test.GetType().GetField(test.ToString());
            object[] attribArray = fieldInfo.GetCustomAttributes(false);
         
            string des = (attribArray[index] as NameAttribute).Description;
            return des;
        }

        /// <summary>
        /// 获取一个类型中的，属性与特性值对应
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public Dictionary<string, string> NameForToDictionary(Type type=null)
        {
            var fieldInfos = type.GetFields();
            var dic = new Dictionary<string, string>();
            foreach (var fieldInfo in fieldInfos)
            {
                var attr = fieldInfo.GetCustomAttributes(typeof (NameAttribute), false);
                if (attr.Length > 0)
                {
                    dic.Add(fieldInfo.Name, ((NameAttribute) attr.First()).Description);
                }
            }
            return dic;
        }
        public Dictionary<string, string> PropertysToDictionary()
        {
            var type = new T().GetType();

            var fieldInfos = type.GetProperties();
            var dic = new Dictionary<string, string>();

            foreach (var fieldInfo in fieldInfos)
            {
                var attr = fieldInfo.GetCustomAttributes(typeof(NameAttribute), false);
                if (attr.Length > 0)
                {
                    dic.Add(fieldInfo.Name, ((NameAttribute)attr.First()).Description);
                }
            }
            return dic;
        }
     
        /// <summary>
        /// 获取属性自定义属性Name
        /// </summary>
        /// <param name="predicate">表达式</param>
        /// <returns></returns>
        public string NameFor(Expression<Func<T, object>> expr)
        {
            string name = PropertyNameFor(expr);
            T et = new T();
            Type type = et.GetType();
            PropertyInfo[] properties = type.GetProperties();
            object[] attributes = null;
            foreach (PropertyInfo p in properties)
            {
                if (p.Name == name)
                {
                    attributes = p.GetCustomAttributes(typeof(NameAttribute), true);
                    break;
                }
            }
            string des = ((NameAttribute)attributes[0]).Description;
            return des;
        }

        /// <summary>
        /// 获取属性的名称
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public string PropertyNameFor(Expression<Func<T, object>> expr)
        {
            var rtn = "";
            if (expr.Body is UnaryExpression)
            {
                rtn = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            }
            else if (expr.Body is MemberExpression)
            {
                rtn = ((MemberExpression)expr.Body).Member.Name;
            }
            else if (expr.Body is ParameterExpression)
            {
                rtn = ((ParameterExpression)expr.Body).Type.Name;
            }
            return rtn;
        }
    }
}
