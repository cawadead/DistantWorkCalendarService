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

        public async Task<ICollection<EventStatus>> GetEventsByPageAsync(int eventId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var events = await _context.EventStatuses
                .Where(e => e.EventId == eventId)
                .Skip(pageNumber * pageSize).Take(pageSize).ToArrayAsync(cancellationToken);

            return events;
        }

        public async Task<Event?> GetEventAsync(int id, CancellationToken cancellationToken)
        {
            var events = await _context.Events.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return events;
        }

        public async Task<ICollection<Event>> GetEventsAsync(CancellationToken cancellationToken)
        {
            var events = await _context.Events.ToArrayAsync(cancellationToken);

            return events;
        }

        public async Task<ICollection<EventStatus>> GetEventsAsync(DateTime start, DateTime end, CancellationToken cancellationToken)
        {
            start = new DateTime(start.Ticks, DateTimeKind.Utc);
            end = new DateTime(end.Ticks, DateTimeKind.Utc);

            var events = await _context.EventStatuses
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
