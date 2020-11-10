using Managing_Teacher_Work.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Managing_Teacher_Work.Services
{
    public interface IScienseService
    {
        Task<bool> UpdateScienseByIdAsysn(int id);
        Task<List<Science>> GetScienseListAsync();
        Task<bool> DeleteScienseByIdAsync(int id);
        Task<Science> GetScienseByIdAsync(int id);
    }
    public class ScienseService : IScienseService
    {
        protected readonly AppDbContext _dbContext;
        public ScienseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteScienseByIdAsync(int id)
        {
            var sciense = await GetScienseByIdAsync(id);
            if (sciense != null)
            {
                _dbContext.Science.Remove(sciense);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Science> GetScienseByIdAsync(int id)
        {
            return await _dbContext.Science.FindAsync(id);
        }

        public async Task<List<Science>> GetScienseListAsync()
        {
            return await _dbContext.Science.Where(x => x.Name != null).OrderBy(y => y.ID).ToListAsync();
        }

        public Task<bool> UpdateScienseByIdAsysn(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}