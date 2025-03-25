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
   
    [HttpGet("GetUsers/{testValue}")]
    public string[] GetUsers(string testValue) {
        string[] responseArray = new string[] {
            "test1",
            "test2",
            testValue
        };

        return responseArray;
    }
}
