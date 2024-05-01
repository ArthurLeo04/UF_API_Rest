using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        //[Authorize(Roles = "Server")] // Faudra ajouter ça un peut partout
        public ActionResult<IEnumerable<Users>> GetUsers()
        {
            return _context.Users.ToList();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<Users> GetUsersById(Guid id)
        {
            var users = _context.Users.Find(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        [HttpPost("JWT")]
        public ActionResult<Users> GetUserIdByJWT([FromBody] Dictionary<String, String> token)
        {
            var userToken = TokenParser.ParseToken(token["token"]);

            Guid userId = TokenParser.GetUserId(userToken);

            var users = _context.Users.Find(userId);

            if(users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutUsers(Guid id, [FromBody] Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Users> PostUsers(Users users)
        {
            _context.Users.Add(users);
            _context.SaveChanges();

            //return CreatedAtAction("GetUsers", new { id = users.Id }, users);
            return CreatedAtAction(nameof(GetUsers), new { id = users.Id }, users);
        }

        private void GrantAchievement(string name, Guid user)
        {
            List<Guid> userAchievements = _context.UserAchievements
                    .Where(ua => ua.IdUser == user)
                    .Select(ua => ua.IdAchievement)
                    .ToList();

            // The new achievement
            Achievements? achievement = _context.Achievements
                .Where(a => a.Nom == name)
                .FirstOrDefault();

            if (achievement != null && !userAchievements.Contains(achievement.Id))
            {
                var newAchievement = new UserAchievements(user, achievement.Id);
                _context.UserAchievements.Add(newAchievement);
                _context.SaveChanges();
            }
        }

        // POST : api/Users/Dead
        [HttpPost("Dead")]
        public IActionResult IncrementDeath([FromBody] Dictionary<String, String> token)
        {
            var userToken = TokenParser.ParseToken(token["token"]);

            if (userToken == null)
            {
                return Unauthorized();
            }

            Guid user = TokenParser.GetUserId(userToken);
            var users = _context.Users.Find(user);
            if (users == null)
            {
                return NotFound();
            }

            users.DeathCount++;

            // Update the user achievements
            if (users.DeathCount >= 1)
            {
                GrantAchievement("The first of a long series", user);
            }
            if (users.DeathCount >= 10)
            {
                GrantAchievement("Immortal", user);
            }


            _context.SaveChanges();


            return Ok();
        }

        // POST : api/Users/Kill
        [HttpPost("Kill")]
        public IActionResult IncrementKill([FromBody] Dictionary<String, String> token)
        {
            var userToken = TokenParser.ParseToken(token["token"]);

            if (userToken == null)
            {
                return Unauthorized();
            }

            Guid user = TokenParser.GetUserId(userToken);
            var users = _context.Users.Find(user);
            if (users == null)
            {
                return NotFound();
            }

            users.KillCount++;

            // Update the user achievements
            if (users.KillCount >= 1)
            {
                GrantAchievement("Beginner Luck", user);
            }
            if (users.KillCount >= 10)
            {
                GrantAchievement("Rampage", user);
            }

            _context.SaveChanges();

            return Ok();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUsers(Guid id)
        {
            var users = _context.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
