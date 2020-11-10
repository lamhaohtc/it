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
    }
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _dbContext;
        public TransactionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Transaction>> GetTransactionListAsync()
        {
            return await _dbContext.Transactions.ToListAsync();
        }
    }
}   