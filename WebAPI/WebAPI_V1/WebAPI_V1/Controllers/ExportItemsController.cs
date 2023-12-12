using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_V1.Models;
using WebAPI_V1.Models.RequestDto;

namespace WebAPI_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportItemsController : ControllerBase
    {
        private readonly StorageDlhiContext _context;
        private readonly IMapper mapper;

        public ExportItemsController(StorageDlhiContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/ExportItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExportItem>>> GetExportItems()
        {
            if (_context.ExportItems == null)
            {
                return NotFound();
            }
            return await _context.ExportItems.ToListAsync();
        }

        // GET: api/ExportItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExportItem>> GetExportItem(Guid id)
        {
            if (_context.ExportItems == null)
            {
                return NotFound();
            }
            var exportItem = await _context.ExportItems.FindAsync(id);

            if (exportItem == null)
            {
                return NotFound();
            }

            return exportItem;
        }

        [HttpGet("get-current-bill-no-in-date/{date}")]
        public async Task<ActionResult<string>> GetCurrentBillNoInDate(string date)
        {
            try
            {
                var stringCode = await _context.Database.SqlQueryRaw<string>($"EXEC GET_CURRENT_BILLNO_EXPORT '{date}'").ToListAsync();
                var courseList = string.IsNullOrEmpty(stringCode[0]) ? "000" : stringCode[0].ToString();

                if (string.IsNullOrEmpty(courseList))
                {
                    return "001";
                }
                try
                {
                    var number = int.Parse(courseList);
                    if (number >= 0 && number < 9)
                    {
                        return "00" + (number + 1);
                    }
                    else if (number >= 9 && number < 99)
                    {
                        return "0" + (number + 1);
                    }
                    else
                    {
                        return "" + (number + 1);
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

        // PUT: api/ExportItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExportItem(Guid id, ExportItem exportItem)
        {
            if (id != exportItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(exportItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExportItemExists(id))
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

        // POST: api/ExportItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExportItem>> PostExportItem(ExportItemRequestDto exportItem)
        {
            if (_context.ExportItems == null)
            {
                return Problem("Entity set 'StorageDlhiContext.ExportItems'  is null.");
            }
            var entity = mapper.Map<ExportItem>(exportItem);
            _context.ExportItems.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExportItemExists(exportItem.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExportItem", new { id = exportItem.Id }, exportItem);
        }

        // DELETE: api/ExportItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExportItem(Guid id)
        {
            if (_context.ExportItems == null)
            {
                return NotFound();
            }
            var exportItem = await _context.ExportItems.FindAsync(id);
            if (exportItem == null)
            {
                return NotFound();
            }

            _context.ExportItems.Remove(exportItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExportItemExists(Guid id)
        {
            return (_context.ExportItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
