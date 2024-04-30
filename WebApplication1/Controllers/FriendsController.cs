using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Friends")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly FriendsContext _context;

        public FriendsController(FriendsContext context)
        {
            _context = context;
        }

        // GET: api/Friends
        [HttpGet]
        public ActionResult<IEnumerable<Friends>> GetFriends()
        {
            return _context.Friends.ToList();
        }

        // GET: api/Friends
        [HttpPost("GetFriendsByUser")]
        public ActionResult<IEnumerable<Users>> GetFriendsByUser([FromBody] Dictionary<String, String> token)
        {
            var userToken = TokenParser.ParseToken(token["token"]);

            if (userToken == null || !TokenParser.IsClient(userToken))
            {
                return Unauthorized();
            }

            Guid user = TokenParser.GetUserId(userToken);

            var friends = _context.Friends
            .Where(f => f.User1 == user || f.User2 == user)
            .ToList();


            // Get the list of users in Friends, removing duplicates and user
            var users = new List<Guid>();
            foreach (var friend in friends)
            {
                if (friend.User1 != user)
                {
                    users.Add(friend.User1);
                }
                if (friend.User2 != user)
                {
                    users.Add(friend.User2);
                }
            }
            users = users.Distinct().ToList();

            // Get the list of users in Users
            var friendsList = _context.Users
                .Where(u => users.Contains(u.Id))
                .ToList();

            return friendsList;
        }

        // POST: api/Friends
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Friends> PostFriends(Friends Friends)
        {
            _context.Friends.Add(Friends);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetFriends), new { id = Friends.User1 }, Friends);
        }

        // DELETE: api/Friends/5/4
        [HttpDelete("{user1}/{user2}")]
        public IActionResult DeleteFriends(Guid user1, Guid user2)
        {
            var friends = _context.Friends
            .Where(f => (f.User1 == user1 && f.User2 == user2) || (f.User1 == user2 && f.User2 == user1))
            .ToList();
            
            if (friends == null)
            {
                return NotFound();
            }

            foreach (var friend in friends) // Normalement, il n'y a qu'un seul élément dans la liste
            {
                _context.Friends.Remove(friend);
            }
            _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
