using Managing_Teacher_Work.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Managing_Teacher_Work.Services
{
    public interface ITeacherActitvityService
    {
        Task<List<TeacherActivity>> GetActivityByTeacherId(int teacherId);
        Task<List<TeacherActivity>> GetListTeacherByActivityId(int activityId);
        Task<bool> IsTeacherRegisterActivity(int teacherid, int activity);
        Task<bool> DeleteByIdAsync(int id, int teacherID);
        Task<TeacherActivity> GetById(int id);
        Task<TeacherActivity> AddTeacherActivityAsync(TeacherActivity model);
    }
    public class TeacherActivityService : ITeacherActitvityService
    {
        private readonly AppDbContext _dbContext;
        public TeacherActivityService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TeacherActivity> AddTeacherActivityAsync(TeacherActivity model)
        {
            _dbContext.TeacherActivities.Add(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<bool> DeleteByIdAsync(int id, int teacherID)
        {
            var entity = await _dbContext.TeacherActivities.Where(x => x.TeacherId == teacherID && x.ActivityId == id).FirstOrDefaultAsync();
            if(entity != null)
            {
                _dbContext.TeacherActivities.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<TeacherActivity>> GetActivityByTeacherId(int teacherId)
        {
            return await _dbContext.TeacherActivities.Where(t => t.TeacherId == teacherId).ToListAsync();
        }

        public async Task<TeacherActivity> GetById(int id)
        {
            return await _dbContext.TeacherActivities.FindAsync(id);
        }

        public async Task<List<TeacherActivity>> GetListTeacherByActivityId(int activityId)
        {
            return await _dbContext.TeacherActivities.Where(t => t.ActivityId == activityId).ToListAsync();
        }

        public async Task<bool> IsTeacherRegisterActivity(int teacherid, int activity)
        {
            var result = await _dbContext.TeacherActivities.Where(t => t.ActivityId == activity && t.TeacherId == teacherid).FirstOrDefaultAsync();
            if (result == null)
                return false;

            return true;
        }
    }
}