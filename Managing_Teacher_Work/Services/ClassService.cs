using Managing_Teacher_Work.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Managing_Teacher_Work.Services
{
    public interface IClassService
    {
        Task<List<Class>> GetClassListAsync();
        Task<bool> DeleteClassByIdAsync(int id);
        Task<Class> GetClassByIdAsync(int id);
        Task<List<Class>> GetClassListByTeacherIdAsync(int teacherId);
        Task<List<Class>> GetClassListByScienseIdAsync(int scienseId);
    }

    public class ClassService : IClassService
    {
        private readonly AppDbContext _dbContext;
        public ClassService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteClassByIdAsync(int id)
        {
            var classId = await GetClassByIdAsync(id);

            if(classId != null)
            {
                _dbContext.Class.Remove(classId);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<Class> GetClassByIdAsync(int id)
        {
            return await _dbContext.Class.Where(c => c.ID == id).FirstOrDefaultAsync();
        }

        public async Task<List<Class>> GetClassListAsync()
        {
            return await _dbContext.Class.OrderBy(c => c.ClassName).ToListAsync();
        }

        public async Task<List<Class>> GetClassListByScienseIdAsync(int scienseId)
        {
            return await _dbContext.Class.Where(c => c.ScienceID == scienseId).ToListAsync();
        }

        public async Task<List<Class>> GetClassListByTeacherIdAsync(int teacherId)
        {
            return await _dbContext.Class.Where(c => c.TeacherID == teacherId).ToListAsync();
        }
    }
}