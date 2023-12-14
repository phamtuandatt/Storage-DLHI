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
using WebAPI_V1.Models.ResponseDto.POResponseDto;

namespace WebAPI_V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoDetailsController : ControllerBase
    {
        private readonly StorageDlhiContext _context;
        private readonly IMapper mapper;

        public PoDetailsController(StorageDlhiContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/PoDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoDetail>>> GetPoDetails()
        {
            if (_context.PoDetails == null)
            {
                return NotFound();
            }
            return await _context.PoDetails.ToListAsync();
        }

        [HttpGet("get-po-detail-by-proc")]
        public async Task<ActionResult<IEnumerable<PODetailResponseDto>>> GetPoDetailsByProc()
        {
            try
            {
                var courseList = await _context.PODetailResponseDtos.FromSqlRaw($"EXEC GET_PO_DETAIL").ToListAsync();
                return Ok(courseList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("get-po-export")]
        public async Task<ActionResult<IEnumerable<GetPoExportResponseDto>>> GetPoExport()
        {
            try
            {
                var courseList = await _context.GetPoExportResponseDtos.FromSqlRaw($"EXEC GET_PO_EXPORT").ToListAsync();
                return Ok(courseList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST: api/PoDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PORequestDto>> PostPoDetail(List<PORequestDto> poDetails)
        {
            if (_context.PoDetails == null)
            {
                return Problem("Entity set 'StorageDlhiContext.PoDetails'  is null.");
            }
            try
            {
                var viewModel = mapper.Map<List<PoDetail>>(poDetails);
                using (var context = _context)
                {
                    foreach (var item in viewModel)
                    {
                        var oldModel = context.PoDetails.AsNoTracking().FirstOrDefault(a => a.PoId == item.PoId 
                                                                                        && a.ItemId == item.ItemId
                                                                                        && a.MprNo == item.MprNo
                                                                                        && a.PoNo == item.PoNo);
                        if (oldModel != null)
                        {
                            oldModel = mapper.Map<PoDetail>(item);

                            context.Attach(oldModel);
                            context.Entry(oldModel).State = EntityState.Modified;
                            //context.Entry(oldModel).State = EntityState.Detached;
                        }
                        else
                        {
                            context.PoDetails.Add(new PoDetail()
                            {
                                PoId = item.PoId,
                                ItemId= item.ItemId,
                                MprNo = item.MprNo,
                                PoNo= item.PoNo,
                                Price = item.Price,
                                Quantity = item.Quantity,
                            });
                        }
                    }
                    await context.SaveChangesAsync();
                    return Ok();
                }
            }
            catch (DbUpdateException)
            {
                return NotFound();
            }
        }

        // DELETE: api/PoDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoDetail(Guid id)
        {
            if (_context.PoDetails == null)
            {
                return NotFound();
            }
            var poDetail = await _context.PoDetails.FindAsync(id);
            if (poDetail == null)
            {
                return NotFound();
            }

            _context.PoDetails.Remove(poDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PoDetailExists(Guid id)
        {
            return (_context.PoDetails?.Any(e => e.PoId == id)).GetValueOrDefault();
        }
    }
}
