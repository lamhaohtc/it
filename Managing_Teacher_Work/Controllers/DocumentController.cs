using Managing_Teacher_Work.Helpers;
using Managing_Teacher_Work.Models;
using Managing_Teacher_Work.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Managing_Teacher_Work.Controllers
{
    [ValidateInput(false)]
    public class DocumentController : BaseController
    {
        private bool isAddNew;
        private readonly IDocumentService _documentService;
        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        public async Task<ActionResult> Index()
        {
            var documentList = await _documentService.GetDocumentListAsync();
            ViewBag.DocumentList = documentList;

            return View();
        }

        public async Task<ActionResult> Add(Document model, string submit)
        {
            if (model != null)
            {
                if (submit == "Thêm")
                {
                    isAddNew = true;
                    var document = new Document
                    {
                        Title = model.Title,
                        Description = model.Description,
                        CreatedDate = DateTimeHelper.GetCurrentDateTime(),
                        ModifiedDate = DateTimeHelper.GetCurrentDateTime(),
                        FilePath = model.FilePath
                    };
                    await _documentService.AddNewDocumentAsync(document);
                    SetAlert("Thêm thông tin thành công!", "success");

                    return RedirectToAction("Index");
                }
                else if (submit == "Cập Nhật")
                {
                    isAddNew = false;

                    var document = await _documentService.GetByIdAsync(model.ID);
                    document.Title = model.Title;
                    document.Description = model.Description;
                    document.ModifiedDate = DateTimeHelper.GetCurrentDateTime();
                    document.FilePath = model.FilePath;

                    await _documentService.UpdateDocumentAsync(document);

                    SetAlert("Cập nhật thông tin thành công!", "success");
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> getList(int id)
        {
            JsonSerializerSettings jss = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var hs = await _documentService.GetByIdAsync(id);
            var result = JsonConvert.SerializeObject(hs, Formatting.Indented, jss);

            return this.Json(result, JsonRequestBehavior.AllowGet);

        }

        public async Task<ActionResult> Delete(int id)
        {
            await _documentService.DeteleByIdAsync(id);

            return RedirectToAction("Index");
        }
    }
}