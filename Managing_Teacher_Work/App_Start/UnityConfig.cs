using Managing_Teacher_Work.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Managing_Teacher_Work
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            // add services
            container.RegisterType<IScienseService, ScienseService>();
            container.RegisterType<IAuthService, AuthService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IClassService, ClassService>();
            container.RegisterType<IStudentService, StudentService>();
            container.RegisterType<ITeacherService, TeacherService>();
            container.RegisterType<ITypeCalendarService, TypeCalandarService>();
            container.RegisterType<IWorkService, WorkService>();
            container.RegisterType<ICalendarWorkingService, CalendarWorkingService>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<ITransactionService, TransactionService>();
            container.RegisterType<IActivityService, ActivityService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}