using DistantWorkCalendarService.Classes;
using Microsoft.EntityFrameworkCore;

namespace DistantWorkCalendarService.Data
{
    public class DbSeed
    {
        private readonly Context _context;

        public DbSeed(Context context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            if (await _context.Events.AnyAsync()) return;

            await InitializeEventsAsync();
            await InitializeEventStatusesAsync();
        }

        private List<EventStatus> eventStatuses = new List<EventStatus>();
        private async Task InitializeEventStatusesAsync()
        {
            var rnd = new Random();

            foreach (var _event in _context.Events.ToArray())
            {
                for (int i = 1; i < rnd.Next(1, 5); i++)
                {
                    var status = new EventStatus
                    {
                        Title = $"User {i}",
                        EventType = (EEventType)rnd.Next(0, 1),
                        StartDate = new DateTime(2024, 7, 1).AddDays(rnd.Next(15)),
                        EndDate = DateTime.Now,
                        CreatedDate = _event.ModifiedDate,
                        EventId = _event.Id
                    };
                    eventStatuses.Add(status);
                }
            }

            await _context.EventStatuses.AddRangeAsync(eventStatuses);
            await _context.SaveChangesAsync();
        }

        private const int __eventsCount = 10;
        private Event[] _events;
        private async Task InitializeEventsAsync()
        {
            _events = Enumerable.Range(1, __eventsCount)
                .Select(i => new Event
                {
                    CreatedDate = DateTime.Now,
                    EventStatuses = new List<EventStatus>(),
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                })
                .ToArray();

            await _context.Events.AddRangeAsync(_events);
            await _context.SaveChangesAsync();
        }
    }
}
