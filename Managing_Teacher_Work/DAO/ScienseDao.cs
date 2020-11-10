using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Managing_Teacher_Work.Models;
using PagedList;


namespace Managing_Teacher_Work.DAO
{
    public class ScienseDao
    {
        AppDbContext db = null;

        public ScienseDao()
        {
            db = new AppDbContext();
        }
        public IEnumerable<Science> Listpg(int page, int pageSize)
        {
            return db.Science.OrderByDescending(x => x.CreatedDate ).ToPagedList(page, pageSize);
        }
        public List<Science> ListAll()
        {
            return db.Science.ToList();
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
                var user = await db.Science.FindAsync(id);
                db.Science.Remove(user);
                await db.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }
        public Science GetScienceById(long id)
        {
            return db.Science.Find(id);
        }
    }
}