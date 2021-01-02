using Managing_Teacher_Work.Enums;
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
    public interface IActivityService
    {
        Task<List<Activity>> GetActivityListAsync();
        Task<bool> DeleteActivityByIdAsync(int id);
        Task<List<Activity>> GetActivityListByTeacherId(int teacherId);
        Task<List<Activity>> GetActivityLisrByTeacherIdAndType(int teacherId, ActivityTypeEnum activityType);
        Task<Activity> GetActivityByIdAsync(int id);
        Task<Activity> AddActivityAsync(Activity model);
        Task UpdateActivityAsync(Activity activity);
        Task<int> CountArt();
        Task<int> CountSport();
        Task<int> CountCharity();
        Task<int> CountVolunteer();
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

        public async Task<int> CountArt()
        {
            return await _dbContext.Activities.Where(a => a.ActivityType == (int)ActivityTypeEnum.Art).CountAsync();
        }

        public async Task<int> CountCharity()
        {
            return await _dbContext.Activities.Where(a => a.ActivityType == (int)ActivityTypeEnum.Charity).CountAsync();
        }

        public async Task<int> CountSport()
        {
            return await _dbContext.Activities.Where(a => a.ActivityType == (int)ActivityTypeEnum.Sport).CountAsync();
        }

        public async Task<int> CountVolunteer()
        {
            return await _dbContext.Activities.Where(a => a.ActivityType == (int)ActivityTypeEnum.Volunteer).CountAsync();
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
            var list =  await _dbContext.Activities.OrderByDescending(a => a.StartDate).ToListAsync();
            if(list.Count > 0 && list != null)
            {
                foreach(var item in list)
                {
                    item.ActivityStatus = DateTimeHelper.SetActivityStatus(item.StartDate, item.EndDate);
                    await UpdateActivityAsync(item);
                }
            }

            return list;
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