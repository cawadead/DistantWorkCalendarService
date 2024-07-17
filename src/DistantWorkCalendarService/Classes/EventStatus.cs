using System.Text.Json.Serialization;

namespace DistantWorkCalendarService.Classes
{
    public class EventStatus
    {
        public int Id { get; set; }
        public string Title { get; set; }  
        public EEventType EventType { get; set; }
        [JsonPropertyName("start")]
        public DateTime StartDate { get; set; }
        [JsonPropertyName("end")]
        public DateTime EndDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
