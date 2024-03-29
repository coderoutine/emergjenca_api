﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Emergency.DAL.Data;
using Emergency.DAL.Data.Entities;
using EmergencyCordinationApi.Models.ViewModels;
using EmergencyCordinationApi.DataFilters;
using EmergencyCordinationApi.Services;

namespace EmergencyCordinationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly EContext _context;
        INotificationService _notificationService;
        public EventsController(EContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;

        }
        [HttpGet]
        [Route("relevant")]
        public async Task<ActionResult<IEnumerable<EventListViewModel>>> GetEvent()
        {
            var filter = new EventFilter() { Relevant=true};
            return await _context.Event.Where(filter.Filter).Select(EventListViewModel.Map).ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventListViewModel>>> GetEvent([FromQuery]EventFilter filter=null)
        {
            if (filter == null) filter = new EventFilter();
            return await _context.Event.Where(filter.Filter).Select(EventListViewModel.Map).ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDetailsViewModel>> GetEvent(Guid id)
        {
            var @event = await _context.Event.Where(z=>z.Id==id).Select(EventDetailsViewModel.Map).SingleOrDefaultAsync();

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, Event @event)
        {
            if (id != @event.Id)
            {
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            _notificationService.NotifyAll(NotificationType.EventUpdate);
            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            _context.Event.Add(@event);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EventExists(@event.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            _notificationService.NotifyAll(NotificationType.EventUpdate);
            return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(Guid id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            _notificationService.NotifyAll(NotificationType.EventUpdate);
            return @event;
        }

        private bool EventExists(Guid id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
