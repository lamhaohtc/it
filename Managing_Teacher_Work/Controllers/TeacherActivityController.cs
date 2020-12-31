using Managing_Teacher_Work.Services;
using Managing_Teacher_Work.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Managing_Teacher_Work.Controllers
{
    public class TeacherActivityController : Controller
    {
        private readonly ITeacherActitvityService _teacherActitvityService;
        private readonly IActivityService _activityService;
        public TeacherActivityController(ITeacherActitvityService teacherActitvityService,
            IActivityService activityService)
        {
            _teacherActitvityService = teacherActitvityService;
            _activityService = activityService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetData()
        {
            ActivityTypeViewModel model = new ActivityTypeViewModel();

            model.CountArt = await _activityService.CountArt();
            model.CountCharity = await _activityService.CountCharity();
            model.CountSport = await _activityService.CountSport();
            model.CountVolunteer = await _activityService.CountVolunteer();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}