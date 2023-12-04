using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAPI_V1.Models;
using WebAPI_V1.Models.ResponseDto.ItemResponse;
using WebAPI_V1.Models.ResponseDto.ItemResponse.ItemResponse;

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

        [HttpGet("get-item-v2")]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var courseList = await _context.ItemResponses.FromSqlRaw("EXEC GET_ITEMS_V2").ToListAsync();
                return Ok(courseList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("get-item-export-v2/{id}")]
        public async Task<IActionResult> GetItemtByWarehouseId(Guid id)
        {
            try
            {
                var courseList = await _context.ItemByWarehouseResponses.FromSqlRaw($"EXEC GET_ITEMS_EXPORT_V2 '{id}'").ToListAsync();
                return Ok(courseList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

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

        [HttpGet("get-code/{code}")]
        public async Task<ActionResult<string>> GetCode(string code)
        {
            try
            {
                var stringCode = await _context.Database.SqlQueryRaw<string>($"EXEC GET_CURRENT_CODE_ITEM '{code}'").ToListAsync();
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

        // POST: api/Items
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("AddItemNoImage")]
        public async Task<ActionResult<Item>> PostItemNoImage(Item item)
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

        [HttpPut("UpdateItemNoImage/{id}")]
        public async Task<IActionResult> PutItemNoImage(Guid id, Item item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            _context.Entry(item).Property(x => x.Picture).IsModified = false;

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

        //--------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------

        private bool ItemExists(Guid id)
        {
            return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
