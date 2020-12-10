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
            var transactionList = await _transactionService.GetTransactionListAsync();
            ViewBag.Transactions = transactionList;

            return View();
        }


    }
}