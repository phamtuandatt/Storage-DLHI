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
    public class SuppliersController : ControllerBase
    {
        private readonly StorageDlhiContext _context;

        public SuppliersController(StorageDlhiContext context)
        {
            _context = context;
        }

        // GET: api/Suppliers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supplier>>> GetSuppliers()
        {
            if (_context.Suppliers == null)
            {
                return NotFound();
            }
            return await _context.Suppliers.ToListAsync();
        }

        // GET: api/Suppliers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplier(Guid id)
        {
            if (_context.Suppliers == null)
            {
                return NotFound();
            }
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return supplier;
        }

        [HttpGet("get-code-supplier/{code}")]
        public async Task<ActionResult<string>> GetCodeSupplier(string code)
        {
            try
            {
                var stringCode = await _context.Database.SqlQueryRaw<string>($"EXEC GET_CURRENT_CODE_SUPPLIER '{code}'").ToListAsync();
                var courseList = string.IsNullOrEmpty(stringCode[0]) ? "0000000" : stringCode[0].ToString();

                if (string.IsNullOrEmpty(courseList))
                {
                    return "0000001";
                }
                try
                {
                    var number = int.Parse(courseList);
                    if (number >= 0 && number < 9)
                    {
                        return "000000" + (number + 1);
                    }
                    else if (number >= 9 && number < 99)
                    {
                        return "00000" + (number + 1);
                    }
                    else if (number >= 99 && number < 999)
                    {
                        return "0000" + (number + 1);
                    }
                    else if (number >= 999 && number < 9999)
                    {
                        return "000" + (number + 1);
                    }
                    else if (number >= 9999 && number < 99999)
                    {
                        return "00" + (number + 1);
                    }
                    else if (number >= 99999 && number < 999999)
                    {
                        return "0" + (number + 1);
                    }
                    else
                    {
                        return courseList + 1;
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: api/Suppliers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplier(Guid id, Supplier supplier)
        {
            if (id != supplier.Id)
            {
                return BadRequest();
            }

            _context.Entry(supplier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
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

        // POST: api/Suppliers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier(Supplier supplier)
        {
            if (_context.Suppliers == null)
            {
                return Problem("Entity set 'StorageDlhiContext.Suppliers'  is null.");
            }
            _context.Suppliers.Add(supplier);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SupplierExists(supplier.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSupplier", new { id = supplier.Id }, supplier);
        }

        // DELETE: api/Suppliers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            if (_context.Suppliers == null)
            {
                return NotFound();
            }
            var supplier = await _context.Suppliers.FindAsync(id);
            if (supplier == null)
            {
                return NotFound();
            }

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupplierExists(Guid id)
        {
            return (_context.Suppliers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
