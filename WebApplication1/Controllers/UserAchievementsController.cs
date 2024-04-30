using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/UserAchievements")]
    [ApiController]
    public class UserAchievementsController : ControllerBase
    {
        private readonly UserAchievementsContext _context;

        public UserAchievementsController(UserAchievementsContext context)
        {
            _context = context;
        }

        // GET: api/UserAchievements
        [HttpGet]
        public ActionResult<IEnumerable<UserAchievements>> GetUserAchievements()
        {
            return _context.UserAchievements.ToList();
        }

        // GET: api/UserAchievements
        [HttpPost("GetAchievementsByUser")]
        public ActionResult<IEnumerable<Achievements>> GetAchievementsByUser([FromBody] Dictionary<String, String> token)
        {
            var userToken = TokenParser.ParseToken(token["token"]);

            if (userToken == null || !TokenParser.IsClient(userToken))
            {
                return Unauthorized();
            }

            Guid user = TokenParser.GetUserId(userToken);

            var userAchievements = _context.UserAchievements
                .Where(ua => ua.IdUser == user)
                .Select(ua => ua.IdAchievement)
                .ToList();

            if (userAchievements == null)
            {
                return NotFound();
            }

            var achievements = _context.Achievements
                .Where(a => userAchievements.Contains(a.Id))
                .ToList();

            return achievements;
        }
       
        // POST: api/UserAchievements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<UserAchievements> PostUserAchievements(UserAchievements UserAchievements)
        {
            _context.UserAchievements.Add(UserAchievements);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUserAchievements), new { id = UserAchievements.IdUser }, UserAchievements);
        }

        // DELETE: api/UserAchievements/5/
        [HttpDelete("{user}/{achiev}")]
        public IActionResult DeleteUserAchievements(Guid user, Guid achiev)
        {
            var userAchievements = _context.UserAchievements.Where(ua => (ua.IdUser == user && ua.IdAchievement == achiev)).ToList();

            if (userAchievements == null)
            {
                return NotFound();
            }

            foreach (var userAchievement in userAchievements) // Normaly userAchievements should contain only one element with user and achiev
            {
                _context.UserAchievements.Remove(userAchievement);
            }
            _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
