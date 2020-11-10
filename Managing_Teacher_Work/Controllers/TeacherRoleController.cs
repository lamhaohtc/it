using Managing_Teacher_Work.Enums;
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
    public class TeacherRoleController : BaseController
    {
        private readonly TeacherService _teacherService;
        public TeacherRoleController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        public async Task<ActionResult> Index()
        {
            var teacherModel = new List<TeacherViewModel>();
            var teacherList = await _teacherService.GetTeacherRoleAsync();

            teacherList.ForEach(t =>
            {
                teacherModel.Add(new TeacherViewModel
                {
                    ID = t.ID,
                    Address = t.Address,
                    DateOfBirth = t.DateOfBirth,
                    Gender = t.Gender,
                    Name_Teacher = t.Name_Teacher,
                    Phone = t.Phone,
                    Email = t.Email,
                    RoleId = t.RoleId,
                    Role = t.Role,
                    CreatedDate = t.CreatedDate,
                    ModifiedDate = t.ModifiedDate                   
                });
            });

            ViewBag.listTeacher = teacherModel;

            return View();
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _teacherService.DeteleTeacherByIdAsync(id);
             
            return RedirectToAction("Index");
        }
    }
}