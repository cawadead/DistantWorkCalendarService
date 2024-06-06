using Microsoft.EntityFrameworkCore;

namespace DistantWorkCalendarService.Classes
{
    public class EventService
    {
        private readonly Context _context;
        private readonly ILogger _logger;

        public EventService(Context context, ILogger<EventService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Event? GetEvent(int id)
        {
            var events = _context.Events.FirstOrDefault();

            return events;
        }

        public ICollection<Event> GetEvents()
        {
            var events = _context.Events.ToArray();

            return events;
        }

        public ICollection<Event> GetEvents(DateTime start, DateTime end)
        {
            start = new DateTime(start.Ticks, DateTimeKind.Utc);
            end = new DateTime(end.Ticks, DateTimeKind.Utc);

            var events = _context.Events
                .Where(x => (x.StartDate >= start || x.EndDate >= start) && (x.StartDate <= end || x.EndDate <= end))
                .ToArray();

            return events;
        }

        public Event AddEvent(Event event_)
        {
            _context.Events.Add(event_);
            _context.SaveChanges();

            return event_;
        }

        public Event EditEvent(int id, Event event_)
        {
            event_.Id = id;
            _context.Events.Update(event_);
            _context.SaveChanges();

            return event_;
        }

        public int? DeleteEvent(int id)
        {
            var event_ = GetEvent(id);
            if (event_ is null)
            {
                return null;
            }

            _context.Events.Remove(event_);
            _context.SaveChanges();

            return id;
        }

        public async Task<Event?> GetEventAsync(int id, CancellationToken cancellationToken)
        {
            var events = await _context.Events.FirstOrDefaultAsync(cancellationToken);

            return events;
        }

        public async Task<ICollection<Event>> GetEventsAsync(CancellationToken cancellationToken)
        {
            var events = await _context.Events.ToArrayAsync(cancellationToken);

            return events;
        }

        public async Task<ICollection<Event>> GetEventsAsync(DateTime start, DateTime end, CancellationToken cancellationToken)
        {
            start = new DateTime(start.Ticks, DateTimeKind.Utc);
            end = new DateTime(end.Ticks, DateTimeKind.Utc);

            var events = await _context.Events
                .Where(x => (x.StartDate >= start || x.EndDate >= start) && (x.StartDate <= end || x.EndDate <= end))
                .ToArrayAsync(cancellationToken);

            return events;
        }

        public async Task<Event> AddEventAsync(Event event_, CancellationToken cancellationToken)
        {
            await _context.Events.AddAsync(event_, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return event_;
        }

        public async Task<Event> EditEventAsync(int id, Event event_, CancellationToken cancellationToken)
        {
            event_.Id = id;
            _context.Events.Update(event_);
            await _context.SaveChangesAsync(cancellationToken);

            return event_;
        }

        public async Task<int?> DeleteEventAsync(int id, CancellationToken cancellationToken)
        {
            var event_ = await GetEventAsync(id, cancellationToken);
            if (event_ is null)
            {
                return null;
            }

            _context.Events.Remove(event_);
            await _context.SaveChangesAsync(cancellationToken);

            return id;
        }
    }
}
