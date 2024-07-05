using System.Text.Json.Serialization;

namespace DistantWorkCalendarService.Classes
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public EEventType Type { get; set; }
        [JsonPropertyName("start")]
        public DateTime StartDate {  get; set; }
        [JsonPropertyName("end")]
        public DateTime EndDate { get; set; }
    }
}
