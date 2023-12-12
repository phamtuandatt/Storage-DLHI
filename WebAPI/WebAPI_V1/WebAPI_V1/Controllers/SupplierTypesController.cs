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
    public class SupplierTypesController : ControllerBase
    {
        private readonly StorageDlhiContext _context;

        public SupplierTypesController(StorageDlhiContext context)
        {
            _context = context;
        }

        // GET: api/SupplierTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierType>>> GetSupplierTypes()
        {
            if (_context.SupplierTypes == null)
            {
                return NotFound();
            }
            return await _context.SupplierTypes.ToListAsync();
        }

        // GET: api/SupplierTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierType>> GetSupplierType(Guid id)
        {
            if (_context.SupplierTypes == null)
            {
                return NotFound();
            }
            var supplierType = await _context.SupplierTypes.FindAsync(id);

            if (supplierType == null)
            {
                return NotFound();
            }

            return supplierType;
        }

        // PUT: api/SupplierTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplierType(Guid id, SupplierType supplierType)
        {
            if (id != supplierType.Id)
            {
                return BadRequest();
            }

            _context.Entry(supplierType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierTypeExists(id))
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

        // POST: api/SupplierTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SupplierType>> PostSupplierType(SupplierType supplierType)
        {
            if (_context.SupplierTypes == null)
            {
                return Problem("Entity set 'StorageDlhiContext.SupplierTypes'  is null.");
            }
            _context.SupplierTypes.Add(supplierType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SupplierTypeExists(supplierType.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSupplierType", new { id = supplierType.Id }, supplierType);
        }

        // DELETE: api/SupplierTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplierType(Guid id)
        {
            if (_context.SupplierTypes == null)
            {
                return NotFound();
            }
            var supplierType = await _context.SupplierTypes.FindAsync(id);
            if (supplierType == null)
            {
                return NotFound();
            }

            _context.SupplierTypes.Remove(supplierType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierTypeExists(Guid id)
        {
            return (_context.SupplierTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
