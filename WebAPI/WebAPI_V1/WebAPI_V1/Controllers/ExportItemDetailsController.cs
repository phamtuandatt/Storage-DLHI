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
using WebAPI_V1.Models.ResponseDto.ExportItemResponseDto;

namespace WebAPI_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportItemDetailsController : ControllerBase
    {
        private readonly StorageDlhiContext _context;
        private readonly IMapper mapper;

        public ExportItemDetailsController(StorageDlhiContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/ExportItemDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExportItemDetail>>> GetExportItemDetails()
        {
            if (_context.ExportItemDetails == null)
            {
                return NotFound();
            }
            return await _context.ExportItemDetails.ToListAsync();
        }

        [HttpGet("get-export-item-details")]
        public async Task<ActionResult<IEnumerable<ExportItemDetailResponseDto>>> GetExportItemDetailsFromProc()
        {
            try
            {
                var courseList = await _context.ExportItemDetailResponseDtos.FromSqlRaw($"EXEC GET_EMPORT_ITEMS").ToListAsync();
                return Ok(courseList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: api/ExportItemDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExportItemDetail>> GetExportItemDetail(Guid id)
        {
            if (_context.ExportItemDetails == null)
            {
                return NotFound();
            }
            var exportItemDetail = await _context.ExportItemDetails.FindAsync(id);

            if (exportItemDetail == null)
            {
                return NotFound();
            }

            return exportItemDetail;
        }

        // PUT: api/ExportItemDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExportItemDetail(Guid id, ExportItemDetail exportItemDetail)
        {
            if (id != exportItemDetail.ExportItemId)
            {
                return BadRequest();
            }

            _context.Entry(exportItemDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExportItemDetailExists(id))
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

        // POST: api/ExportItemDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ExportItemDetail>>> PostExportItemDetail(List<ExportItemDetailRequestDto> exportItemDetails)
        {
            if (_context.ExportItemDetails == null)
            {
                return Problem("Entity set 'StorageDlhiContext.ExportItemDetails'  is null.");
            }
            //_context.ExportItemDetails.Add(exportItemDetail);
            try
            {
                var viewModel = mapper.Map<List<ExportItemDetailRequestDto>>(exportItemDetails);
                using (var context = _context)
                {
                    foreach (var item in viewModel)
                    {
                        var oldModel = context.ExportItemDetails.AsNoTracking().FirstOrDefault(a => a.ExportItemId == item.ExportItemId
                                                                        && a.ItemId == item.ItemId);
                        if (oldModel != null)
                        {
                            oldModel = mapper.Map<ExportItemDetail>(item);

                            context.Attach(oldModel);
                            context.Entry(oldModel).State = EntityState.Modified;
                            //context.Entry(oldModel).State = EntityState.Detached;
                        }
                        else
                        {
                            context.ExportItemDetails.Add(new ExportItemDetail()
                            {
                                ExportItemId = item.ExportItemId,
                                ItemId = item.ItemId,
                                Qty = item.Qty,
                                Note = item.Note,
                            });
                        }
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (DbUpdateException)
            {
                if (ExportItemDetailExists(exportItemDetails[0].ExportItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExportItemDetail", new { id = exportItemDetails[0].ExportItemId }, exportItemDetails);
        }

        // DELETE: api/ExportItemDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExportItemDetail(Guid id)
        {
            if (_context.ExportItemDetails == null)
            {
                return NotFound();
            }
            var exportItemDetail = await _context.ExportItemDetails.FindAsync(id);
            if (exportItemDetail == null)
            {
                return NotFound();
            }

            _context.ExportItemDetails.Remove(exportItemDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExportItemDetailExists(Guid id)
        {
            return (_context.ExportItemDetails?.Any(e => e.ExportItemId == id)).GetValueOrDefault();
        }
    }
}
