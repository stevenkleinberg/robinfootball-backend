using Microsoft.AspNetCore.Mvc;
using RobinFootball.API.Data;
using RobinFootball.API.Models;

namespace RobinFootball.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlayersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/players
        [HttpGet]
        public ActionResult<IEnumerable<Player>> GetPlayers()
        {
            return Ok(_context.Players.ToList());
        }

        // GET: api/players/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null) return NotFound();
            return player;
        }

        //POST: api/players
        [HttpPost]
        public async Task<ActionResult<Player>> CreatePlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
        }
        //PUT /api/Players/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlayer(int id, Player updatedPlayer)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            player.Name = updatedPlayer.Name;
            player.Position = updatedPlayer.Position;
            player.Team = updatedPlayer.Team;
            player.InitialValue = updatedPlayer.InitialValue;
            player.CurrentValue = updatedPlayer.CurrentValue;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/Players/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
