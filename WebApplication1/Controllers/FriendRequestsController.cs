using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/FriendRequests")]
    [ApiController]
    public class FriendRequestsController : ControllerBase
    {
        private readonly FriendRequestsContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FriendRequestsController(FriendRequestsContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/FriendRequests/Received
        [HttpGet("Received")]
        [Authorize(Roles = "client")]
        public ActionResult<IEnumerable<Users>> GetFriendRequestsReceived()
        {
            // Donne la liste des demandes d'amis que j'ai reçu en tant qu'utilisateur

            // Get the NameIdentifier claim from httpContextAccessor
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Cast the string to Guid
            Guid userId = Guid.Parse(user);

            var friendRequests = _context.FriendRequests
            .Where(f => f.Receiver == userId)
            .Select(f => f.Sender)
            .ToList();

            // Get the list of users in Users
            var friendRequestsList = _context.Users
                .Where(u => friendRequests.Contains(u.Id))
                .ToList();

            return friendRequestsList;
        }

        // GET: api/FriendRequests/Sent/5
        [HttpGet("Sent/{user}")]
        public ActionResult<IEnumerable<Users>> GetFriendRequestsSent(Guid user)
        {
            var friendRequests = _context.FriendRequests
            .Where(f => f.Sender == user)
            .Select(f => f.Receiver)
            .ToList();

            // Get the list of users in Users
            var friendRequestsList = _context.Users
                .Where(u => friendRequests.Contains(u.Id))
                .ToList();

            return friendRequestsList;
        }

        // POST: api/FriendRequests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<FriendRequests> PostFriendRequests(FriendRequests friendRequests)
        {
            _context.FriendRequests.Add(friendRequests);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetFriendRequestsSent), friendRequests);
        }

        // DELETE: api/FriendRequests/5/4
        [HttpDelete("{sender}/{receiver}")]
        public IActionResult DeleteFriendRequests(Guid sender, Guid receiver)
        {
            var FriendRequests = _context.FriendRequests
            .Where(f => (f.Sender == sender && f.Receiver == receiver))
            .ToList();
            
            if (FriendRequests == null)
            {
                return NotFound();
            }

            foreach (var friend in FriendRequests) // Normalement, il n'y a qu'un seul élément dans la liste
            {
                _context.FriendRequests.Remove(friend);
            }
            _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
