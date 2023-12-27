using Autofac;
using Hospital.Infrastructure;
using Hospital.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PatientController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<PatientController> _logger;
        public PatientController(ILifetimeScope scope, ILogger<PatientController> logger)
        {
            _logger = logger;
            _scope = scope;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var model = _scope.Resolve<PatientCreateModel>();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientCreateModel model)
        {
            if (ModelState.IsValid)
            {
                model.Resolve(_scope);
                await model.CreateBookAsync();
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Patient created successfully.",
                    Type = ResponseTypes.Success
                });
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetPatients(PatientListModel model)
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            model.Resolve(_scope);
            var data = await model.GetPagedPatientsAsync(dataTablesModel);
            return Json(data);
        }
        public async Task<IActionResult> Update(Guid id)
        {
            var model = _scope.Resolve<PatientUpdateModel>();
            await model.LoadAsync(id);
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PatientUpdateModel model)
        {
            model.Resolve(_scope);
            if (ModelState.IsValid)
            {
                await model.UpdatePatientAsync();
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Patient edited successfully.",
                    Type = ResponseTypes.Success
                });
                return RedirectToAction("Index");
            }
            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var model = _scope.Resolve<PatientListModel>();
            if (ModelState.IsValid)
            {
                await model.DeletePatientAsync(id);
                TempData.Put("ResponseMessage", new ResponseModel
                {
                    Message = "Patient deleted successfully.",
                    Type = ResponseTypes.Success
                });
            }
            return RedirectToAction("Index");
        }
    }
}
