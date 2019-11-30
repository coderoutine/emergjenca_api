using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Emergency.DAL.Data;
using Emergency.DAL.Data.Entities;
using EmergencyCordinationApi.DataFilters;
using EmergencyCordinationApi.Models.ViewModels;

namespace EmergencyCordinationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliesController : ControllerBase
    {
        private readonly EContext _context;

        public SuppliesController(EContext context)
        {
            _context = context;
        }

        // GET: api/Supplies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplyViewModel>>> GetSupplies([FromQuery] SupliesFilter filter=null)
        {
            if (filter == null) filter = new SupliesFilter();
            return await _context.Supplies.Where(filter.Filter).Select(SupplyViewModel.Map).ToListAsync();
        }

        // GET: api/Supplies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplyViewModel>> GetSupplies(Guid id)
        {
            var supplies = await _context.Supplies.Where(z=>z.Id==id).Select(SupplyViewModel.Map).SingleOrDefaultAsync();

            if (supplies == null)
            {
                return NotFound();
            }

            return supplies;
        }

        // PUT: api/Supplies/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupplies(Guid id, Supplies supplies)
        {
            if (id != supplies.Id)
            {
                return BadRequest();
            }

            _context.Entry(supplies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SuppliesExists(id))
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

        // POST: api/Supplies
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SupplyViewModel>> PostSupplies(SuplieCreateViewModel data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var supplies = new Supplies
            {

                Address = data.Address,
                City = data.City,
                Country = data.Country,
                EventId = data.EventId,
                Lat = data.Lat,
                Lng = data.Lng,
                Name = data.Name,
                Description = data.Description,
                Status = data.Status,
                ContactPerson = data.ContactPersons?.Select(cp => new ContactPerson
                {
                    Email = cp.Email,
                    FirstName = cp.FirstName,
                    Phone = cp.Phone,
                    LastName = cp.LastName,
                }).ToList()
            };
            _context.Supplies.Add(supplies);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SuppliesExists(supplies.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return await GetSupplies(supplies.Id);
        }

        // DELETE: api/Supplies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Supplies>> DeleteSupplies(Guid id)
        {
            var supplies = await _context.Supplies.FindAsync(id);
            if (supplies == null)
            {
                return NotFound();
            }

            _context.Supplies.Remove(supplies);
            await _context.SaveChangesAsync();

            return supplies;
        }

        private bool SuppliesExists(Guid id)
        {
            return _context.Supplies.Any(e => e.Id == id);
        }
    }
}
