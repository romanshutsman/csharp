using System.Data;
using api.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace api 
{
    class DataContextDapper {
        private readonly IConfiguration _config;
        public DataContextDapper(IConfiguration config) { 
            _config = config;
            // this.WriteUsers();
            // this.WriteUsersJobInfo();
            // this.WriteUsersSalary();
        }

        public IEnumerable<T> LoadData<T>(string sql) {
        
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql);
        }
        public T LoadDataSingle<T>(string sql) {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql) {
            IDbConnection databaseConn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return databaseConn.Execute(sql) > 0;
        }
        public bool ExecuteSqlWithParams(string sql, object parameters)
        {
            using IDbConnection databaseConn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return databaseConn.Execute(sql, parameters) > 0;
        }


        public void WriteUsers() {
            string usersJson = File.ReadAllText("Users.json");
        
            IEnumerable<User>? users = JsonConvert.DeserializeObject<IEnumerable<User>>(usersJson);
            if (users != null) {
                foreach (var item in users)
                {                    
                    string query = @"
                        INSERT INTO TutorialAppSchema.Users 
                            (FirstName, LastName, Email, Gender, Active)
                        VALUES 
                            (@FirstName, @LastName, @Email, @Gender, @Active);
                    ";

                    ExecuteSqlWithParams(query, new {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        Gender = item.Gender,
                        Active = item.Active
                    });
                }
            }
        }
        public void WriteUsersJobInfo() {
            string usersJson = File.ReadAllText("UserJobInfo.json");
        
            IEnumerable<UserJobInfo>? users = JsonConvert.DeserializeObject<IEnumerable<UserJobInfo>>(usersJson);
            if (users != null) {
                foreach (var item in users)
                {                    
                    string query = @"
                        INSERT INTO TutorialAppSchema.UserJobInfo 
                            (UserId, Department, JobTitle)
                        VALUES 
                            (@UserId, @Department, @JobTitle);
                    ";

                    ExecuteSqlWithParams(query, new {
                        UserId = item.UserId,
                        Department = item.Department,
                        JobTitle = item.JobTitle,
                    });
                }
            }
        }

        public void WriteUsersSalary() {
            string usersJson = File.ReadAllText("UserSalary.json");
        
            IEnumerable<UserSalary>? users = JsonConvert.DeserializeObject<IEnumerable<UserSalary>>(usersJson);
            if (users != null) {
                foreach (var item in users)
                {                    
                    string query = @"
                        INSERT INTO TutorialAppSchema.UserSalary 
                            (UserId, Salary)
                        VALUES 
                            (@UserId, @Salary);
                    ";

                    ExecuteSqlWithParams(query, new {
                        UserId = item.UserId,
                        Salary = item.Salary
                    });
                }
            }
        }
    }
    
}