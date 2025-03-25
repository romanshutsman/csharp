using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {

    DataContextDapper _dapper;

    public UserController(IConfiguration config) {
        _dapper = new DataContextDapper(config);
        Console.WriteLine();
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection() {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }
   
    [HttpGet("test/{testValue}")]
    public string[] Test(string testValue) {
        string[] responseArray = new string[] {
            "test1",
            "test2",
            testValue
        };

        return responseArray;
    }
   
    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers() {
        string sql = @"SELECT * FROM  TutorialAppSchema.Users;";
        IEnumerable<User> users =  _dapper.LoadData<User>(sql);
        return users;
    }

       
    [HttpGet("GetUser/{id}")]
    public User GetUser(int id) {
        string sql = @"SELECT * FROM  TutorialAppSchema.Users WHERE UserId =" + id.ToString();
   
        User user =  _dapper.LoadDataSingle<User>(sql);
        return user;
    }
}
