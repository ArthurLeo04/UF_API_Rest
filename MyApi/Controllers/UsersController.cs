using Microsoft.AspNetCore.Mvc;
using MyApi.Data;
using MyApi.Models;

namespace MyApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public UsersController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        return _dbContext.users.ToList();
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetUserById(Guid id)
    {
        var user = _dbContext.users.Find(id);
        if(user == null)
        {
            return NotFound();
        }
        return user;
    }

    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        user.SetPassword(user.password);
        _dbContext.users.Add(user);
        _dbContext.SaveChanges();
        return CreatedAtAction(nameof(GetUserById), new { id = user.id }, user);
    }

    [HttpPut("{id}/updateKillCount")]
    public IActionResult UpdateKillCount(Guid id, int newKillCount)
    {
        var user = _dbContext.users.Find(id);
        if(user == null)
        {
            return NotFound();
        }

        user.kill_count = newKillCount;
        try
        {
            _dbContext.SaveChanges();
            return Ok("Kill count updated successfully");
        }
        catch (Exception ex)
        {
            // Log the exception for debugging purposes
            Console.WriteLine($"An error occurred while updating kill count for user {id}: {ex.Message}");
            return StatusCode(500, "An error occurred while updating kill count.");
        }
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        var user = _dbContext.users.Find(id);

        if(user == null)
        {
            return NotFound();
        }
        _dbContext.users.Remove(user);
        _dbContext.SaveChanges();
        return NoContent();
    }
}