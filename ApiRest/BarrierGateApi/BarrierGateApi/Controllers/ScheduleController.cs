using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using BarrierGateApi;
using BarrierGateApi.Singleton;
using BarrierGateApi.Models;

namespace BarrierGateApi.Controllers
{
    public class ScheduleController : Controller
    {
        [HttpGet(nameof(this.CreateSchedule))]
        public bool CreateSchedule(CalendarEvent calendarEvent) 
        {
            BarrierGateSingleton.instance.CreateSchedule(calendarEvent);
            return true;
        }
    }
}
