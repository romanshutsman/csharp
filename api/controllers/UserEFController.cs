using AutoMapper;
using api.Data;
using api.DTO;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    DataContextEF _entityFramework;    
    IMapper _mapper;
    IUserRepository _userRepository;

    public UserEFController(IConfiguration config, IUserRepository userRepository)
    {
        _entityFramework = new DataContextEF(config);

        _userRepository = userRepository;

        _mapper = new Mapper(new MapperConfiguration(cfg =>{
            cfg.CreateMap<UserToAddDto, User>();
            cfg.CreateMap<UserSalary, UserSalary>().ReverseMap();
            cfg.CreateMap<UserJobInfo, UserJobInfo>().ReverseMap();
        }));

    }

    [HttpGet("GetUsers")]
    // public IEnumerable<User> GetUsers()
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _entityFramework.Users.ToList<User>();
        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        User? user = _entityFramework.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefault<User>();

        if (user != null)
        {
            return user;
        }
        
        throw new Exception("Failed to Get User");
    }
    
    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
        User? userDb = _entityFramework.Users
            .Where(u => u.UserId == user.UserId)
            .FirstOrDefault<User>();
            
        if (userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            } 

            throw new Exception("Failed to Update User");
        }
        
        throw new Exception("Failed to Get User");
    }


    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        User userDb = _mapper.Map<User>(user);
        
        _entityFramework.Add(userDb);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        } 

        throw new Exception("Failed to Add User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        User? userDb = _entityFramework.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefault<User>();
            
        if (userDb != null)
        {
            _entityFramework.Users.Remove(userDb);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            } 

            throw new Exception("Failed to Delete User");
        }
        
        throw new Exception("Failed to Get User");
    }


    [HttpGet("GetUsersJobInfo")]
    public IEnumerable<UserJobInfo> GetUsersJobInfo() {
        return _entityFramework.UserJobInfo.ToList<UserJobInfo>();
    }

    [HttpGet("GetUsersSalary")]
    public IEnumerable<UserSalary> GetUsersSalary() {
        return _entityFramework.UserSalary.ToList<UserSalary>();
    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfo(int userId)
    {
        UserJobInfo? user = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserJobInfo>();

        if (user != null) return user;
        
        throw new Exception("Failed to GetUserJobInfo");
    }

    
    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetUserSalary(int userId)
    {
        UserSalary? user = _entityFramework.UserSalary
            .Where(u => u.UserId == userId)
            .FirstOrDefault<UserSalary>();

        if (user != null) return user;
        
        throw new Exception("Failed to GetUserSalary");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary user)
    {
        UserSalary? userDb = _entityFramework.UserSalary
            .Where(u => u.UserId == user.UserId)
            .FirstOrDefault<UserSalary>();
            
        if (userDb != null)
        {
            userDb.Salary = user.Salary;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            } 

            throw new Exception("Failed to Update User Salary");
        }
        
        throw new Exception("Failed to Get User Salary");
    }

        [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo user)
    {
        UserJobInfo? userDb = _entityFramework.UserJobInfo
            .Where(u => u.UserId == user.UserId)
            .FirstOrDefault<UserJobInfo>();
            
        if (userDb != null)
        {
            userDb.JobTitle = user.JobTitle;
            userDb.Department = user.Department;
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            } 

            throw new Exception("Failed to Update UserJobInfo");
        }
        
        throw new Exception("Failed to Get UserJobInfo");
    }



}
