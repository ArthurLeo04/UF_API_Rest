// ServerCachingController.cs

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StackExchange.Redis;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/ServerCaching")]
    [ApiController]
    public class ServerCachingController : ControllerBase
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly ServerCachingContext _context;

        public ServerCachingController(IConnectionMultiplexer redisConnection, ServerCachingContext context)
        {
            _redisConnection = redisConnection ?? throw new ArgumentNullException(nameof(redisConnection));
            _context = context;
        }

        [HttpGet("entries")]
        public async Task<IActionResult> GetAllEntries()
        {
            try
            {
                var rDb = _redisConnection.GetDatabase();

                var allKeys = _redisConnection.GetServer(_redisConnection.GetEndPoints()[0]).Keys();

                var entries = new Dictionary<string, ServerCaching>();

                foreach(var key in allKeys)
                {
                    var value = await rDb.StringGetAsync(key);
                    if(value.HasValue)
                    {
                        try
                        {
                            var serverCaching = JsonConvert.DeserializeObject<ServerCaching>(value);
                            entries.Add(key, serverCaching);
                        }
                        catch(Newtonsoft.Json.JsonException ex)
                        {
                            return StatusCode(500, $"An json error occurred : {ex.Message}");
                        }
                    }
                }

                return Ok(entries);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred : {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetFromCache(string key)
        {
            var rDb = _redisConnection.GetDatabase();
            var json = rDb.StringGet(key);
            
            if(json.IsNullOrEmpty)
            {
                return NotFound();
            }

            var serverCaching = JsonConvert.DeserializeObject<ServerCaching>(json);

            return Ok(serverCaching);
        }

        private decimal RankToFloat(string rank)
        {
            if (rank == "Bronze")
            {
                return 10;
            }
            else if (rank == "Silver")
            {
                return 20;
            }
            else if (rank == "Gold")
            {
                return 30;
            }
            else if (rank == "Platinum")
            {
                return 40;
            }
            else if (rank == "Diamond")
            {
                return 50;
            }
            else
            {
                return 0;
            }
        }

        // GET: api/ServerCaching/Matchmaking/5
        [HttpGet("Matchmaking/{id}")]
        public async Task<IActionResult> GetUsersMatch(Guid id)
        {
            var users = _context.Users.Find(id);

            if (users == null)
            {
                return NotFound();
            }

            decimal userPoints = RankToFloat(users.Rank) 
                + (users.KDRatio * 10 -5); // Membre li� au kill/death ratio

            // Get all servers from redis using function GetAllEntries
            var result = await GetAllEntries();
            
            if (result is OkObjectResult okObjectResult)
            {
                if (okObjectResult.Value is not Dictionary<string, ServerCaching> entries)
                {
                    return StatusCode(500, "Result is not a dictionary");
                }

                // Trouve le serveur dans le dictionnaire entries qui a la valeur la plus proche de userPoints et avec un nombre de joueurs inf�rieur � N
                var bestServer = entries.Values
                    .Where(server => server.PlayerNumber < 6) // N = 6, hardcod� ici, mais pourrait �tre un param�tre
                    .OrderBy(server => Math.Abs(RankToFloat(server.AverageRank) - userPoints))
                    .FirstOrDefault();

                return Ok(bestServer);
            }

            return result; // On propage le message d'erreur
        }

        [HttpPost]
        public IActionResult SetToCache(string key, [FromBody] ServerCaching value)
        {
            var rDb = _redisConnection.GetDatabase();
            var serializedValue = JsonConvert.SerializeObject(value);
            rDb.StringSet(key, serializedValue);
            return Ok();
        }

        [HttpPut("update/{key}")]
        public async Task<IActionResult> UpdateEntry(string key, [FromBody] ServerCaching newServerCaching)
        {
            try
            {
                var rDb = _redisConnection.GetDatabase();

                // Serialize the newServerCaching object
                var serializedValue = JsonConvert.SerializeObject(newServerCaching);

                await rDb.StringSetAsync(key, serializedValue);

                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred : {ex.Message}");
            }
        }
    
        [HttpDelete]
        public IActionResult DeleteFromCache(string key)
        {
            var rDb = _redisConnection.GetDatabase();

            if(rDb.KeyDelete(key))
            {
                return Ok();
            }
            else {
                return NotFound();
            }
        }
    }

}