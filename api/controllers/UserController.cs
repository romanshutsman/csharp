using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {

    public UserController() {}
   
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
