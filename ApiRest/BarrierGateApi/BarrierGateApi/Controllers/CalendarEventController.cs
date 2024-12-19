using BarrierGateApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BarrierGateApi.DB;
using BarrierGateApi.DB.Context;

namespace BarrierGateApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarEventController : ControllerGestionnableElements<CalendarEvent>
    {
        protected override Context<CalendarEvent> database { get; set; } = Database.Instance.CalendarEventDB;
    }
}

