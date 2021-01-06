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
        Task<Transaction> AddTransactionAsync(Transaction model);
        Task<Transaction> GetTransactionByIdAync(int id);
        Task<bool> DeleteTransactionByIdAsync(int id);
        Task UpdateTransactionAsync(Transaction model);
        Transaction GetById(int id);
    }
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _dbContext;
        public TransactionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Transaction> AddTransactionAsync(Transaction model)
        {
            _dbContext.Transactions.Add(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<bool> DeleteTransactionByIdAsync(int id)
        {
            var transaction = await GetTransactionByIdAync(id);
            if(transaction != null)
            {
                _dbContext.Transactions.Remove(transaction);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public Transaction GetById(int id)
        {
            return _dbContext.Transactions.Find(id);
        }

        public async Task<decimal> GetTotalTransactionByTypeAndMonth(MonthEnum month, TransactionType type)
        {
            return await _dbContext.Transactions.Where(t => t.CreatedDate.Value.Month == (int)month && t.TransactionType == (int)type).Select(t => t.Amout).SumAsync();
        }

        public async Task<Transaction> GetTransactionByIdAync(int id)
        {
            return await _dbContext.Transactions.FindAsync(id);
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

        public async Task UpdateTransactionAsync(Transaction model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}   