using Managing_Teacher_Work.Enums;
using Managing_Teacher_Work.Helpers;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Managing_Teacher_Work.Controllers
{
    [ValidateInput(false)]
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
            ViewBag.ActivityTypeList = EnumHelper.GetEnumList<ActivityTypeEnum>();

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
                        ActivityStatus = (int)ActivityStatus.Ready,
                        FilePath = model.FilePath
                    };
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
                    activityById.FilePath = model.FilePath;

                    await _activityService.UpdateActivityAsync(activityById);

                    SetAlert("Cập nhật thông tin thành công!", "success");
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public JsonResult getList(int id)
        {
            try
            {
                var data = _activityService.GetById(id);

                JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                var result = JsonConvert.SerializeObject(data, Formatting.Indented, jss);

                var jsonResult =  Json(result, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;

                return jsonResult;
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _activityService.DeleteActivityByIdAsync(id);

            return RedirectToAction("Index");
        }
    }
}