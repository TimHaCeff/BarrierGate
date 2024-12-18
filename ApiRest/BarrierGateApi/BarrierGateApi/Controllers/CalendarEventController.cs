using BarrierGateApi.Models;
using BarrierGateApi.Singleton;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BarrierGateApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalendarEventController : ControllerBase
    {

        [HttpGet(nameof(this.GetAllFromOneBarrierGateFromJsonFile))]
        public string GetAllFromOneBarrierGateFromJsonFile(string barrierGateParent)
        {
            try
            {
                BarrierGate barrierGateParentObj = JsonConvert.DeserializeObject<BarrierGate>(barrierGateParent);
                CalendarEvent calendarEvent = new CalendarEvent();
                List<CalendarEvent> calendarEvents = calendarEvent.GetAllFromJsonFile(barrierGateParentObj);
                return JsonConvert.SerializeObject(calendarEvents);
            }catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return "[]";
            }
        }

        [HttpGet(nameof(this.AddInJsonFile))]
        public bool AddInJsonFile(string barrierGateParent, string calendarEventToAdd)
        {
            try
            {
                BarrierGate barrierGateParentObj = JsonConvert.DeserializeObject<BarrierGate>(barrierGateParent);
                CalendarEvent calendarEvent = JsonConvert.DeserializeObject<CalendarEvent>(calendarEventToAdd);
                return calendarEvent.AddInJsonFile([barrierGateParentObj]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(calendarEventToAdd);
                Console.WriteLine("");
                return false;
            }
        }

        [HttpGet(nameof(this.RemoveInJsonFile))]
        public bool RemoveInJsonFile(string barrierGateParent, string calendarEventToRemove)
        {
            try
            {
                BarrierGate barrierGate = JsonConvert.DeserializeObject<BarrierGate>(barrierGateParent);
                CalendarEvent calendarEvent = JsonConvert.DeserializeObject<CalendarEvent>(calendarEventToRemove);
                return calendarEvent.RemoveInJsonFile([barrierGate]);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet(nameof(this.EditInJsonFile))]
        public bool EditInJsonFile(string barrierGateParent, string calendarEventToEdit, string calendarEventEdited)
        {
            try
            {
                BarrierGate barrierGate = JsonConvert.DeserializeObject<BarrierGate>(barrierGateParent);
                CalendarEvent calendarEvent = JsonConvert.DeserializeObject<CalendarEvent>(calendarEventToEdit);
                CalendarEvent editedCalendarEvent = JsonConvert.DeserializeObject<CalendarEvent>(calendarEventEdited);
                return calendarEvent.ModifyInJsonFile(editedCalendarEvent, [barrierGate]);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
