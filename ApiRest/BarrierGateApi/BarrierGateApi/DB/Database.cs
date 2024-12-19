using BarrierGateApi.Models;
using BarrierGateApi.Singleton;
using Microsoft.EntityFrameworkCore;
using BarrierGateApi.DB.Context;

namespace BarrierGateApi.DB
{
    public class Database : Singleton<Database>
    {
        private Database() { }

        public Context<CalendarEvent> CalendarEventDB { get; set; } 
            = new Context<CalendarEvent>(sqliteFilePath: ConfigSingleton.Instance.ConfigParam.JsonPath);
        public Context<BarrierGate> BarrierGateDB {  get; set; } 
            = new Context<BarrierGate>(sqliteFilePath: ConfigSingleton.Instance.ConfigParam.JsonPath);
    }
}
