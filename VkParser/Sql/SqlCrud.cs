using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkParser.src.Sql
{
    internal class SqlCrud
    {
        private readonly string _connectionString;
        private readonly DataAccess _db;
        public SqlCrud()
        {
            _db = new DataAccess();
            _connectionString = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString; ;
        }
        public void LoadData(string name,string url,int played,string target)
        {
            string sql = "INSERT or ignore INTO Playlists Values(@name,@url,@played,@target) ";
            _db.SaveData(sql, new { name,url,played,target }, _connectionString);
        }
    }
}
