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
        public FontendController(ITeacherService teacherService, IScienseService scienseService)
        {
            _teacherService = teacherService;
            _scienseService = scienseService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> ProfileTeacher(int id)
        {
            var teacher = await _teacherService.GetTeacherByIdAysnc(id);
            var science = await _scienseService.GetScienseByIdAsync(teacher.SicenceID);
            ViewBag.teacher = teacher;
            ViewBag.science = science;

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