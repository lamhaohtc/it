using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.DAO;
using Newtonsoft.Json;
using System.Globalization;
using Managing_Teacher_Work.Common;
using Managing_Teacher_Work.Services;
using System.Threading.Tasks;

namespace Managing_Teacher_Work.Controllers
{
    public class UserController : BaseController
    {
        private readonly AppDbContext _dbContext;
        private readonly TeacherService _teacherService;
        private readonly UserService _userService;
        public UserController(AppDbContext dbContext,
            TeacherService teacherService,
            UserService userService)
        {
            _dbContext = dbContext;
            _teacherService = teacherService;
            _userService = userService;
        }
        public bool isThemMoi;
        public async Task<ActionResult> Index()
        {
            List<User> listUser = await _userService.GetListUserAsync();
            ViewBag.listUser = listUser;
            List<GroupUser> listGroup = _dbContext.GroupUser.ToList();
            ViewBag.listGroup = listGroup;
            ViewBag.TeacherList = await _teacherService.GetAllTeacherListAsync();


            return View();
        }
        public ActionResult getList(int id)
        {

            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var hs = _dbContext.User.SingleOrDefault(x => x.ID == id);
            var result = JsonConvert.SerializeObject(hs, Formatting.Indented, jss);
            return this.Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add(User model, string submit)
        {
            if (submit == "Thêm")
            {


                isThemMoi = true;
                if (model != null)
                {
                    model.Name = model.Name.ToString().Trim();
                    model.UserName = model.UserName.ToString().Trim();
                    model.Password = Encryptor.MD5Hash(model.Password.Trim());
                    model.GroupID = model.GroupID;
                    model.Status = model.Status;
                    model.Phone = model.Phone.ToString().Trim();
                    model.Email = model.Email.ToString().Trim();
                    model.TeacherId = model.TeacherId;
                    model.CreatedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);
                  

                    _dbContext.User.Add(model);
                    _dbContext.SaveChanges();
                    model = null;
                }
                SetAlert("Thêm thông tin thành công!", "success");
                return RedirectToAction("Index");
            }
            else if (submit == "Cập Nhật")
            {
                isThemMoi = false;
                ViewBag.Disable = isThemMoi;
                if (model != null)
                {
                    var list = _dbContext.User.SingleOrDefault(x => x.ID == model.ID);
                    list.Name = model.Name.ToString().Trim();
                    list.UserName = model.UserName.ToString().Trim();
                    list.Password = Encryptor.MD5Hash(model.Password.Trim());
                    list.GroupID = model.GroupID;
                    list.Status = model.Status;
                    list.Phone = model.Phone.ToString().Trim();
                    list.Email = model.Email.ToString().Trim();
                    list.TeacherId = model.TeacherId;
                    list.ModifiedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);
                    _dbContext.SaveChanges();
                    model = null;
                }
                SetAlert("Cập nhật thông tin thành công!", "success");
                return RedirectToAction("Index");
            }
            else if (submit == "Tìm")
            {
                if (!string.IsNullOrEmpty(model.UserName))
                {
                    List<User> list = GetData().Where(s => s.UserName.Contains(model.UserName)).ToList();
                    return View("Index", list);
                }
                else
                {
                    List<User> list = GetData();
                    return View("Index", list);
                }
            }
            else
            {
                List<User> list = GetData().OrderBy(s => s.UserName).ToList();
                return View("Index", list);
            }
        }
        public List<User> GetData()
        {
            return _dbContext.User.ToList();
        }
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            SetAlert("Xoá thành công!", "success");
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            Session[Common.CommonConstants.USER_SESSION] = null;
            return Redirect("/Login/Index");
        }

    }
}