using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace LTPhoto.Helpers
{
    public class LtDataHelper
    {
        
       
        // string ceDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "firstSqlServerCe.sdf");
        //const string CONN_STRING = "Data Source=firstSqlServerCe.sdf;encryption mode=platform default;Password=2654;";
        public static string CONNECTION_STRING { get; }
        
        static LtDataHelper()
        {
            string ceDataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sqlcompact/lt.sdf");
            CONNECTION_STRING = $"Data Source={ceDataFilePath};encryption mode=platform default;Password=z@163.com;";
            //初始化数据
            //var list = new string[1000];
            //var i = 0;
            //foreach (var s in list)
            //{
            //    i += 1;
            //    var m = 13600000000 + i;
            //    var sql = "INSERT INTO [UserPhotoSnap] (XM,Mobile,PhotoSnapPath, SnapTime) VALUES ('Wilson" + i +
            //              "', '" + m + "','','" + DateTime.Now + "')";
            //    SqlCeHelper.ExecuteNonQuery(CONNECTION_STRING, CommandType.Text, sql);
            //}
        }
        /// <summary>
        /// 首先建议你使用 SQL Server Management Studio 2012 这个版本来创建SQLCE数据库，
        /// 因为这样才是4.0的版本，我使用2008的版本创建出来是3.5的，所以只能使用SQLCE 3.5的引用。
        /// </summary>
        internal void Upgrade()
        {
            using (SqlCeEngine engine = new SqlCeEngine(CONNECTION_STRING))
            {
                engine.Upgrade();
            }
        }

        public static DataSet GetPhotos()
        {
            return SqlCeHelper.ExecuteDataset(CONNECTION_STRING, CommandType.Text, "SELECT * FROM [UserPhotoSnap]");
        }
        /// <summary>
        /// 分页获取dataset
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static DataSet GetPageDataSet(int pagesize, int pageindex, out int total, string where="")
        {
            var skip = (pageindex - 1) * pagesize;
            var wsql = "SELECT * FROM [UserPhotoSnap] " + where;
            var sql = wsql + " ORDER BY ID  DESC OFFSET " + skip + " ROWS FETCH NEXT " +
                      pagesize +
                      " ROWS ONLY;";
            total = (int)SqlCeHelper.ExecuteScalar(CONNECTION_STRING, CommandType.Text, wsql.Replace("*", "COUNT(*) AS CT"));

            return SqlCeHelper.ExecuteDataset(CONNECTION_STRING, CommandType.Text, sql);
        }

        public static void Save(string xm, string mobile, string picture)
        {

            var sql = "INSERT INTO [UserPhotoSnap] (XM,Mobile,PhotoSnapPath, SnapTime) VALUES ('" + xm.Replace("'","\'") +
                        "', '" + mobile + "','"+picture+"','" + DateTime.Now + "')";
            SqlCeHelper.ExecuteNonQuery(CONNECTION_STRING, CommandType.Text, sql);

        }

        public static void Remove(string id)
        {
            var sql = "DELETE FROM [UserPhotoSnap] WHERE Id="+id;
            SqlCeHelper.ExecuteNonQuery(CONNECTION_STRING, CommandType.Text, sql);
        }
    }
}