using System;
using System.Collections.Generic;
using System.Linq;

namespace LTPhoto.Helpers
{
    public static class EnumHelper
    {
        public static string ToValueString<T>(this T n) where T:struct 
        {
            return Convert.ToInt32(n).ToString();
        }
        public static int[] ToIntArray<T>(T[] value)
        {
            int[] result = new int[value.Length];
            for (int i = 0; i < value.Length; i++)
                result[i] = Convert.ToInt32(value[i]);
            return result;
        }

        public static T[] FromIntArray<T>(int[] value)
        {
            T[] result = new T[value.Length];
            for (int i = 0; i < value.Length; i++)
                result[i] = (T) Enum.ToObject(typeof(T), value[i]);

            return result;
        }

        public static T Parse<T>(string value)
        {
            if (Enum.IsDefined(typeof(T), value))
                return (T) Enum.Parse(typeof(T), value);

            int num;
            if (int.TryParse(value, out num))
            {
                if (Enum.IsDefined(typeof(T), num))
                    return (T) Enum.ToObject(typeof(T), num);
            }
            return default(T);
        }

        public static T Parse<T>(string value, T defaultValue)
        {
            if (Enum.IsDefined(typeof(T), value))
                return (T) Enum.Parse(typeof(T), value);

            int num;
            if (int.TryParse(value, out num))
            {
                if (Enum.IsDefined(typeof(T), num))
                    return (T) Enum.ToObject(typeof(T), num);
            }

            return defaultValue;
        }

        /// <summary>
        ///  将Enum类型，转成Dictionary
        /// </summary>
        /// <typeparam name="TKey">int or string </typeparam>
        /// <typeparam name="TValue">int or string</typeparam>
        /// <param name="t">enum type</param>
        /// <param name="filters">过滤项列表</param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(Type t, object[] filters = null)
        {
            var datas = Enum.GetNames(t);
            if (filters != null)
            {
                var filternames = filters.Select(p => p.ToString());
                datas = datas.Where(p => !filternames.Contains(p)).ToArray();
            }
            return datas.ToDictionary(
                k => (TKey) Convert.ChangeType(Enum.Parse(t, k), typeof(TKey)),
                v => (TValue) Convert.ChangeType(Enum.Parse(t, v), typeof(TValue)));
        }
    }
}
