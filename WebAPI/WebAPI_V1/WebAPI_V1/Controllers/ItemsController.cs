using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAPI_V1.Models;
using WebAPI_V1.Models.ResponseDto;

namespace WebAPI_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly StorageDlhiContext _context;

        public ItemsController(StorageDlhiContext context)
        {
            _context = context;
        }

        [HttpGet("CallProcedure")]
        public async Task<IActionResult> CallStoredProcedure()
        {
            try
            {
                // Replace "YourStoredProcedure" with the actual name of your stored procedure
                var result = await _context.Database.SqlQuery<ItemExportResponseDto>($"EXEC GET_ITEMS_EXPORT_V2 '{Guid.NewGuid()}'").ToListAsync();

                if (result.Any())
                {
                    // Process the result as needed
                    return Ok(result);
                }
                else
                {
                    // Handle the case where no results are returned
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return NotFound(ex);
            }
        }

        [HttpGet("get-item-v2")]
        public async Task<IActionResult> GetItemsByProc()
        {
            try
            {
                //var courseList = await _context.ItemResponses.FromSqlInterpolated($"EXEC GET_ITEMS_V2").ToListAsync();
                //var a = await _context.Database.ExecuteSqlRawAsync($"EXEC GET_ITEMS_V2");
                //return Ok(JArray.FromObject(courseList).ToString());

                var courseList = await _context.ItemResponses.FromSqlRaw("EXEC GET_ITEMS_V2").ToListAsync();
                return Ok(courseList);
            }
            catch (Exception)
            {
                return BadRequest();    
            }
        }

        [HttpGet("get-item-export-v2")]
        public async Task<IActionResult> GetItemExportByProc(Guid id)
        {
            try
            {
                //var courseList = await _context.ItemExportResponses.FromSqlInterpolated($"EXEC GET_ITEMS_EXPORT_V2 '{id}'").ToListAsync();
                //var courseList = _context.UnitResponses.FromSqlRaw($"SELECT TOP (1000) [ID] ,[NAME] FROM [STORAGE_DLHI].[dbo].[UNIT]").AsQueryable().ToList();
                //var courseList = _context.ItemExportResponses.FromSqlRaw<ItemExportResponseDto>($"EXEC GET_ITEMS_EXPORT_V2 '{id}'").AsQueryable().ToList();
                var courseList = await _context.Database.ExecuteSqlRawAsync($"EXEC GET_ITEMS_EXPORT_V2 '{id}'");

                if (courseList <= 0)
                {
                    return BadRequest();
                }

                return Ok(JArray.FromObject(courseList).ToString());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        //--------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItems()
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            return await _context.Items.ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(Guid id)
        {
          if (_context.Items == null)
          {
              return NotFound();
          }
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(Guid id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
          if (_context.Items == null)
          {
              return Problem("Entity set 'StorageDlhiContext.Items'  is null.");
          }
            _context.Items.Add(item);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ItemExists(item.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetItem", new { id = item.Id }, item);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            if (_context.Items == null)
            {
                return NotFound();
            }
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(Guid id)
        {
            return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
