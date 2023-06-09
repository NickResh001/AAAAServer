﻿using ASP_busstation.Models1;
using ASP_busstation.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Cors;
using Microsoft.OpenApi.Writers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP_busstation.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class SettlementController : ControllerBase
    {
        AspbusstationContext _context;

        public SettlementController(AspbusstationContext context)
        {
            _context = context;
        }

        // GET api/<Settlement>/5
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SettlementDTO>>> GetSettlements()
        {
            List<SettlementDTO> settDTOs = new List<SettlementDTO>();
            await foreach (var s in _context.Settlements)
            {
                SettlementDTO settDTO = new SettlementDTO()
                {
                    SettlementDTOId = s.SettlementId,
                    Title = s.Title,
                    RegionFK = s.RegionFk
                };
                settDTOs.Add(settDTO);
            }
            foreach (var s in settDTOs)
            {
                await foreach (var r in _context.Regions)
                {
                    if (r.RegionId == s.RegionFK)
                    {
                        s.RegionTitle = r.Title;
                        break;
                    }
                }
            }
            return settDTOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SettlementDTO>> GetSettlement(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            SettlementDTO settDTO = new SettlementDTO();
            bool isExist = false;
            await foreach (var s in _context.Settlements)
            {
                if (s.SettlementId == id)
                {
                    isExist = true;
                    settDTO = new SettlementDTO()
                    {
                        SettlementDTOId = s.SettlementId,
                        Title = s.Title,
                        RegionFK = s.RegionFk
                    };
                    break;
                }
            }
            if (isExist)
            {
                await foreach (var r in _context.Regions)
                {
                    if (r.RegionId == settDTO.RegionFK)
                    {
                        settDTO.RegionTitle = r.Title;
                        break;
                    }
                }
                return Ok(settDTO);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Settlement>> Post([FromBody] SettlementDTO settDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Settlement settlement = new()
            {
                Title = settDTO.Title
            };
            bool regionExist = false;
            foreach (var region in _context.Regions)
            {
                if (region.RegionId == settDTO.RegionFK)
                {
                    settlement.RegionFkNavigation = region;
                    regionExist = true;
                }
            }
            if (!regionExist)
            {
                return BadRequest(ModelState);
            }

            _context.Settlements.Add(settlement); 
            await _context.SaveChangesAsync();
            settDTO.SettlementDTOId = settlement.SettlementId;
            return CreatedAtAction("GetSettlement", new { id = settDTO.SettlementDTOId }, settDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSettlement(int id, SettlementDTO settDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Settlement settlement = await _context.Settlements.FindAsync(id);
            
            settlement.Title = settDTO.Title;
            bool regionExist = false;
            foreach (var region in _context.Regions)
            {
                if (region.RegionId == settDTO.RegionFK)
                {
                    settlement.RegionFkNavigation = region;
                    regionExist = true;
                }
            }
            if (!regionExist)
            {
                return BadRequest(ModelState);
            }

            _context.Settlements.Update(settlement);
            await _context.SaveChangesAsync();
            //settDTO.SettlementDTOId = settlement.SettlementId;
            //return CreatedAtAction("GetSettlement", new { id = settDTO.SettlementDTOId }, settDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var settlement = await _context.Settlements.FindAsync(id);
            if (settlement == null)
            {
                return NotFound();
            }
            _context.Settlements.Remove(settlement);
            await _context.SaveChangesAsync();
            return Ok("Сущность успешно удалена");
        }
    }
}
