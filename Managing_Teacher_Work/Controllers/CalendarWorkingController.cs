using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.DAO;
using Newtonsoft.Json;
using Managing_Teacher_Work.ViewModels;
using System.IO;
using Managing_Teacher_Work.Services;
using System.Threading.Tasks;

namespace Managing_Teacher_Work.Controllers
{
    [ValidateInput(false)]
    
    public class CalendarWorkingController : BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly ICalendarWorkingService _calendarWorkingService;
        private readonly ITypeCalendarService _typeCalendarService;
        private readonly ITeacherService _teacherService;
        private readonly IWorkService _workService;
        public CalendarWorkingController(AppDbContext dbContext, 
            ICalendarWorkingService calendarWorkingService,
            ITypeCalendarService typeCalendarService,
            ITeacherService teacherService,
            IWorkService workService)
        {
            _dbContext = dbContext;
            _calendarWorkingService = calendarWorkingService;
            _typeCalendarService = typeCalendarService;
            _teacherService = teacherService;
            _workService = workService;
        }
        public bool isAddNew;
        public async Task<ActionResult> Index()
        {
            var calendarWorkingModel = new List<CalendarWorkingViewModel>();
            var calendarWorkingList = await _calendarWorkingService.GetCalendarWorkingListAsyc();

            calendarWorkingList.ForEach(clw =>
            {
                calendarWorkingModel.Add(new CalendarWorkingViewModel(
                        clw.ID,
                        clw.Name_CalendarWorking,
                        clw.Description,
                        clw.DateStart,
                        clw.DateEnd,
                        clw.Address,
                        clw.TeacherID,
                        clw.WorkID,
                        clw.TypeCalendarID,
                        clw.WorkState,
                        clw.Status
                    ));
            });

            ViewBag.listCW = calendarWorkingModel;
            ViewBag.listTeacher = await _teacherService.GetTeacherListAsync();
            ViewBag.listWork = await _workService.GetWorkListAsync();
            ViewBag.listTypeCalendar = await _typeCalendarService.GetTypeCalendarListAsync();

            return View();
        }
        public ActionResult getList(int id)
        {

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var hs = _dbContext.CalendarWorking.SingleOrDefault(x => x.ID == id);
            var result = JsonConvert.SerializeObject(hs, Formatting.Indented, jss);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(CalendarWorking model, string submit)
        {
            if (submit == "Thêm")
            {
                isAddNew = true;
                if (model != null)
                {
                    model.Name_CalendarWorking = model.Name_CalendarWorking.ToString();

                    model.Description = model.Description.ToString();
                    model.DateStart = model.DateStart;
                    model.DateEnd = model.DateEnd;
                    model.Address = model.Address != null ? model.Address : "";
                    model.TeacherID = model.TeacherID;
                    model.WorkID = model.WorkID;
                    model.TypeCalendarID = model.TypeCalendarID;
                    model.WorkState = model.WorkState;
                    model.Status = model.Status;
                    model.CreatedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);

                    _dbContext.CalendarWorking.Add(model);
                    _dbContext.SaveChanges();
                    model = null;
                }
                SetAlert("Thêm thông tin thành công!", "success");
                return RedirectToAction("Index");
            }
            else if (submit == "Cập Nhật")
            {
                isAddNew = false;
                if (model != null)
                {
                    var list = _dbContext.CalendarWorking.SingleOrDefault(x => x.ID == model.ID);
                    list.Name_CalendarWorking = model.Name_CalendarWorking;
                    list.Description = model.Description;
                    list.DateStart = model.DateStart;
                    list.DateEnd = model.DateEnd;
                    list.Address = model.Address.ToString();
                    list.TeacherID = model.TeacherID;
                    list.WorkID = model.WorkID;
                    list.TypeCalendarID = model.TypeCalendarID;
                    list.WorkState = model.WorkState;
                    list.Status = model.Status;
                    list.ModifiedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);
                    _dbContext.SaveChanges();
                    model = null;
                }
                SetAlert("Cập nhật thông tin thành công!", "success");
                return RedirectToAction("Index");
            }
            else if (submit == "Tìm")
            {
                if (!string.IsNullOrEmpty(model.Name_CalendarWorking))
                {
                    List<CalendarWorking> list = GetData().Where(s => s.Name_CalendarWorking.Contains(model.Name_CalendarWorking)).ToList();
                    return View("Index", list);
                }
                else
                {
                    List<CalendarWorking> list = GetData();
                    return View("Index", list);
                }
            }
            else
            {
                List<CalendarWorking> list = GetData().OrderBy(s => s.Name_CalendarWorking).ToList();
                return View("Index", list);
            }
        }
        public List<CalendarWorking> GetData()
        {
            return _dbContext.CalendarWorking.ToList();


        }
        public ActionResult Delete(int id)
        {
            new CalendarWorkingDao().Delete(id);
            SetAlert("Xoá thành công!", "success");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }
    }

}