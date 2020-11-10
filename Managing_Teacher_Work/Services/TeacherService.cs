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

    }
    public class TeacherService : ITeacherService
    {
        private readonly AppDbContext _dbContext;
        public TeacherService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
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
            return await _dbContext.Teacher.Where(t => t.RoleId != RoleEnum.CT.ToString() && t.RoleId != RoleEnum.PCT.ToString() && t.RoleId != RoleEnum.UV.ToString()).ToListAsync();
        }

        public async Task<List<Teacher>> GetTeacherRoleAsync()
        {
            return await _dbContext.Teacher.Where(t => t.RoleId != RoleEnum.DV.ToString() && t.RoleId != RoleEnum.TT.ToString() && t.RoleId != RoleEnum.TP.ToString()).ToListAsync();
        }
    }
}