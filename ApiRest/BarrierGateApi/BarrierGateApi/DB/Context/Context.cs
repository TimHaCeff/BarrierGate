using BarrierGateApi.Singleton;
using Microsoft.EntityFrameworkCore;
using BarrierGateApi.Models;
using BarrierGateApi.Interfaces;

namespace BarrierGateApi.DB.Context
{
    public class Context : DbContext, IDbBasicActions<BarrierGate>, IDbBasicActions<CalendarEvent>
    {
        public DbSet<BarrierGate> barrierGate { get; set; }
        public DbSet<CalendarEvent> calendarEvent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseSqlite($"DataSource={ConfigSingleton.Instance.ConfigParam.SQLitePath}");
                
            }
        }

        public DbSet<T> ReturnDBSet<T>() where T : GestionnableElement
        {
            if (typeof(T) == typeof(BarrierGate))
            {
                return barrierGate as DbSet<T>;
            }
            else if (typeof(T) == typeof(CalendarEvent))
            {
                return calendarEvent as DbSet<T>;
            }
            return barrierGate as DbSet<T>;
        }

        public async Task Save() 
        {
            await SaveChangesAsync();
        }
    }
}
