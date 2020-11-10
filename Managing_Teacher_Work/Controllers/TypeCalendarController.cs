using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.DAO;
using Newtonsoft.Json;

namespace Managing_Teacher_Work.Controllers
{
    [ValidateInput(false)]
    public class TypeCalendarController : BaseController
    {
        private readonly AppDbContext _dbContext;
        public bool isThemMoi;
        public string MessageIndex;
        public TypeCalendarController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            List<TypeCalendar> listTypeCalendar = _dbContext.TypeCalendar.ToList();
            ViewBag.listTypeCalendar = listTypeCalendar;

            return View();
        }
        public ActionResult getList(int id)
        {

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var hs = _dbContext.TypeCalendar.SingleOrDefault(x => x.ID == id);
            var result = JsonConvert.SerializeObject(hs, Formatting.Indented, jss);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(TypeCalendar model, string submit)
        {
            if (submit == "Thêm")
            {


                isThemMoi = true;
                if (model != null)
                {

                    model.TypeName = model.TypeName.ToString().Trim();
                    model.TypeDescription = model.TypeDescription.ToString();
                    model.Status = model.Status.ToString();
                    model.CreatedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);

                    _dbContext.TypeCalendar.Add(model);
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
                    var list = _dbContext.TypeCalendar.SingleOrDefault(x => x.ID == model.ID);
                    list.TypeName = model.TypeName.ToString().Trim();
                    list.TypeDescription = model.TypeDescription.ToString();
                    list.Status = model.Status.ToString();
                    list.ModifiedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);
                    _dbContext.SaveChanges();
                    model = null;
                }
                SetAlert("Cập nhật thông tin thành công!", "success");
                return RedirectToAction("Index");
            }
            else if (submit == "Tìm")
            {
                if (!string.IsNullOrEmpty(model.TypeName))
                {
                    List<TypeCalendar> list = GetData().Where(s => s.TypeName.Contains(model.TypeName)).ToList();
                    return View("Index", list);
                }
                else
                {
                    List<TypeCalendar> list = GetData();
                    return View("Index", list);
                }
            }
            else
            {
                List<TypeCalendar> list = GetData().OrderBy(s => s.TypeName).ToList();
                return View("Index", list);
            }
        }
        public List<TypeCalendar> GetData()
        {
            return _dbContext.TypeCalendar.ToList();


        }
        public ActionResult Delete(int id)
        {
            new TypeCalendarDao().Delete(id);
            SetAlert("Xoá thành công!", "success");
            return RedirectToAction("Index");
        }

    }
}