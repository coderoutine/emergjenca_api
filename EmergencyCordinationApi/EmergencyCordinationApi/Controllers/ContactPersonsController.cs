using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Emergency.DAL.Data;
using Emergency.DAL.Data.Entities;

namespace EmergencyCordinationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactPersonsController : ControllerBase
    {
        private readonly EContext _context;

        public ContactPersonsController(EContext context)
        {
            _context = context;
        }

        // GET: api/ContactPersons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactPerson>>> GetContactPerson()
        {
            return await _context.ContactPerson.ToListAsync();
        }

        // GET: api/ContactPersons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactPerson>> GetContactPerson(Guid id)
        {
            var contactPerson = await _context.ContactPerson.FindAsync(id);

            if (contactPerson == null)
            {
                return NotFound();
            }

            return contactPerson;
        }

        // PUT: api/ContactPersons/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactPerson(Guid id, ContactPerson contactPerson)
        {
            if (id != contactPerson.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactPerson).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactPersonExists(id))
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

        // POST: api/ContactPersons
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ContactPerson>> PostContactPerson(ContactPerson contactPerson)
        {
            _context.ContactPerson.Add(contactPerson);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactPersonExists(contactPerson.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContactPerson", new { id = contactPerson.Id }, contactPerson);
        }

        // DELETE: api/ContactPersons/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ContactPerson>> DeleteContactPerson(Guid id)
        {
            var contactPerson = await _context.ContactPerson.FindAsync(id);
            if (contactPerson == null)
            {
                return NotFound();
            }

            _context.ContactPerson.Remove(contactPerson);
            await _context.SaveChangesAsync();

            return contactPerson;
        }

        private bool ContactPersonExists(Guid id)
        {
            return _context.ContactPerson.Any(e => e.Id == id);
        }
    }
}
