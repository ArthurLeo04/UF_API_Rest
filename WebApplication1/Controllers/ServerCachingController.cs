// ServerCachingController.cs

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
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

        public ServerCachingController(IConnectionMultiplexer redisConnection)
        {
            _redisConnection = redisConnection ?? throw new ArgumentNullException(nameof(redisConnection));
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