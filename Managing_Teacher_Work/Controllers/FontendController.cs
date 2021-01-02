using Managing_Teacher_Work.DAO;
using Managing_Teacher_Work.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Managing_Teacher_Work.Controllers
{
    public class FontendController : BaseController
    {
        // GET: Fontend
        private readonly ITeacherService _teacherService;
        private readonly IScienseService _scienseService;
        private readonly ITeacherActitvityService _teacherActitvityService;
        public FontendController(ITeacherService teacherService, 
            IScienseService scienseService,
            ITeacherActitvityService teacherActitvityService)
        {
            _teacherService = teacherService;
            _scienseService = scienseService;
            _teacherActitvityService = teacherActitvityService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> ProfileTeacher(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAysnc(id);
            var science = await _scienseService.GetScienseByIdAsync(teacher.SicenceID);
            var activityListByTeacher = await _teacherActitvityService.GetActivityByTeacherId(id);

            ViewBag.teacher = teacher;
            ViewBag.science = science;
            ViewBag.ActivityList = activityListByTeacher;
            ViewBag.ActivityId = activityListByTeacher.Select(a => a.ActivityId).FirstOrDefault();

            return View();
        }
        public ActionResult ProfileStudent(int id)
        {
            var dao = new StudentDao();
            var student = dao.GetStudentById(id);
            ViewBag.student = student;
            var Class = new ClassDao().GetClassById(student.ClassID);
            ViewBag.Class = Class;

            return View();

        }
    }
}