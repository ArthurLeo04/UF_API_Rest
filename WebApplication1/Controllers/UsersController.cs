using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost]
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
