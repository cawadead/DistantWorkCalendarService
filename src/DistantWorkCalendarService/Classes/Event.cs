using System.Text.Json.Serialization;

namespace DistantWorkCalendarService.Classes
{
    public class Event
    {
        public int Id { get; set; }
        public ICollection<EventStatus> EventStatuses { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted {  get; set; }
    }
}
