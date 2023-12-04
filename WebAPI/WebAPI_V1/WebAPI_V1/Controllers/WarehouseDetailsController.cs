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
    public class WarehouseDetailsController : ControllerBase
    {
        private readonly StorageDlhiContext _context;

        public WarehouseDetailsController(StorageDlhiContext context)
        {
            _context = context;
        }

        // GET: api/WarehouseDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WarehouseDetail>>> GetWarehouseDetails()
        {
          if (_context.WarehouseDetails == null)
          {
              return NotFound();
          }
            return await _context.WarehouseDetails.ToListAsync();
        }

        // GET: api/WarehouseDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WarehouseDetail>> GetWarehouseDetail(Guid id)
        {
          if (_context.WarehouseDetails == null)
          {
              return NotFound();
          }
            var warehouseDetail = await _context.WarehouseDetails.FindAsync(id);

            if (warehouseDetail == null)
            {
                return NotFound();
            }

            return warehouseDetail;
        }

        // PUT: api/WarehouseDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWarehouseDetail(Guid id, WarehouseDetail warehouseDetail)
        {
            if (id != warehouseDetail.WarehouseId)
            {
                return BadRequest();
            }

            _context.Entry(warehouseDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WarehouseDetailExists(id))
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

        // POST: api/WarehouseDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WarehouseDetail>> PostWarehouseDetail(WarehouseDetail warehouseDetail)
        {
          if (_context.WarehouseDetails == null)
          {
              return Problem("Entity set 'StorageDlhiContext.WarehouseDetails'  is null.");
          }
            _context.WarehouseDetails.Add(warehouseDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WarehouseDetailExists(warehouseDetail.WarehouseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWarehouseDetail", new { id = warehouseDetail.WarehouseId }, warehouseDetail);
        }

        // DELETE: api/WarehouseDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouseDetail(Guid id)
        {
            if (_context.WarehouseDetails == null)
            {
                return NotFound();
            }
            var warehouseDetail = await _context.WarehouseDetails.FindAsync(id);
            if (warehouseDetail == null)
            {
                return NotFound();
            }

            _context.WarehouseDetails.Remove(warehouseDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WarehouseDetailExists(Guid id)
        {
            return (_context.WarehouseDetails?.Any(e => e.WarehouseId == id)).GetValueOrDefault();
        }
    }
}
