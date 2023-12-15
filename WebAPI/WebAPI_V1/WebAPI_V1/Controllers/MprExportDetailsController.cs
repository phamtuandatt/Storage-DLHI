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
    public class MprExportDetailsController : ControllerBase
    {
        private readonly StorageDlhiContext _context;

        public MprExportDetailsController(StorageDlhiContext context)
        {
            _context = context;
        }

        // GET: api/MprExportDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MprExportDetail>>> GetMprExportDetails()
        {
            if (_context.MprExportDetails == null)
            {
                return NotFound();
            }
            return await _context.MprExportDetails.ToListAsync();
        }

        [HttpGet("GetMprExportDetailsByProc")]
        public async Task<ActionResult<MprExportDetail>> GetMprExportDetailsByProc()
        {
            try
            {
                var courseList = await _context.MPRExportDetailResponseDtos.FromSqlRaw($"EXEC GET_MPR_EXPORT_DETAILS").ToListAsync();
                return Ok(courseList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetMprExportExcel")]
        public async Task<ActionResult<MprExportDetail>> GetMprExportExcel()
        {
            try
            {
                var courseList = await _context.MRPExportExcelResponseDtos.FromSqlRaw($"GET_MPR_EXPORT_EXCEL").ToListAsync();
                return Ok(courseList);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT: api/MprExportDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMprExportDetail(Guid id, MprExportDetail mprExportDetail)
        {
            if (id != mprExportDetail.MprExportId)
            {
                return BadRequest();
            }

            _context.Entry(mprExportDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MprExportDetailExists(id))
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

        // POST: api/MprExportDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MprExportDetail>> PostMprExportDetail(MprExportDetail mprExportDetail)
        {
            if (_context.MprExportDetails == null)
            {
                return Problem("Entity set 'StorageDlhiContext.MprExportDetails'  is null.");
            }
            _context.MprExportDetails.Add(mprExportDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MprExportDetailExists(mprExportDetail.MprExportId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMprExportDetail", new { id = mprExportDetail.MprExportId }, mprExportDetail);
        }

        [HttpPost("InsertDetailExportIntoCurrentMPRExport")]
        public async Task<ActionResult<MprExportDetail>> InsertDetailExportIntoCurrentMPRExport(MprExportDetail mprExportDetail)
        {
            if (_context.MprExportDetails == null)
            {
                return Problem("Entity set 'StorageDlhiContext.MprExportDetails'  is null.");
            }

            try
            {
                var mprExport = _context.MprExports.FirstOrDefault(st => st.Status == 2);

                mprExportDetail.MprExportId = mprExport!.Id;
                _context.MprExportDetails.Add(mprExportDetail);
                await _context.SaveChangesAsync();

                mprExport.ItemCount = _context.MprExports.Count(it => it.Id == mprExport.Id);
                _context.Attach(mprExport);
                _context.Entry(mprExport).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                if (MprExportDetailExists(mprExportDetail.MprExportId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: api/MprExportDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMprExportDetail(Guid id)
        {
            if (_context.MprExportDetails == null)
            {
                return NotFound();
            }
            var mprExportDetail = await _context.MprExportDetails.FindAsync(id);
            if (mprExportDetail == null)
            {
                return NotFound();
            }

            _context.MprExportDetails.Remove(mprExportDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MprExportDetailExists(Guid id)
        {
            return (_context.MprExportDetails?.Any(e => e.MprExportId == id)).GetValueOrDefault();
        }
    }
}
