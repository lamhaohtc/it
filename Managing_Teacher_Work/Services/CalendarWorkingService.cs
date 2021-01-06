using Managing_Teacher_Work.Helpers;
using Managing_Teacher_Work.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Managing_Teacher_Work.Services
{
    public interface ICalendarWorkingService
    {
        Task<List<CalendarWorking>> GetCalendarWorkingListAsyc();
        Task<CalendarWorking> GetCalendarWorkingByIdAsync(int id);
        Task<bool> DeleteCalendarWorkingByIdAysnc(int id);
        Task<bool> CreateCalendarWorkingAsync(CalendarWorking model);
    }
    public class CalendarWorkingService : ICalendarWorkingService
    {
        private readonly AppDbContext _dbContext;
        public CalendarWorkingService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> CreateCalendarWorkingAsync(CalendarWorking model)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCalendarWorkingByIdAysnc(int id)
        {
            var calendar = await GetCalendarWorkingByIdAsync(id);
            if (calendar != null)
            {
                _dbContext.CalendarWorking.Remove(calendar);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<CalendarWorking> GetCalendarWorkingByIdAsync(int id)
        {
            return await _dbContext.CalendarWorking.FindAsync(id);
        }

        public async Task<List<CalendarWorking>> GetCalendarWorkingListAsyc()
        {
            var list = await _dbContext.CalendarWorking.OrderByDescending(o => o.DateStart).ToListAsync();
            if(list.Count > 0 && list != null)
            {
                foreach(var item in list)
                {
                    item.WorkState = DateTimeHelper.SetActivityStatus(item.DateStart, item.DateEnd);
                }

                await _dbContext.SaveChangesAsync();
            }

            return list;
        }
    }
}