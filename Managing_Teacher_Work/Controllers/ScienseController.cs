using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.DAO;
using Newtonsoft.Json;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Managing_Teacher_Work.Services;
using System.Threading.Tasks;
using Managing_Teacher_Work.ViewModels;

namespace Managing_Teacher_Work.Controllers
{
    [ValidateInput(false)]
    public class ScienseController : BaseController
    {
        // GET: Sciense
        public bool isThemMoi;
        public User currentUser;
        private readonly AppDbContext _dbContext;
        private readonly IScienseService _scienseService;
        public ScienseController(
            AppDbContext dbContext, 
            IScienseService scienseService)
        {
            _dbContext = dbContext;
            _scienseService = scienseService;
        }

        public async Task<ActionResult> Index()
        {
            //isThemMoi = true;
            //ViewBag.Check = isThemMoi;

            //string maincnn = ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
            //SqlConnection sqlcnn = new SqlConnection(maincnn);
            //string condition = "select * from Science where (1=1) order by CreatedDate desc";
            //SqlDataAdapter sqlda = new SqlDataAdapter(condition, maincnn);
            //sqlcnn.Open();
            //DataTable dt = new DataTable();
            //sqlda.Fill(dt);
            //IEnumerable<Science> model = ConvertToTankReadings(dt);

            //ViewBag.listSciense = model;
            //sqlcnn.Close();
            var scienceModel = new List<ScienceViewModel>();
            var scienses = await _scienseService.GetScienseListAsync();
            scienses.ForEach(sc =>
            {
                scienceModel.Add(new ScienceViewModel
                {
                    ID = sc.ID,
                    Address = sc.Address,
                    Description = sc.Description,
                    Founding = sc.Founding,
                    Name = sc.Name
                });
            });

            ViewBag.ScienceList = scienceModel;
            
            return View();
        }

        private IEnumerable<Science> ConvertToTankReadings(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new Science
                {
                    ID = Convert.ToInt32(row["ID"]),
                    Name = Convert.ToString(row["Name"]),
                    Address = Convert.ToString(row["Address"]),
                    Description = Convert.ToString(row["Description"]),
                    Founding = Convert.ToDateTime(row["Founding"])

                };
            }

        }

        public ActionResult getList(int id)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var hs = _dbContext.Science.SingleOrDefault(x => x.ID == id);
            var result = JsonConvert.SerializeObject(hs, Formatting.Indented, jss);
            return this.Json(result, JsonRequestBehavior.AllowGet);
                           
        }

        public ActionResult Add(Science model, string submit)
        {
            if (submit == "Thêm")
            {     
                if (model != null)
                {
                    model.Name = model.Name.ToString().Trim()??"";
                    model.Address = model.Address.ToString().Trim()??"";
                    if(model.Description!=null)
                    {
                        model.Description = model.Description;
                    }
                   
                    model.Founding = model.Founding;
                    model.CreatedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);   

                    _dbContext.Science.Add(model);
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
                    var list = _dbContext.Science.SingleOrDefault(x => x.ID == model.ID);
                    list.Name = model.Name;
                    list.Address = model.Address.ToString();
                    if(list.Description!=null)
                    {
                        list.Description = model.Description;
                    }
                    
                    list.Founding = model.Founding;
                    list.ModifiedDate = model.CreatedDate.GetValueOrDefault(System.DateTime.Now);
                    _dbContext.SaveChanges();
                    model = null;
                }
                SetAlert("Cập nhật khoa thành công!", "success");
                return RedirectToAction("Index");
            }
            else if (submit == "Tìm")
            {
                if (!string.IsNullOrEmpty(model.Name))
                {
                    List<Science> list = GetData().Where(s => s.Name.Contains(model.Name)).ToList();
                    return View("Index", list);
                }
                else
                {
                    List<Science> list = GetData();
                    return View("Index", list);
                }
            }
            else
            {
                List<Science> list = GetData().OrderBy(s => s.Name).ToList();
                return View("Index", list);
            }
        }

        public List<Science> GetData()
        {
            return _dbContext.Science.ToList();


        }

        public ActionResult Delete(int id)
        {
            new ScienseDao().Delete(id);
            SetAlert("Xoá thành công!", "success");
            return RedirectToAction("Index");
        }
      
    }
}