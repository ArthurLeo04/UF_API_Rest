using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/Roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RolesContext _context;

        public RolesController(RolesContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public ActionResult<IEnumerable<Roles>> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
