using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult<IEnumerable<Achievements>> GetAchievements()
        {
            return _context.Achievements.ToList();
        }

        // GET: api/Achievements/5
        [HttpGet("{id}")]
        public ActionResult<Achievements> GetAchievementsById(Guid id)
        {
            var achievements = _context.Achievements.Find(id);

            if (achievements == null)
            {
                return NotFound();
            }

            return achievements;
        }

        // PUT: api/Achievements/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public IActionResult PutAchievements(Guid id, [FromBody] Achievements achievements)
        {
            if (id != achievements.Id)
            {
                return BadRequest();
            }

            _context.Entry(achievements).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AchievementsExists(id))
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

        // POST: api/Achievements
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Achievements> PostAchievements(Achievements achievements)
        {
            _context.Achievements.Add(achievements);
            _context.SaveChanges();

            //return CreatedAtAction("GetAchievements", new { id = Achievements.Id }, Achievements);
            return CreatedAtAction(nameof(GetAchievements), new { id = achievements.Id }, achievements);
        }

        // DELETE: api/Achievements/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAchievements(Guid id)
        {
            var achievements = _context.Achievements.Find(id);
            if (achievements == null)
            {
                return NotFound();
            }

            _context.Achievements.Remove(achievements);
            _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AchievementsExists(Guid id)
        {
            return _context.Achievements.Any(e => e.Id == id);
        }
    }
}
