using Managing_Teacher_Work.Enums;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Managing_Teacher_Work.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionListAsync();
        Task<decimal> TotalDonateFee();
        Task<decimal> TotalUnionFee();
        Task<decimal> TotalCharityFee();
        Task<decimal> TotalAllTransaction();
        Task<decimal> GetTotalTransactionByTypeAndMonth(MonthEnum month, TransactionType type);
    }
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _dbContext;
        public TransactionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<decimal> GetTotalTransactionByTypeAndMonth(MonthEnum month, TransactionType type)
        {
            return await _dbContext.Transactions.Where(t => t.CreatedDate.Value.Month == (int)month && t.TransactionType == (int)type).Select(t => t.Amout).SumAsync();
        }

        public async Task<List<Transaction>> GetTransactionListAsync()
        {
            return await _dbContext.Transactions.OrderByDescending(t => t.CreatedDate).ToListAsync();
        }

        public async Task<decimal> TotalAllTransaction()
        {
            return await _dbContext.Transactions.Where(t => t.IsPaid).SumAsync(t => t.Amout);
        }

        public async Task<decimal> TotalCharityFee()
        {
            return await _dbContext.Transactions.Where(t => t.TransactionType == (int)TransactionType.CHARITY_FEE && t.IsPaid).SumAsync(t => t.Amout);
        }

        public async Task<decimal> TotalDonateFee()
        {
            return await _dbContext.Transactions.Where(t => t.TransactionType == (int)TransactionType.DONATE && t.IsPaid).SumAsync(t => t.Amout);
        }

        public async Task<decimal> TotalUnionFee()
        {
            return await _dbContext.Transactions.Where(t => t.TransactionType == (int)TransactionType.UNION_FEE && t.IsPaid).SumAsync(t => t.Amout);
        }
    }
}   