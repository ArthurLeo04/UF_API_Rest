using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Achievements")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private readonly AchievementsContext _context;

        public AchievementsController(AchievementsContext context)
        {
            _context = context;
        }

        // GET: api/Achievements
        [HttpGet]
        [Authorize(Roles = "server")]
        public ActionResult<IEnumerable<Achievements>> GetAchievements()
        {
            return _context.Achievements.ToList();
        }

        // GET: api/Achievements/5
        [HttpGet("{id}")]
        [Authorize(Roles = "server")]
        public ActionResult<Achievements> GetAchievementsById(Guid id)
        {
            var achievements = _context.Achievements.Find(id);

            if (achievements == null)
            {
                return NotFound();
            }

            return achievements;
        }
    }
}
