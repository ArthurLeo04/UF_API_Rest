using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Ranks")]
    [ApiController]
    public class RanksController : ControllerBase
    {
        private readonly RanksContext _context;

        public RanksController(RanksContext context)
        {
            _context = context;
        }

        // GET: api/Ranks
        [HttpGet]
        public ActionResult<IEnumerable<Ranks>> GetRanks()
        {
            return _context.Ranks.ToList();
        }

        private bool RanksExists(string rank)
        {
            return _context.Ranks.Any(e => e.Rank == rank);
        }
    }
}
