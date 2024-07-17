using DistantWorkCalendarService.Classes;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DistantWorkCalendarService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly ILogger<EventsController> _logger;
        private readonly EventService _eventService;

        public EventsController(ILogger<EventsController> logger, EventService service)
        {
            _logger = logger;
            _eventService = service;
        }

        /// <summary>
        /// Получение истории по Id ивента, номеру и размерности страницы
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetByPage")]
        public async Task<IActionResult> GetEventByPage(int eventId, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            return Ok(await _eventService.GetEventsByPageAsync(eventId, pageNumber, pageSize, cancellationToken));
        }

        /// <summary>
        /// Получение ивентов
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetEvents(DateTime start, DateTime end, CancellationToken cancellationToken)
        {
            return Ok(await _eventService.GetEventsAsync(start, end, cancellationToken));
        }

        /// <summary>
        /// Получение ивента по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(int id, CancellationToken cancellationToken)
        {
            var result = await _eventService.GetEventAsync(id, cancellationToken);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Добавление ивента
        /// </summary>
        /// <param name="event_"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult>PostEvent(Event event_, CancellationToken cancellationToken)
        {
            var result = await _eventService.AddEventAsync(event_, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Редактирование ивента
        /// </summary>
        /// <param name="id"></param>
        /// <param name="event_"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event event_, CancellationToken cancellationToken)
        {
            var result = await _eventService.EditEventAsync(id, event_, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Удаление ивента по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id, CancellationToken cancellationToken)
        {
            var result = await _eventService.DeleteEventAsync(id, cancellationToken);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
