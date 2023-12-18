using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_V1.Models;
using WebAPI_V1.Models.ResponseDto.ImportItemResponseDto;

namespace WebAPI_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportItemsController : ControllerBase
    {
        private readonly StorageDlhiContext _context;
        private readonly IMapper mapper;

        public ImportItemsController(StorageDlhiContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/ImportItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImportItemResponseDto>>> GetImportItems()
        {
            if (_context.ImportItems == null)
            {
                return NotFound();
            }

            var viewModel = await _context.ImportItems.ToListAsync();
            var data = mapper.Map<List<ImportItemResponseDto>>(viewModel);
            return data;
        }

        // GET: api/ImportItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImportItem>> GetImportItem(Guid id)
        {
            if (_context.ImportItems == null)
            {
                return NotFound();
            }
            var importItem = await _context.ImportItems.FindAsync(id);

            if (importItem == null)
            {
                return NotFound();
            }

            return importItem;
        }

        // PUT: api/ImportItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImportItem(Guid id, ImportItem importItem)
        {
            if (id != importItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(importItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImportItemExists(id))
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

        // POST: api/ImportItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImportItem>> PostImportItem(ImportItem importItem)
        {
            if (_context.ImportItems == null)
            {
                return Problem("Entity set 'StorageDlhiContext.ImportItems'  is null.");
            }
            _context.ImportItems.Add(importItem);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ImportItemExists(importItem.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetImportItem", new { id = importItem.Id }, importItem);
        }

        // DELETE: api/ImportItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImportItem(Guid id)
        {
            if (_context.ImportItems == null)
            {
                return NotFound();
            }
            var importItem = await _context.ImportItems.FindAsync(id);
            if (importItem == null)
            {
                return NotFound();
            }

            _context.ImportItems.Remove(importItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImportItemExists(Guid id)
        {
            return (_context.ImportItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
