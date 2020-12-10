using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.DAO;
using Newtonsoft.Json;
using System.Globalization;
using Managing_Teacher_Work.ViewModels;
using Managing_Teacher_Work.Services;
using System.Threading.Tasks;

namespace Managing_Teacher_Work.Controllers
{
    public class StudentController : BaseController
    {
        public bool isThemMoi;
        private readonly AppDbContext _dbContext;
        private readonly IStudentService _studentService;
        private readonly IClassService _classService;
        public StudentController(
            AppDbContext dbContext, 
            IStudentService studentService,
            IClassService classService)
        {
            _dbContext = dbContext;
            _studentService = studentService;
            _classService = classService;
        }
        public async Task<ActionResult> Index()
        {
            var studentsList = new List<StudentViewModel>();
            var students = await _studentService.GetStudentListAsync();

            students.ForEach(student =>
            {
                studentsList.Add(new StudentViewModel(
                    student.ID, 
                    student.Name_Student, 
                    student.ClassID, 
                    student.DateOfBirth, 
                    student.Address, 
                    student.Email, 
                    student.Phone));
            });

            ViewBag.listStudent = studentsList;
            var classList = await _classService.GetClassListAsync();
            ViewBag.listClass = classList;

            return View();
        }
        public ActionResult getList(int id)
        {

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var hs = _dbContext.Student.SingleOrDefault(x => x.ID == id);
            var result = JsonConvert.SerializeObject(hs, Formatting.Indented, jss);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(Student model, string submit)
        {
            if (submit == "Thêm")
            {


                isThemMoi = true;
                if (model != null)
                {
                    model.Name_Student = model.Name_Student.ToString().Trim()??"";
                    model.DateOfBirth = model.DateOfBirth;
                    model.Address = model.Address.ToString().Trim()??"";
                    model.Email = model.Email??"";
                    model.Phone = model.Phone.ToString().Trim()??"";
                    model.ClassID = model.ClassID;
                    model.CreatedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);
                   

                    _dbContext.Student.Add(model);
                    _dbContext.SaveChanges();
                    model = null;
                }
                SetAlert("Thêm thông tin thành công!", "success");
                return RedirectToAction("Index");
            }
            else if (submit == "Cập Nhật")
            {
                isThemMoi = false;
                if (model != null)
                {
                    var list = _dbContext.Student.SingleOrDefault(x => x.ID == model.ID);
                    list.Name_Student = model.Name_Student.ToString().Trim();
                    list.DateOfBirth = model.DateOfBirth;
                    list.Address = model.Address.ToString().Trim();
                    list.Email = model.Email;
                    list.Phone = model.Phone.ToString().Trim();
                    list.ClassID = model.ClassID;
                    list.ModifiedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);
                    _dbContext.SaveChanges();
                    model = null;
                }
                SetAlert("Cập nhật thông tin thành công!", "success");
                return RedirectToAction("Index");
            }
            else if (submit == "Tìm")
            {
                if (!string.IsNullOrEmpty(model.Name_Student))
                {
                    List<Student> list = GetData().Where(s => s.Name_Student.Contains(model.Name_Student)).ToList();
                    return View("Index", list);
                }
                else
                {
                    List<Student> list = GetData();
                    return View("Index", list);
                }
            }
            else
            {
                List<Student> list = GetData().OrderBy(s => s.Name_Student).ToList();
                return View("Index", list);
            }
        }
        public List<Student> GetData()
        {
            return _dbContext.Student.ToList();


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            new StudentDao().Delete(id);
            SetAlert("Xoá thành công!", "success");
            return RedirectToAction("Index");
        }

    }
}