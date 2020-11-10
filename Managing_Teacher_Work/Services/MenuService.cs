using Managing_Teacher_Work.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Managing_Teacher_Work.Services
{
    public interface IMenuService
    {
        Task<List<Menu>> GetMenuListAsync();
    }
    public class MenuService : IMenuService
    {
        private readonly AppDbContext _dbContext;
        public MenuService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Menu>> GetMenuListAsync()
        {
            return await _dbContext.Menu.OrderByDescending(m => m.CreatedDate).ToListAsync();
        }
    }
}