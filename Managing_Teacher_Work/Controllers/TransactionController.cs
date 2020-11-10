using Managing_Teacher_Work.Enums;
using Managing_Teacher_Work.Services;
using Managing_Teacher_Work.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Managing_Teacher_Work.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        public async Task<ActionResult> Index()
        {
            var model = new List<TransactionViewModel>();
            var transactionList = await _transactionService.GetTransactionListAsync();
            transactionList.ForEach(t =>
            {
                model.Add(new TransactionViewModel
                {
                    ID = t.ID,
                    Amout = t.Amout,
                    CreatedDate = t.CreatedDate,
                    IsPaid = t.IsPaid,
                    TeacherName = t.Teacher.Name_Teacher,
                    TransactionType = (TransactionType)t.TransactionType
                });
            });

            ViewBag.Transactions = model;

            return View();
        }

    }
}