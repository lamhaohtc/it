using Managing_Teacher_Work.Enums;
using Managing_Teacher_Work.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Managing_Teacher_Work.Services
{
    public interface ITeacherService
    {
        Task<List<Teacher>> GetTeacherListAsync();
        Task<Teacher> GetTeacherByIdAysnc(int id);
        Task<List<Teacher>> GetTeacherByScienseIdAsync(int scienseId);
        Task<bool> DeteleTeacherByIdAsync(int id);
        Task<List<Teacher>> GetTeacherRoleAsync();
        Task<Teacher> AddTacherAsync(Teacher teacher);
        Task UpdateTeacherAsync(Teacher teacher);
        Task<List<Teacher>> GetAllTeacherListAsync();

    }
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _dbContext;
        public TeacherService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Teacher> AddTacherAsync(Teacher teacher)
        {
            _dbContext.Teacher.Add(teacher);
            await _dbContext.SaveChangesAsync();

            return teacher;
        }

        public async Task<bool> DeteleTeacherByIdAsync(int id)
        {
            var teacher = await GetTeacherByIdAysnc(id);
            if(teacher != null)
            {
                _dbContext.Teacher.Remove(teacher);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<Teacher>> GetAllTeacherListAsync()
        {
            return await _dbContext.Teacher.OrderByDescending(t => t.CreatedDate).ToListAsync();
        }

        public async Task<Teacher> GetTeacherByIdAysnc(int id)
        {
            return await _dbContext.Teacher.FindAsync(id);
        }

        public async Task<List<Teacher>> GetTeacherByScienseIdAsync(int scienseId)
        {
            return await _dbContext.Teacher.Where(t => t.SicenceID == scienseId).ToListAsync();
        }

        public async Task<List<Teacher>> GetTeacherListAsync()
        {
            return await _dbContext.Teacher.Where(t => t.RoleId != RoleEnum.CT.ToString() && t.RoleId != RoleEnum.PCT.ToString() && t.RoleId != RoleEnum.UV.ToString() && t.RoleId != RoleEnum.UV_BTV.ToString()).ToListAsync();
        }

        public async Task<List<Teacher>> GetTeacherRoleAsync()
        {
            return await _dbContext.Teacher.Where(t => t.RoleId != RoleEnum.DV.ToString() && t.RoleId != RoleEnum.TT.ToString() && t.RoleId != RoleEnum.TP.ToString()).ToListAsync();
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            _dbContext.Entry(teacher).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}