using api.DTO;
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

[HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        string sql = @"
        UPDATE TutorialAppSchema.Users
            SET [FirstName] = '" + user.FirstName + 
                "', [LastName] = '" + user.LastName +
                "', [Email] = '" + user.Email + 
                "', [Gender] = '" + user.Gender + 
                "', [Active] = '" + user.Active + 
            "' WHERE UserId = " + user.UserId;
        
        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        } 

        throw new Exception("Failed to Update User");
    }


    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        string sql = @"
            INSERT INTO TutorialAppSchema.Users(
                [FirstName],
                [LastName],
                [Email],
                [Gender],
                [Active]
            ) VALUES (" +
                "'" + user.FirstName + 
                "', '" + user.LastName +
                "', '" + user.Email + 
                "', '" + user.Gender + 
                "', '" + user.Active + 
            "')";
        
        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        } 

        throw new Exception("Failed to Add User");
    }

        [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.Users 
                WHERE UserId = " + userId.ToString();
        
        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        } 

        throw new Exception("Failed to Delete User");
    }

    [HttpGet("GetUsersJobInfo")]
    public IEnumerable<UserJobInfo> GetUsersJobInfo() {
        string sql = @"SELECT * FROM  TutorialAppSchema.UserJobInfo;";
        return _dapper.LoadData<UserJobInfo>(sql);
    } 

    [HttpGet("GetUsersSalary")]
    public IEnumerable<UserSalary> GetUsersSalary() {
        string sql = @"SELECT * FROM  TutorialAppSchema.UserSalary;";
        return _dapper.LoadData<UserSalary>(sql); 
    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfo(int userId)
    {
        string sql = @"SELECT * FROM  TutorialAppSchema.UserJobInfo WHERE UserId = " + userId.ToString();
        return _dapper.LoadDataSingle<UserJobInfo>(sql);
    }

    
    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetUserSalary(int userId)
    {
        string sql = @"SELECT * FROM  TutorialAppSchema.UserSalary WHERE UserId = " + userId.ToString();
        return _dapper.LoadDataSingle<UserSalary>(sql);
    }
    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary user)
    {

        string query = @"
            UPDATE TutorialAppSchema.UserSalary 
            SET Salary = @Salary
            WHERE UserId = @UserId";


        bool success = _dapper.ExecuteSqlWithParams(query, new {
            Salary = user.Salary,
            UserId = user.UserId
        });
            
        if (success == true)
        {
            return Ok();
        }
        
        throw new Exception("Failed to Update User Salary");
    }

        [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo user)
    {
        string query = @"
            UPDATE TutorialAppSchema.UserJobInfo 
            SET Department = @Department, 
                JobTitle = @JobTitle
            WHERE UserId = @UserId";

        bool success = _dapper.ExecuteSqlWithParams(query, new {
            Department = user.Department,
            JobTitle = user.JobTitle,
            UserId = user.UserId
        });
            
        if (success == true)
        {
            return Ok();
        }
        
        throw new Exception("Failed to Update User Job");
    }

                
}
