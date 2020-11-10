using Managing_Teacher_Work.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Managing_Teacher_Work.Services
{
    public interface IWorkService
    {
        Task<List<Work>> GetWorkListAsync();
        Task<Work> GetWorkByIdAsync(int id);
    }
    public class WorkService : IWorkService
    {
        private readonly AppDbContext _dbContext;
        public WorkService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Work> GetWorkByIdAsync(int id)
        {
            return await _dbContext.Work.FindAsync(id);
        }

        public async Task<List<Work>> GetWorkListAsync()
        {
            return await _dbContext.Work.ToListAsync();
        }
    }
}