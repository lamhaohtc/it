using Managing_Teacher_Work.Enums;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.Services;
using Managing_Teacher_Work.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Managing_Teacher_Work.Controllers
{
    [ValidateInput(false)]
    public class TeacherRoleController : BaseController
    {
        private bool isAddNew;
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

            teacherModel.OrderBy(t => t.Role.Name).ToList();

            ViewBag.teacherRoleList = teacherModel;
            ViewBag.teacherMemberList = await _teacherService.GetTeacherListAsync();

            return View();
        }

        public async Task<ActionResult> Add(Teacher model, string submit)
        {
            if (model != null)
            {
                if (submit == "Thêm")
                {
                    isAddNew = true;
                    var teacher = new Teacher
                    {
                        Address = model.Address,
                        DateOfBirth = model.DateOfBirth,
                        Gender = model.Gender,
                        Name_Teacher = model.Name_Teacher,
                        Phone = model.Phone,
                        Email = model.Email,
                        RoleId = model.RoleId,
                        Role = model.Role,
                        CreatedDate = model.CreatedDate,
                        ModifiedDate = model.ModifiedDate
                    };
                    await _teacherService.AddTacherAsync(teacher);
                    SetAlert("Thêm thông tin thành công!", "success");

                    return RedirectToAction("Index");
                }
                else if (submit == "Cập Nhật")
                {
                    isAddNew = false;

                    var teacherId = await _teacherService.GetTeacherByIdAysnc(model.ID);
                    teacherId.Address = model.Address;
                    teacherId.DateOfBirth = model.DateOfBirth;
                    teacherId.Gender = model.Gender;
                    teacherId.Name_Teacher = model.Name_Teacher;
                    teacherId.Phone = model.Phone;
                    teacherId.Email = model.Email;
                    teacherId.RoleId = model.RoleId;
                    teacherId.Role = model.Role;
                    teacherId.CreatedDate = model.CreatedDate;
                    teacherId.ModifiedDate = model.ModifiedDate;

                    await _teacherService.UpdateTeacherAsync(teacherId);

                    SetAlert("Cập nhật thông tin thành công!", "success");
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> getList(int id)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var hs = await _teacherService.GetTeacherByIdAysnc(id);
            var result = JsonConvert.SerializeObject(hs, Formatting.Indented, jss);

            return this.Json(result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _teacherService.DeteleTeacherByIdAsync(id);
             
            return RedirectToAction("Index");
        }
    }
}