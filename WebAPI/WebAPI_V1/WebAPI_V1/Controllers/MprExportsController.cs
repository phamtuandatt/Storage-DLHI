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
    public class MprExportsController : ControllerBase
    {
        private readonly StorageDlhiContext _context;

        public MprExportsController(StorageDlhiContext context)
        {
            _context = context;
        }

        // GET: api/MprExports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MprExport>>> GetMprExports()
        {
            if (_context.MprExports == null)
            {
                return NotFound();
            }
            return await _context.MprExports.ToListAsync();
        }

        // GET: api/MprExports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MprExport>> GetMprExport(Guid id)
        {
            if (_context.MprExports == null)
            {
                return NotFound();
            }
            var mprExport = await _context.MprExports.FindAsync(id);

            if (mprExport == null)
            {
                return NotFound();
            }

            return mprExport;
        }


        [HttpPut("UpdateMPR_Export_Status")]
        public async Task<IActionResult> UpdateMPR_Export_Status()
        {
            var mprExport = _context.MprExports.FirstOrDefault(st => st.Status == 2);

            if (mprExport == null)
            {
                return BadRequest();
            }
            mprExport.Status = 1;
            _context.Entry(mprExport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MprExportExists(mprExport.Id))
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

        [HttpPost]
        public async Task<ActionResult<MprExport>> PostMprExport(MprExport mprExport)
        {
            if (_context.MprExports == null)
            {
                return Problem("Entity set 'StorageDlhiContext.MprExports'  is null.");
            }
            _context.MprExports.Add(mprExport);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MprExportExists(mprExport.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMprExport", new { id = mprExport.Id }, mprExport);
        }

        // DELETE: api/MprExports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMprExport(Guid id)
        {
            if (_context.MprExports == null)
            {
                return NotFound();
            }
            var mprExport = await _context.MprExports.FindAsync(id);
            if (mprExport == null)
            {
                return NotFound();
            }

            _context.MprExports.Remove(mprExport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MprExportExists(Guid id)
        {
            return (_context.MprExports?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
