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
    public class TransactionController : BaseController
    {
        private bool isAddNew;
        private readonly ITransactionService _transactionService;
        private readonly ITeacherService _teacherService;
        public TransactionController(ITransactionService transactionService,
            ITeacherService teacherService)
        {
            _transactionService = transactionService;
            _teacherService = teacherService;
        }
        public async Task<ActionResult> Index()
        {
            var transactionList = await _transactionService.GetTransactionListAsync();
            ViewBag.Transactions = transactionList;
            ViewBag.TeacherList = await _teacherService.GetAllTeacherListAsync();

            return View();
        }

        public async Task<ActionResult> Add(Transaction model, string submit)
        {
            if (model != null)
            {
                if (submit == "Thêm")
                {
                    isAddNew = true;
                    var transaction = new Transaction
                    {
                        Amout = model.Amout,
                        IsPaid = model.IsPaid,
                        TeacherId = model.TeacherId,
                        TransactionType = model.TransactionType,
                        CreatedDate = model.CreatedDate,
                        ModifiedDate = model.ModifiedDate
                    };
                    await _transactionService.AddTransactionAsync(transaction);

                    SetAlert("Thêm thông tin thành công!", "success");

                    return RedirectToAction("Index");
                }
                else if (submit == "Cập Nhật")
                {
                    isAddNew = false;

                    var entity = await _transactionService.GetTransactionByIdAync(model.ID);
                    entity.TransactionType = model.TransactionType;
                    entity.TeacherId = model.TeacherId;
                    entity.Amout = model.Amout;
                    model.IsPaid = model.IsPaid;
                    entity.CreatedDate = model.CreatedDate;
                    entity.ModifiedDate = model.ModifiedDate;

                    await _transactionService.UpdateTransactionAsync(entity);

                    SetAlert("Cập nhật thông tin thành công!", "success");
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> getList(int id)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var hs = await _transactionService.GetTransactionByIdAync(id);
            var result = JsonConvert.SerializeObject(hs, Formatting.Indented, jss);

            var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;

        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _transactionService.DeleteTransactionByIdAsync(id);

            return RedirectToAction("Index");
        }

    }
}