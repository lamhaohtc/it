using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.DAO;
using Newtonsoft.Json;
using Managing_Teacher_Work.ViewModels;

namespace Managing_Teacher_Work.Controllers
{
    public class ClassController : BaseController
    {
        private readonly AppDbContext _dbContext;
        public bool isThemMoi;
        public ClassController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public ActionResult Index(string id)
        {
            
            List<Teacher> listTeacher = _dbContext.Teacher.ToList();
            ViewBag.listTeacher = listTeacher;
            List<Science> listScience = _dbContext.Science.ToList();
            ViewBag.listScience = listScience;
            var listClass = new List<ClassViewModel>();
            _dbContext.Class.ToList().ForEach(item =>
            {
                listClass.Add(new ClassViewModel(item.ID, item.ClassCode, item.ClassName, item.TeacherID, item.ScienceID));
            });

            ViewBag.listClass = listClass;
          
                return View();
        }
        public ActionResult getList(int id)
        {

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var hs = _dbContext.Class.SingleOrDefault(x => x.ID == id);
            var result = JsonConvert.SerializeObject(hs, Formatting.Indented, jss);
            return this.Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Add(Class model, string submit)
        {
            if (submit == "Thêm")
            {
                isThemMoi = true;
                if (model != null)
                {
                    model.ClassCode = model.ClassCode.ToString();
                    model.ClassName = model.ClassName.ToString();
                    model.TeacherID = model.TeacherID;
                    model.ScienceID = model.ScienceID;
                    model.CreatedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);

                    _dbContext.Class.Add(model);
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
                    var list = _dbContext.Class.SingleOrDefault(x => x.ID == model.ID);
                    list.ClassCode = model.ClassCode;
                    list.ClassName = model.ClassName.ToString();
                    list.TeacherID = model.TeacherID;
                    list.ScienceID   = model.ScienceID;
                    list.ModifiedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);
                    _dbContext.SaveChanges();
                  
                }
                SetAlert("Cập nhật thông tin thành công!", "success");
                return RedirectToAction("Index");
            }
            else if (submit == "Tìm")
            {
                if (!string.IsNullOrEmpty(model.ClassCode))
                {
                    List<Class> list = GetData().Where(s => s.ClassCode.Contains(model.ClassCode)).ToList();
                    return View("Index", list);
                }
                else
                {
                    List<Class> list = GetData();
                    return View("Index", list);
                }
            }
            else
            {
                List<Class> list = GetData().OrderBy(s => s.ClassCode).ToList();
                return View("Index", list);
            }
        }
        public List<Class> GetData()
        {
            return _dbContext.Class.ToList();

        }
        public ActionResult Delete(int id)
        {
            new ClassDao().Delete(id);
            SetAlert("Xoá thành công!", "success");
            return RedirectToAction("Index");
        }

    }
}