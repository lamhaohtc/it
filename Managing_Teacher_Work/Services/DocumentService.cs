using Managing_Teacher_Work.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Managing_Teacher_Work.Services
{
    public interface IDocumentService
    {
        Task<Document> AddNewDocumentAsync(Document model);
        Task UpdateDocumentAsync(Document model);
        Task<Document> GetByIdAsync(int id);
        Task<bool> DeteleByIdAsync(int id);
    }
    public class DocumentService : IDocumentService
    {
        private readonly AppDbContext _dbContext;
        public DocumentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Document> AddNewDocumentAsync(Document model)
        {
            _dbContext.Documents.Add(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<bool> DeteleByIdAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if(entity != null)
            {
                _dbContext.Documents.Remove(entity);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<Document> GetByIdAsync(int id)
        {
            return await _dbContext.Documents.FindAsync(id);
        }

        public async Task UpdateDocumentAsync(Document model)
        {
            _dbContext.Entry(model).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}