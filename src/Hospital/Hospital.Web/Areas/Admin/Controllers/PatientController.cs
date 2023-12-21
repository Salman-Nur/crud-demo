using Autofac;
using Hospital.Infrastructure;
using Hospital.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PatientController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<PatientController> _logger;
        public PatientController(ILifetimeScope scope, ILogger<PatientController> logger)
        {
            _scope = scope;
            _logger = logger;
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
                try
                {
                    model.Resolve(_scope);
                    await model.CreatePatientAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to create patient");
                }
            }

            return View(model);
        }
        public async Task<JsonResult> GetPatients()
        {
            var dataTablesModel = new DataTablesAjaxRequestUtility(Request);
            var model = _scope.Resolve<PatientListModel>();

            var data = await model.GetPagedPatientsAsync(dataTablesModel);
            return Json(data);
        }
    }
}
