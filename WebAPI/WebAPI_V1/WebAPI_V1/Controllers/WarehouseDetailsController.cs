using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPI_V1.Models;
using WebAPI_V1.Models.RequestDto;

namespace WebAPI_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseDetailsController : ControllerBase
    {
        private readonly StorageDlhiContext _context;
        private readonly IMapper mapper;

        public WarehouseDetailsController(StorageDlhiContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
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
            _context.WarehouseDetails.UpdateRange(new List<WarehouseDetail>());
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
        public async Task<ActionResult<List<WarehouseDetail>>> PostWarehouseDetail(List<WarehouseRequestDto> warehouseDetail)
        {
            if (_context.WarehouseDetails == null)
            {
                return Problem("Entity set 'StorageDlhiContext.WarehouseDetails'  is null.");
            }

            try
            {
                var viewModel = mapper.Map<List<WarehouseDetail>>(warehouseDetail);
                using (var context = _context)
                {
                    foreach (var item in viewModel)
                    {
                        var oldModel = context.WarehouseDetails.AsNoTracking().FirstOrDefault(a => a.WarehouseId == item.WarehouseId
                                                                        && a.ItemId == item.ItemId
                                                                        && a.Year == item.Year
                                                                        && a.Month == item.Month);
                        if (oldModel != null)
                        {
                            oldModel = mapper.Map<WarehouseDetail>(item);

                            context.Attach(oldModel);
                            context.Entry(oldModel).State = EntityState.Modified;
                            //context.Entry(oldModel).State = EntityState.Detached;
                        }
                        else
                        {
                            context.WarehouseDetails.Add(new WarehouseDetail()
                            {
                                WarehouseId = item.WarehouseId,
                                ItemId = item.ItemId,
                                Month = item.Month,
                                Year = item.Year,
                                Quantity = item.Quantity,
                            });
                        }
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateException)
            {
                if (WarehouseDetailExists(warehouseDetail[0].WarehouseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWarehouseDetail", new { id = warehouseDetail[0].WarehouseId }, warehouseDetail);
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
