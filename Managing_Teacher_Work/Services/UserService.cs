using Managing_Teacher_Work.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Managing_Teacher_Work.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetPageListUser(int pageNum, int pageSize);
        Task<List<User>> GetListUserAsync();
        Task<bool> DeleteUserByIdAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        List<string> GetCredentialListAsync(string username);

    }
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            if(user != null)
            {
                _dbContext.User.Remove(user);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public List<string> GetCredentialListAsync(string username)
        {
            var user = _dbContext.User.Single(x => x.UserName == username);

            var data = (from a in _dbContext.Credentials
                        join b in _dbContext.GroupUser on a.UserGroupID equals b.ID
                        join c in _dbContext.Role on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credentials()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });

            return data.Select(x => x.RoleID).ToList();
        }

        public async Task<List<User>> GetListUserAsync()
        {
            return await _dbContext.User.OrderByDescending(u => u.CreatedDate).ToListAsync();
        }

        public IEnumerable<User> GetPageListUser(int pageNum, int pageSize)
        {
            return _dbContext.User.OrderByDescending(x => x.CreatedDate).ToPagedList(pageNum, pageSize);
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            return _dbContext.User.FindAsync(id);
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _dbContext.User.Where(u => u.UserName == username).FirstOrDefaultAsync();
        }
    }
}