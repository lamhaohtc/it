using Managing_Teacher_Work.Enums;
using Managing_Teacher_Work.Helpers;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Managing_Teacher_Work.Controllers
{
    public class ActivityController : BaseController
    {
        private bool isAddNew;
        private readonly IActivityService _activityService;
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public async Task<ActionResult> Index()
        {
            var activities = await _activityService.GetActivityListAsync();
            ViewBag.Activity = activities;

            return View();
        }

        public async Task<ActionResult> Add(Activity model, string submit) 
        {
            if(model != null)
            {
                if(submit == "Thêm")
                {
                    isAddNew = true;
                    var activity = new Activity
                    {
                        Title = model.Title,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        Address = model.Address,
                        ActivityType = model.ActivityType,
                        Description = model.Description,
                    };

                    activity.ActivityStatus = DateTimeHelper.SetActivityStatus(activity.StartDate, activity.EndDate);
                    await _activityService.AddActivityAsync(activity);
                    SetAlert("Thêm thông tin thành công!", "success");
                    return RedirectToAction("Index");
                }
                else if(submit == "Cập Nhật")
                {
                    isAddNew = false;

                    var activityById = await _activityService.GetActivityByIdAsync(model.ID);
                    activityById.StartDate = model.StartDate;
                    activityById.Title = model.Title;
                    activityById.ActivityType = model.ActivityType;
                    activityById.Description = model.Description;
                    activityById.EndDate = model.EndDate;
                    activityById.Address = model.Address;

                    await _activityService.UpdateActivityAsync(activityById);

                    SetAlert("Cập nhật thông tin thành công!", "success");
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> Delete(int id)
        {
            await _activityService.DeleteActivityByIdAsync(id);
            SetAlert("Xoá thành công!", "success");

            return RedirectToAction("Index");
        }
    }
}