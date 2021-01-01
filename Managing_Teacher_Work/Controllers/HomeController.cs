using Managing_Teacher_Work.DAO;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Managing_Teacher_Work.SQLEDM;
using System.Threading.Tasks;
using System.Data.Entity;
using Managing_Teacher_Work.Services;
using System;
using Managing_Teacher_Work.Enums;
using Managing_Teacher_Work.Helpers;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Managing_Teacher_Work.Controllers
{
    public class HomeController : BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly IScienseService _scienseService;
        private readonly ITeacherService _teacherService;
        private readonly ICalendarWorkingService _calendarWorkingService;
        private readonly ITypeCalendarService _typeCalendarService;
        private readonly IWorkService _workService;
        private readonly IMenuService _menuService;
        private readonly IActivityService _activityService;
        private readonly ITeacherActitvityService _teacherActitvityService;
        private readonly ITransactionService _transactionService;
        public HomeController(AppDbContext dbContext,
            IScienseService scienseService,
            ITeacherService teacherService,
            ICalendarWorkingService calendarWorkingService,
            ITypeCalendarService typeCalendarService,
            IWorkService workService,
            IMenuService menuService,
            IActivityService activityService,
            ITeacherActitvityService teacherActitvityService,
            ITransactionService transactionService)
        {
            _dbContext = dbContext;
            _scienseService = scienseService;
            _teacherService = teacherService;
            _calendarWorkingService = calendarWorkingService;
            _typeCalendarService = typeCalendarService;
            _workService = workService;
            _menuService = menuService;
            _activityService = activityService;
            _teacherActitvityService = teacherActitvityService;
            _transactionService = transactionService;
        }
        public async Task<ActionResult> Index()
        {
            var listCW = new List<CalendarWorkingViewModel>();
            var calendarWorkings = await _calendarWorkingService.GetCalendarWorkingListAsyc();
            calendarWorkings.ForEach(S =>
            {
                listCW.Add(new CalendarWorkingViewModel(
                    S.ID,
                    S.Name_CalendarWorking,
                    S.Description,
                    S.DateStart,
                    S.DateEnd,
                    S.Address,
                    S.TeacherID,
                    S.WorkID,
                    S.TypeCalendarID,
                    S.WorkState,
                    S.Status
                ));
            });

            ViewBag.listCW = listCW;
            var dao = new CalendarWorkingDao();
            var model = dao.ListAll();

            return View(model);
        }

        public async Task<PartialViewResult> LeftMenu()
        {
            ViewBag.listMenu = await _menuService.GetMenuListAsync();

            return PartialView();
        }
        public async Task<ActionResult> ManageTeacher()
        {
            var listScience = await _scienseService.GetScienseListAsync();
            ViewBag.listScience = listScience;

            return View();
        }

        public ActionResult ManageStudent()
        {
            var listClass = new List<ClassViewModel>();
            _dbContext.Class.ToList().ForEach(item =>
            {
                listClass.Add(new ClassViewModel(item.ID, item.ClassCode, item.ClassName, item.TeacherID, item.ScienceID));
            });

            ViewBag.listClass = listClass;
            return View();
        }

        public ActionResult ManageClass()
        {
            return View();
        }


        public ActionResult CalendarNote()
        {
            return View();
        }
        public ActionResult CalNote()
        {
            return View();
        }
        public ActionResult Extention()
        {
            return View();
        }

        public ActionResult ListStudent(long idclass)
        {
            var studentDao = new StudentDao();
            var classDao = new ClassDao();
            var ClassDetails = classDao.GetClassById(idclass);
            ViewBag.ClassDetails = ClassDetails;
            var ListStudentOfClass = studentDao.GetListStudentByClassId(idclass);
            ViewBag.ListStudent = ListStudentOfClass;
            return View();
        }
        public async Task<ActionResult> ListTeacher(int scienseId)
        {
            ViewBag.science = await _scienseService.GetScienseByIdAsync(scienseId);
            ViewBag.listTeacher = await _teacherService.GetTeacherByScienseIdAsync(scienseId);

            return View();
        }

        public async Task<ActionResult> CalendarWorkingDetails(int id)
        {
            var cw = await _calendarWorkingService.GetCalendarWorkingByIdAsync(id);

            ViewBag.Cw = cw;
            ViewBag.teacher = await _teacherService.GetTeacherByIdAysnc(cw.TeacherID);
            ViewBag.work = await _workService.GetWorkByIdAsync(cw.WorkID);
            ViewBag.typecalendar = await _typeCalendarService.GetTypeCalendarByIdAsync(cw.TypeCalendarID);

            return View();
        }

        public async Task<ActionResult> CalendarWorkingDetails_Level2(int id)
        {
            var cw = await _calendarWorkingService.GetCalendarWorkingByIdAsync(id);

            ViewBag.Cw = cw;
            ViewBag.teacher = await _teacherService.GetTeacherByIdAysnc(cw.TeacherID);
            ViewBag.work = await _workService.GetWorkByIdAsync(cw.WorkID);
            ViewBag.typecalendar = await _typeCalendarService.GetTypeCalendarByIdAsync(cw.TypeCalendarID);

            return View();
        }


        public JsonResult GetEvents()
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var events = dc.Events.ToList();
                return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            }
        }


        [HttpPost]
        public JsonResult SaveEvent(Events e)
        {
            var status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                if (e.EventID > 0)
                {

                    var v = dc.Events.Where(a => a.EventID == e.EventID).FirstOrDefault();
                    if (v != null)
                    {
                        v.Subject = e.Subject;
                        v.Start = e.Start;
                        v.End = e.End;
                        v.Description = e.Description;
                        v.IsFullDay = e.IsFullDay;
                        v.ThemeColor = e.ThemeColor;
                    }
                }
                else
                {
                    dc.Events.Add(e);
                }

                dc.SaveChanges();
                status = true;

            }
            return new JsonResult { Data = new { status = status } };
        }

        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            var status = false;
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var v = dc.Events.Where(a => a.EventID == eventID).FirstOrDefault();
                if (v != null)
                {
                    dc.Events.Remove(v);
                    dc.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }

        public async Task<ActionResult> ActivityList()
        {
            ViewBag.ActivityList = await _activityService.GetActivityListAsync();

            return View();
        }

        public async Task<ActionResult> ActivityDetail(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            ViewBag.Activity = activity;

            return View();
        }

        public async Task<ActionResult> ActivityMoreDetail(int id)
        {
            var activity = await _activityService.GetActivityByIdAsync(id);
            ViewBag.Activity = activity;

            return View();
        }

        public async Task<ActionResult> TotalTransaction()
        {
            var model = new TotalTransactionViewModel();

            var totalCharityFee = await _transactionService.TotalCharityFee();
            var totalDonateFee = await _transactionService.TotalDonateFee();
            var totalUnionFee = await _transactionService.TotalUnionFee();

            model.TotalCharityFee = totalCharityFee;
            model.TotalDonateFee = totalDonateFee;
            model.TotalUnionFee = totalUnionFee;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> TotalTransactionView()
        {
            ViewBag.TotalCharityFee = await _transactionService.TotalCharityFee();
            ViewBag.TotalDonateFee = await _transactionService.TotalDonateFee();
            ViewBag.TotalUnionFee = await _transactionService.TotalUnionFee();
            ViewBag.TotalAll = await _transactionService.TotalAllTransaction();

            return View();
        }

        public async Task<ActionResult> GetTotalTransactionByMonth()
        {
            List<DataPoint> dataPoints1 = new List<DataPoint>();
            List<DataPoint> dataPoints2 = new List<DataPoint>();
            List<DataPoint> dataPoints3 = new List<DataPoint>();

            foreach (MonthEnum month in Enum.GetValues(typeof(MonthEnum)))
            {
                var totalCharityAmount = await _transactionService.GetTotalTransactionByTypeAndMonth(month, TransactionType.CHARITY_FEE);
                var totalDonateAmount = await _transactionService.GetTotalTransactionByTypeAndMonth(month, TransactionType.DONATE);
                var totalUnionAmount = await _transactionService.GetTotalTransactionByTypeAndMonth(month, TransactionType.UNION_FEE);

                if (totalCharityAmount > 0)
                {
                    dataPoints1.Add(new DataPoint(EnumHelper.GetEnumDescription(month), totalCharityAmount));
                }

                if (totalDonateAmount > 0)
                {
                    dataPoints2.Add(new DataPoint(EnumHelper.GetEnumDescription(month), totalDonateAmount));
                }

                if (totalUnionAmount > 0)
                {
                    dataPoints3.Add(new DataPoint(EnumHelper.GetEnumDescription(month), totalUnionAmount));
                }


            }

            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);
            ViewBag.DataPoints2 = JsonConvert.SerializeObject(dataPoints2);
            ViewBag.DataPoints3 = JsonConvert.SerializeObject(dataPoints3);

            return View();

        }


    }
}