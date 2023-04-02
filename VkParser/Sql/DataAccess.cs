using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkParser.src.Sql
{
    internal class DataAccess
    {
        public List<T> LoadData<T, U>(string sqlStatement, U parametrs, string connectionString)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sqlStatement, parametrs).ToList();
                return rows;
            }
        }

        public void SaveData<T>(string sqlStatement, T parametrs, string connectionString)
        {
            using (IDbConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Execute(sqlStatement, parametrs);
            }
        }
    }
}
