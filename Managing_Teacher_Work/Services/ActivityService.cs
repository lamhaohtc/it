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
    public interface IActivityService
    {
        Task<List<Activity>> GetActivityListAsync();
        Task<bool> DeleteActivityByIdAsync(int id);
        Task<List<Activity>> GetActivityListByTeacherId(int teacherId);
        Task<List<Activity>> GetActivityLisrByTeacherIdAndType(int teacherId, ActivityTypeEnum activityType);
        Task<Activity> GetActivityByIdAsync(int id);
        Task<Activity> AddActivityAsync(Activity model);
        Task UpdateActivityAsync(Activity activity);
    }
    public class ActivityService : IActivityService
    {
        private readonly AppDbContext _dbContext;
        public ActivityService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Activity> AddActivityAsync(Activity activity)
        {
            _dbContext.Activities.Add(activity);
            await _dbContext.SaveChangesAsync();

            return activity;
        }

        public async Task<bool> DeleteActivityByIdAsync(int id)
        {
            var activity = await GetActivityByIdAsync(id);
            if(activity != null)
            {
                _dbContext.Activities.Remove(activity);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Activity> GetActivityByIdAsync(int id)
        {
            return await _dbContext.Activities.FindAsync(id);
        }

        public async Task<List<Activity>> GetActivityLisrByTeacherIdAndType(int teacherId, ActivityTypeEnum activityType)
        {
            return await _dbContext.Activities.Where(a => a.TeacherActivities.FirstOrDefault().TeacherId == teacherId && a.ActivityType == (int)activityType).ToListAsync();
        }

        public async Task<List<Activity>> GetActivityListAsync()
        {
            return await _dbContext.Activities.OrderByDescending(a => a.StartDate).ToListAsync();
        }

        public async Task<List<Activity>> GetActivityListByTeacherId(int teacherId)
        {
            return await _dbContext.Activities.Where(a => a.TeacherActivities.FirstOrDefault().TeacherId == teacherId).ToListAsync();
        }

        public async Task UpdateActivityAsync(Activity activity)
        {
            _dbContext.Entry(activity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}