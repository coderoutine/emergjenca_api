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
using EmergencyCordinationApi.Services;
using EmergencyCordinationApi.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace EmergencyCordinationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheltersController : BaseController
    {
        private readonly EContext _context;
        INotificationService _notificationService;
        public SheltersController(EContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;

        }

        // GET: api/Shelters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShelterViewModel>>> GetShelter([FromQuery]ShelterFilter filter)
        {
            if (filter == null) filter = new ShelterFilter();
            return await _context.Shelter.Where(filter.Filter).Select(ShelterViewModel.Map).ToListAsync();
        }

        // GET: api/Shelters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShelterViewModel>> GetShelter(Guid id)
        {
            var shelter = await _context.Shelter.Where(z => z.Id == id).Select(ShelterViewModel.Map).SingleOrDefaultAsync();

            if (shelter == null)
            {
                return NotFound();
            }
            return shelter;
        }

        // PUT: api/Shelters/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutShelter(Guid id, Shelter shelter)
        {
            if (id != shelter.Id)
            {
                return BadRequest();
            }
            if (shelter.TenantId != CurrentUserId) return Forbid();
            _context.Entry(shelter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShelterExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            _notificationService.NotifyAll(NotificationType.ShelterUpdate);
            return NoContent();
        }

        // POST: api/Shelters
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ShelterViewModel>> PostShelter(ShelterCreateViewModel data)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var shelter = new Shelter
            {
                TenantId= CurrentUserId,
                Address = data.Address,
                Capacity = data.Capacity,
                City = data.City,
                Country = data.Country,
                EventId = data.EventId,
                Lat = data.Lat,
                Lng = data.Lng,
                Name = data.Name,
                Description = data.Description,
                Type = data.Type,
                ContactPerson = data.ContactPersons?.Select(cp => new ContactPerson
                {
                    Email = cp.Email,
                    FirstName = cp.FirstName,
                    Phone = cp.Phone,
                    LastName = cp.LastName,
                }).ToList()
            };


            _context.Shelter.Add(shelter);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ShelterExists(shelter.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            _notificationService.NotifyAll(NotificationType.ShelterUpdate);
            return await GetShelter(shelter.Id);
        }

        // DELETE: api/Shelters/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Shelter>> DeleteShelter(Guid id)
        {
            var shelter = await _context.Shelter.FindAsync(id);
            if (shelter == null) return NotFound();

            if (shelter.TenantId != CurrentUserId) return Forbid();

            _context.Shelter.Remove(shelter);
            await _context.SaveChangesAsync();
            _notificationService.NotifyAll(NotificationType.ShelterUpdate);
            return shelter;
        }

        private bool ShelterExists(Guid id)
        {
            return _context.Shelter.Any(e => e.Id == id);
        }
    }
}
