using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace dbConnection.Data
{
    public class DataContextDapper {
        private string _connectionString = "Server=localhost;Database=DotNetCourseDatabase1;TrustServerCertificate=true;Trusted_Connection=false;User id=sa;Password=SQLConnect1!;";

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