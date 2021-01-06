using System.Web.Mvc;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.DAO;
using Managing_Teacher_Work.Common;
using Managing_Teacher_Work.Services;
using System.Threading.Tasks;

namespace Managing_Teacher_Work.Controllers
{
    public class LoginController : Controller
    {
        public bool alertLogin = false;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public LoginController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.Login(model.UserName, Encryptor.MD5Hash(model.PassWord), true);

                switch (result)
                {
                    case (1):
                        {
                            var user = await _userService.GetUserByUsernameAsync(model.UserName);
                            var userSession = new UserLogin();

                            userSession.UserName = user.UserName;
                            userSession.ID = user.ID;
                            userSession.Name = user.Name;
                            userSession.GroupID = user.GroupID;

                            var listCredentials = _userService.GetUserByUsernameAsync(model.UserName);
                            Session.Add(Managing_Teacher_Work.Common.CommonConstants.USER_SESSION, listCredentials);
                            Session.Add(Managing_Teacher_Work.Common.CommonConstants.USER_SESSION, userSession);

                            return RedirectToAction("Index", "Home");
                        }

                    case (0):
                        {
                            alertLogin = true;
                            ViewBag.alertLogin = alertLogin;
                            Redirect("Login/Index");
                            ViewBag.Mes = "Sai mật khẩu hoặc tài khoản. Vui lòng kiểm tra lại!";

                            break;
                        }

                    case (-1):
                        {
                            alertLogin = true;
                            ViewBag.alertLogin = alertLogin;
                            ViewBag.Mes = "Tài khoản đã bị khoá!";

                            break;
                        }

                    case (-2):
                        {
                            alertLogin = true;
                            ViewBag.alertLogin = alertLogin;
                            ViewBag.Mes = "Sai mật khẩu hoặc tài khoản. Vui lòng kiểm tra lại!";

                            break;
                        }

                    default:
                        {
                            alertLogin = true;
                            ViewBag.alertLogin = alertLogin;
                            ViewBag.Mes = "Đăng nhập không thành công";

                            break;
                        }

                }
            }

            return View("Index");
        }
    }
}