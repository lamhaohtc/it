using Managing_Teacher_Work.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Managing_Teacher_Work.Services
{
    public interface ITypeCalendarService
    {
        Task<List<TypeCalendar>> GetTypeCalendarListAsync();
        Task<TypeCalendar> GetTypeCalendarByIdAsync(int id);

    }
    public class TypeCalandarService : ITypeCalendarService
    {
        private readonly AppDbContext _dbContext;
        public TypeCalandarService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TypeCalendar> GetTypeCalendarByIdAsync(int id)
        {
            return await _dbContext.TypeCalendar.FindAsync(id);
        }

        public async Task<List<TypeCalendar>> GetTypeCalendarListAsync()
        {
            return await _dbContext.TypeCalendar.OrderByDescending(t => t.CreatedDate).ToListAsync();
        }
    }
}