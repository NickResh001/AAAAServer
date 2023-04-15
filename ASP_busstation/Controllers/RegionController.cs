using ASP_busstation.DTOs;
using ASP_busstation.Models1;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP_busstation.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class RegionController : ControllerBase
    {
        AspbusstationContext _context;

        public RegionController(AspbusstationContext context)
        {
            _context = context;
        }

        // GET: api/<RegionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> Get()
        {
            return await _context.Regions.ToListAsync();
        }

        // GET api/<RegionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var region = await _context.Regions.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }

        // POST api/<RegionController>
        [HttpPost]
        public async Task<ActionResult<Region>> Post([FromBody] Region region)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Regions.Add(region);
            await _context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = region.RegionId }, region);
        }

        // PUT api/<RegionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RegionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
