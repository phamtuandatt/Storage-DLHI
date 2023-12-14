using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_V1.Models;
using WebAPI_V1.Models.ResponseDto.POResponseDto;

namespace WebAPI_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoesController : ControllerBase
    {
        private readonly StorageDlhiContext _context;

        public PoesController(StorageDlhiContext context)
        {
            _context = context;
        }

        // GET: api/Poes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<POResponseDto>>> GetPos()
        {
            try
            {
                var courseList = await _context.POResponseDtos.FromSqlRaw($"EXEC GET_POs").ToListAsync();
                return Ok(courseList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Poes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Po>> GetPo(Guid id)
        {
          if (_context.Pos == null)
          {
              return NotFound();
          }
            var po = await _context.Pos.FindAsync(id);

            if (po == null)
            {
                return NotFound();
            }

            return po;
        }

        // PUT: api/Poes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPo(Guid id, Po po)
        {
            if (id != po.Id)
            {
                return BadRequest();
            }

            _context.Entry(po).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoExists(id))
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

        // POST: api/Poes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Po>> PostPo(Po po)
        {
          if (_context.Pos == null)
          {
              return Problem("Entity set 'StorageDlhiContext.Pos'  is null.");
          }
            _context.Pos.Add(po);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PoExists(po.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPo", new { id = po.Id }, po);
        }

        // DELETE: api/Poes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePo(Guid id)
        {
            if (_context.Pos == null)
            {
                return NotFound();
            }
            var po = await _context.Pos.FindAsync(id);
            if (po == null)
            {
                return NotFound();
            }

            _context.Pos.Remove(po);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PoExists(Guid id)
        {
            return (_context.Pos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
