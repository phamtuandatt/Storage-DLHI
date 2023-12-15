using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_V1.Models;

namespace WebAPI_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MprsController : ControllerBase
    {
        private readonly StorageDlhiContext _context;

        public MprsController(StorageDlhiContext context)
        {
            _context = context;
        }

        // GET: api/Mprs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mpr>>> GetMprs()
        {
            try
            {
                var courseList = await _context.MPRResponseDtos.FromSqlRaw("EXEC GET_MPR_LIST").ToListAsync();
                return Ok(courseList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/Mprs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mpr>> GetMpr(Guid id)
        {
            if (_context.Mprs == null)
            {
                return NotFound();
            }
            var mpr = await _context.Mprs.FindAsync(id);

            if (mpr == null)
            {
                return NotFound();
            }

            return mpr;
        }

        // PUT: api/Mprs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMpr(Guid id, Mpr mpr)
        {
            if (id != mpr.Id)
            {
                return BadRequest();
            }

            _context.Entry(mpr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MprExists(id))
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

        // POST: api/Mprs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mpr>> PostMpr(Mpr mpr)
        {
            if (_context.Mprs == null)
            {
                return Problem("Entity set 'StorageDlhiContext.Mprs'  is null.");
            }
            _context.Mprs.Add(mpr);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MprExists(mpr.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMpr", new { id = mpr.Id }, mpr);
        }

        // DELETE: api/Mprs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMpr(Guid id)
        {
            if (_context.Mprs == null)
            {
                return NotFound();
            }
            var mpr = await _context.Mprs.FindAsync(id);
            if (mpr == null)
            {
                return NotFound();
            }

            _context.Mprs.Remove(mpr);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MprExists(Guid id)
        {
            return (_context.Mprs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
