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
        /// <summary>
        /// Осуществляет GET-запрос к серверу. Достает из базы данных список регионов.
        /// </summary>
        /// <returns>Содержание таблицы регионов. Код ответа.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Region>>> GetRegions()
        {
            return await _context.Regions.ToListAsync();
        }

        // GET api/<RegionController>/5
        /// <summary>
        /// Осуществляет GET-запрос к серверу. Достает из базы данных регион с нужным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор региона.</param>
        /// <returns>Найденный регион. Код ответа.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Region>> GetRegion(int id)
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

        /// <summary>
        /// Осуществляет POST-запрос к серверу. Добавляет заданный регион в базу данных.
        /// </summary>
        /// <param name="regionInp">Регион для добавления.</param>
        /// <returns>Код ответа.</returns>
        // POST api/<RegionController>
        [HttpPost]
        public async Task<ActionResult<Region>> Post([FromBody] Region regionInp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Region region = new()
            {
                Title = regionInp.Title
            };

            _context.Regions.Add(region);
            await _context.SaveChangesAsync();
            regionInp.RegionId = region.RegionId;
            return CreatedAtAction("GetRegion", new { id = regionInp.RegionId }, regionInp);

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //_context.Regions.Add(region);
            //await _context.SaveChangesAsync();
            //return CreatedAtAction("Get", new { id = region.RegionId }, region);
        }

        /// <summary>
        /// Осуществляет PUT-запрос к серверу. Вносит изменения в заданный регион в базе данных.
        /// </summary>
        /// <param name="id">Идентификатор региона для изменения.</param>
        /// <param name="regionDTO">Экземпляр класса Region. Содержит данные для замены.</param>
        /// <returns>Код ответа. IEnumerable-список актуальных записей таблицы Region.</returns>
        // PUT api/<RegionController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<IEnumerable<Region>>> PutRegion(int id, Region regionDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Region region = await _context.Regions.FindAsync(id);

            region.Title = regionDTO.Title;

            _context.Regions.Update(region);
            await _context.SaveChangesAsync();
            //settDTO.SettlementDTOId = settlement.SettlementId;
            //return CreatedAtAction("GetSettlement", new { id = settDTO.SettlementDTOId }, settDTO);
            return await GetRegions();
        }

        /// <summary>
        /// Осуществляет DELETE-запрос к серверу. Удаляет заданный регион из базы данных.
        /// </summary>
        /// <param name="id">Идентфиикатор заданного региона.</param>
        /// <returns>Код ответа. IEnumerable-список актуальных записей таблицы Region.</returns>
        // DELETE api/<RegionController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<Region>>> Delete([FromRoute] int id)
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
            await foreach (var sett in _context.Settlements)
            {
                if (sett.RegionFk == id)
                {
                    _context.Settlements.Remove(sett);
                }
            }

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return await GetRegions();
        }
    }
}
