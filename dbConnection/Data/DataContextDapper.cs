using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace dbConnection.Data
{
    public class DataContextDapper {
        private IConfiguration _config;
        private string _connectionString;
        public DataContextDapper(IConfiguration config) {
            _config = config;
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<T> LoadData<T>(string sql) {
            IDbConnection databaseConn = new SqlConnection(_connectionString);
            return databaseConn.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql) {
            IDbConnection databaseConn = new SqlConnection(_connectionString);
            return databaseConn.QuerySingle<T>(sql);
        }


        public bool ExecuteSql(string sql) {
            IDbConnection databaseConn = new SqlConnection(_connectionString);
            return databaseConn.Execute(sql) > 0;
        }

    }
}