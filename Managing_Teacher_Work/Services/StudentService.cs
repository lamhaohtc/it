using Managing_Teacher_Work.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Managing_Teacher_Work.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetStudentListAsync();
        Task<bool> DeteleStudentByIdAsync(int id);
        Task<Student> GetStudentByIdAsync(int id);
        Task<List<Student>> GetStudentListByClassIdAsync(int classId);
    }
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _dbContext;
        public StudentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> DeteleStudentByIdAsync(int id)
        {
            var student = await GetStudentByIdAsync(id);

            if(student != null)
            {
                _dbContext.Student.Remove(student);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _dbContext.Student.FindAsync(id);
        }

        public async Task<List<Student>> GetStudentListAsync()
        {
            return await _dbContext.Student.Where(s => s.ID > 0).ToListAsync();
        }

        public Task<List<Student>> GetStudentListByClassIdAsync(int classId)
        {
            throw new NotImplementedException();
        }
    }
}