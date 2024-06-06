using System.Text.Json.Serialization;

namespace DistantWorkCalendarService.Classes
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EEventType : int
    {
        distant = 0,
        vacation = 1
    }
}
